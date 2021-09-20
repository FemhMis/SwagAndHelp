using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using NSwag.AspNet.Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(SwagAndHelp.OwinStartUp))]

namespace SwagAndHelp
{
    #region "參考至：https://blog.darkthread.net/blog/use-nswag-on-aspnet-webapi2/"
    //1. 加入以下三個元件
    //Microsoft.AspNet.WebApi.Owin
    //Microsoft.Owin.Host.SystemWeb
    //NSwag.AspNet.Owin
    //2. 加入，新增項目，(C#全部)Owin啟動檔案
    //3. 在Owin啟動檔案裏面加入以下引用：（Microsoft.Owin 與 Owin 是原本就有引用）
    // using NSwag.AspNet.Owin;
    // using System.Web.Http;
    //4. 放入已下程式碼：(程式碼已放在下面)
    //5. Web.config 加入：(between <handlers> and </handlers> )
    //<add name = "NSwag" path="swagger" verb="*" type="System.Web.Handlers.TransferRequestHandler" preCondition="integratedMode,runtimeVersionv4.0" />
    //6. 開啟XML說明擋。
    //7. 在Global,asmx呼叫OwinStartUp
    #endregion
    /// <summary>
    /// Owin StartUp 
    /// </summary>
    public class OwinStartUp
    {
        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // 如需如何設定應用程式的詳細資訊，請參閱  http://go.microsoft.com/fwlink/?LinkID=316888
            var config = new HttpConfiguration();
            app.UseSwaggerUi3(typeof(OwinStartUp).Assembly, settings =>
            {
                                //針對RPC-Style WebAPI，指定路由包含Action名稱
                                settings.GeneratorSettings.DefaultUrlTemplate =
                    "api/{controller}/{action}/{id?}";
                                //可加入客製化調整邏輯
                                settings.PostProcess = document =>
                {
                    document.Info.Title = "WebAPI Swag範例";
                };
            });
            app.UseWebApi(config);
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
        }
    }
}
