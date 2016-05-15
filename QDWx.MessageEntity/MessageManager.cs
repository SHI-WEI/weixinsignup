using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace QDWx.MessageEntity
{
    public class MessageManager
    {
        private static MessageEntity CurrentCode
        {
            get
            {
                if (HttpContext.Current.Cache["CurrentCode"] == null)
                {
                    MessageEntity _current_code = new MessageEntity(GetRandomCode(6));
                    HttpContext.Current.Cache.Insert("CurrentCode", _current_code, null,
                        _current_code.CreateTime.AddSeconds(10), Cache.NoSlidingExpiration);
                    return _current_code;
                }
                else
                {
                    return HttpContext.Current.Cache["CurrentCode"] as MessageEntity;
                }
            }
        }

        private static string GetRandomCode(int length)
        {
            string code = "";
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                code += r.Next(10);
            }
            return code;
        }

        public static string GetCode()
        {
            return CurrentCode.Code;
        }

        public static bool Check(string code)
        {
            return CurrentCode.Check(code);
        }
    }
}
