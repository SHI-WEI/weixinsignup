using QDWx.MessageEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QDWx.Controllers
{
    public class BMSController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region Ajax

        public ActionResult GetQrCode()
        {
            string code = MessageManager.GetCode();
            return Json(new { url = "/BMS/QrCode?code=" + code }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QrCode(string code)
        {
            var image = MessageEntity.QrCodeHelper.GetQrCode(code);
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            image.Dispose();
            return File(ms.ToArray(), "image/jpeg");
        }

        #endregion
    }
}
