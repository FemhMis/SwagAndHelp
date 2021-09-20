using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwagAndHelp.Models
{
    #region "Default Models, Please don't edit, if you edit maybe error."
    internal class MyDocument
    {
        internal string Type { get { return Key.Substring(0, 1); } }
        internal string Name { get { return oArr.Last(); } }
        internal string FullNameSpace
        {
            get
            {
                switch (Type)
                {
                    case "T":
                        return string.Join(".", oArr.ToArray());
                    case "P":
                        return string.Join(".", oArr.Take(oArr.Count - 1));
                    default:
                        return "";
                }
            }
        }
        internal string Summary { get { return Value.Replace("\n            <summary>\n            ", "").Replace("\n            </summary>\n        ", ""); } }
        internal string Parent
        {
            get
            {
                if (Type == "T")
                {
                    return "";
                }
                else
                {
                    return oArr[oArr.Count - 2];
                }
            }
        }
        internal List<string> oArr { get { return Key.Substring(2, Key.Length - 2).Split(new string[] { "." }, StringSplitOptions.None).ToList(); } }
        internal string Key { get; set; }
        internal string Value { get; set; }
    }
    /// <summary>
    /// Model Class
    /// </summary>
    public class ModelInfo
    {
        /// <summary>
        /// model name (single)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// description 
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// columns
        /// </summary>
        public List<ColumnInfo> Cols { get; set; }
    }
    /// <summary>
    /// column class
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// column name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// column property
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// column Rule
        /// </summary>
        public string Rule { get; set; }
        /// <summary>
        /// description 
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Children { get; set; }
    }
    /// <summary>
    /// error class
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// 錯誤的Ctrl
        /// </summary>
        public string Ctrl { get; set; } = "";
        /// <summary>
        /// 錯誤的Action
        /// </summary>
        public string Act { get; set; } = "";
        /// <summary>
        /// 錯誤的訊息
        /// </summary>
        public Exception Ex { get; set; } = new Exception() { };
    }
    #endregion
    
    /// <summary>
    /// return result
    /// </summary>
    public class ReturnResult
    {
        /// <summary>
        /// default class
        /// </summary>
        public ReturnResult()
        {
            Result = false;
            Msg = "初始化類別";
            Data = new System.Dynamic.ExpandoObject() { };
        }
        /// <summary>
        /// progress result
        /// </summary>
        public Boolean Result { get; set; }
        /// <summary>
        /// progress message
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// return data (dynamic type)
        /// </summary>
        public dynamic Data { get; set; }
    }

    /// <summary>
    /// this is my first class
    /// </summary>
    public class MyFirstClass
    {
        /// <summary>
        /// first column （type of string）
        /// </summary>
        public string FirstCol { get; set; }
        /// <summary>
        /// Second column （type of DateTime）
        /// </summary>
        public DateTime SecondCol { get; set; }
        /// <summary>
        /// Third column （type of decimal）
        /// </summary>
        public decimal ThirdCol { get; set; }
    }
    /// <summary>
    /// this is my second class
    /// </summary>
    public class MySecondClass
    {
        /// <summary>
        /// column 1
        /// </summary>
        public string Col01 { get; set; }
        /// <summary>
        /// array 1
        /// </summary>
        public List<string> Arr01 { get; set; }
        /// <summary>
        /// list of first class
        /// </summary>
        public List<MyFirstClass> List01 { get; set; }
    }
}