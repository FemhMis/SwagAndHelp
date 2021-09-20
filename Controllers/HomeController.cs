using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwagAndHelp.Models;

namespace SwagAndHelp.Controllers
{
    /// <summary>
    /// home controller
    /// </summary>
    public class HomeController : Controller
    {
        internal static Dictionary<string, string> loadedXmlDocumentation = new Dictionary<string, string>();
        /// GET: Home
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Api()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Models()
        {
            string sPath = Server.MapPath("~/App_Data/SwagAndHelp.xml");
            try
            {
                #region "微軟提供的Function"
                using (XmlReader xmlReader = XmlReader.Create(new StringReader(System.IO.File.ReadAllText(sPath))))
                {
                    while (xmlReader.Read())
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "member")
                        {
                            string raw_name = xmlReader["name"];
                            loadedXmlDocumentation[raw_name] = xmlReader.ReadInnerXml();
                        }
                    }
                }
                #endregion
                var oLinQ = loadedXmlDocumentation.Select(
                    x => new MyDocument()
                    {
                        Key = x.Key,
                        Value = x.Value
                    }).Where(x => x.Key.IndexOf("SwagAndHelp.Controllers") != -1 || x.Key.IndexOf("SwagAndHelp.Models") != -1).ToList();
                List<ModelInfo> oList =
                    oLinQ.Where(x => x.Type == "T").Select(
                        x => new ModelInfo()
                        {
                            Name = x.Name,
                            Desc = x.Summary,
                            Cols = oLinQ.Where(y => y.Type == "P").Where(y => x.Name == y.Parent).Select(
                                y => new ColumnInfo()
                                {
                                    Name = y.Name,
                                    Type = "",
                                    Desc = y.Summary,
                                    Rule = ""
                                }).ToList()
                        }).Where(x => x.Cols.Count > 0).ToList();
                return View(oList);
            }
            catch (Exception _ex)
            {
                TempData["ErrorInfo"] = new ErrorInfo() { Act = "Home", Ctrl = "Models", Ex = _ex };
                return RedirectToAction("Error", "Error");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ResourceModel(string modelName)
        {
            string sPath = Server.MapPath("~/App_Date/SwagAndHelp.xml");
            #region "微軟提供的Function"
            using (XmlReader xmlReader = XmlReader.Create(new StringReader(System.IO.File.ReadAllText(sPath))))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "member")
                    {
                        string raw_name = xmlReader["name"];
                        loadedXmlDocumentation[raw_name] = xmlReader.ReadInnerXml();
                    }
                }
            }
            #endregion
            try
            {
                var oLinQ = loadedXmlDocumentation.Select(
                x => new MyDocument()
                {
                    Key = x.Key,
                    Value = x.Value
                }).Where(x => x.Type == "T").Where(x => x.Name == modelName).First();
                var sVal = oLinQ.FullNameSpace.IndexOf("Controller.") >= 0 ? oLinQ.FullNameSpace.Replace("Controller.", "Controller+") : oLinQ.FullNameSpace;
                Type t = Type.GetType(sVal);
                var oTMP = loadedXmlDocumentation.Select(
                    x => new MyDocument()
                    {
                        Key = x.Key,
                        Value = x.Value
                    }).Where(x => x.Type == "P").Where(x => x.FullNameSpace == oLinQ.FullNameSpace).ToList();
                ModelInfo oList = new ModelInfo()
                {
                    Name = t.Name,
                    Desc = oLinQ.Summary,
                    Cols = t.GetProperties().Select(
                        x => new ColumnInfo()
                        {
                            Name = x.Name,
                            Desc = oTMP.Where(y => y.Name == x.Name).First().Summary,
                            Type = (x.PropertyType.FullName.IndexOf("Generic.List") == -1 ? x.PropertyType.Name : "List Of "),
                            Children = (x.PropertyType.FullName.IndexOf("Generic.List") == -1 ? "" : x.PropertyType.GenericTypeArguments.First().Name),
                            Rule = ""
                        }).ToList(),
                };
                return View(oList);
            }
            catch (Exception _ex)
            {
                TempData["ErrorInfo"] = new ErrorInfo() { Act = "Home", Ctrl = "ResourceModel", Ex = _ex };
                return RedirectToAction("Error", "Error");
            }
        }
    }
}