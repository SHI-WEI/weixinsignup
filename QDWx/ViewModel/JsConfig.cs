using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QDWx.ViewModel
{
    public class JsConfig
    {
        public string ticket { set; get; }
        public string appId { set; get; }
        public string timestamp { set; get; }
        public string nonceStr { set; get; }
        public string signature { set; get; }
        public string url { set; get; }
    }
}