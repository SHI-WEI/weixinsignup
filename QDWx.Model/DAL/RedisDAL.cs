using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ServiceStack;
using ServiceStack.Redis;

namespace QDWx.Model.DAL
{
    class RedisDAL
    {
        private readonly string _redisUrl = ConfigurationManager.AppSettings["RedisUrl"];
        private readonly string _redisPwd = ConfigurationManager.AppSettings["RedisPwd"];
        private RedisClient _client;

        public RedisDAL()
        {
            _client = new RedisClient(_redisUrl, 6379, _redisPwd);
        }

        public bool Exists(string key)
        {
            return _client.Exists(key) == 0 ? false : true;
        }

        public T Get<T>(string key)
        {
            T result = default(T);
            try
            {
                result = _client.Get<T>(key);
            }
            catch
            {
                result = default(T);
            }
            return result;
        }

        public bool Add<T>(string key, T value)
        {
            try
            {
                _client.Add<T>(key, value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
