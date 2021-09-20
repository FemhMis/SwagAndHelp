using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SwagAndHelp.Models;
using System.Threading.Tasks;

namespace SwagAndHelp.Controllers
{
    /// <summary>
    /// api controller
    /// </summary>
    public class apiController : ApiController
    {
        /// <summary>
        /// this is my first web api
        /// </summary>
        /// <param name="nm"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/first/Get")]
        public async Task<ReturnResult> MyFirstAPI(string nm)
        {
            return await Task.Run(() => (new ReturnResult() { Result = true, Msg = "程式運作正常。", Data = "Hello Wolrd. " + nm }));
        }
    }
}
