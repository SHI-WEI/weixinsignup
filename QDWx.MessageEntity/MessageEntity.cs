using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QDWx.MessageEntity
{
    class MessageEntity
    {
        private string _code;
        private DateTime _create_time;

        public MessageEntity(string code)
        {
            this._code = code;
            this._create_time = DateTime.UtcNow;
        }
        public bool Check(string code)
        {
            if (this._code.Equals(code))
                return true;
            else
                return false;
        }
        public string Code
        {
            get
            {
                return this._code;
            }
        }
        public DateTime CreateTime
        {
            get
            {
                return this._create_time;
            }
        }
    }
}
