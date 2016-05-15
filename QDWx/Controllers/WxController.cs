using QDWx.CommonService.MessageHandlers;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.MvcExtension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QDWx.Controllers
{
    public class WxController : Controller
    {
        private static readonly string WxAppID = ConfigurationManager.AppSettings["WxAppID"];
        private static readonly string WxAppSecret = ConfigurationManager.AppSettings["WxAppSecret"];
        private static readonly string WxToken = ConfigurationManager.AppSettings["WxToken"];
        private static readonly string WxEncodingAESKey = ConfigurationManager.AppSettings["WxEncodingAESKey"];

        // GET api/wx
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, WxToken))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + postModel.Signature + "," + Senparc.Weixin.MP.CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, WxToken) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, WxToken))
            {
                //return Content("参数错误！");//v0.7-
                return new WeixinResult("参数错误！");//v0.8+
            }

            postModel.Token = WxToken;
            postModel.EncodingAESKey = WxEncodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = WxAppID;//根据自己后台的设置保持一致

            var messageHandler = new CustomMessageHandler(Request.InputStream, postModel, 10);

            messageHandler.Execute();//执行微信处理过程

            //return Content(messageHandler.ResponseDocument.ToString());//v0.7-
            //return new FixWeixinBugWeixinResult(messageHandler);//v0.8+
            return new WeixinResult(messageHandler);//v0.8+
        }

        [ChildActionOnly]
        public ActionResult JsConfig()
        {
            ViewModel.JsConfig config = new ViewModel.JsConfig();
            config.ticket = CommonApi.GetTicket(WxAppID, WxAppSecret).ticket;
            config.appId = WxAppID;
            config.timestamp = JSSDKHelper.GetTimestamp(); ;
            config.nonceStr = JSSDKHelper.GetNoncestr();
            config.url = Request.Url.AbsoluteUri.ToString();
            config.signature = JSSDKHelper.GetSignature(config.ticket, config.nonceStr, config.timestamp, config.url);
            return PartialView("~/Views/Wx/JsConfig.cshtml", config);
        }

        public ActionResult Scan()
        {
            return View();
        }

        #region Ajax

        public ActionResult CheckQrCode(string code)
        {
            if (MessageEntity.MessageManager.Check(code))
            {
                return Json(new { result = "true" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "false" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
