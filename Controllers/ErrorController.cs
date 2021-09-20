using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwagAndHelp.Models;

namespace SwagAndHelp.Controllers
{
    /// <summary>
    /// error controller
    /// </summary>
    public class ErrorController : Controller
    {
        /// GET: Error
        public ActionResult Error()
        {
            try
            {
                if (TempData["ErrorInfo"] == null)
                    return View(new ErrorInfo() { Ctrl = "Error", Act = "Error", Ex = new Exception("系統發生錯誤，但是並未提供錯誤訊息。") });
                else
                    return View((ErrorInfo)TempData["ErrorInfo"]);
            }
            catch (Exception _ex)
            {
                return View(new ErrorInfo() { Ctrl = "Error", Act = "Error", Ex = _ex });
            }
        }
    }
}