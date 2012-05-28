using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVM;
using ServiceStack.Redis;
using svmFace.Data;

namespace svmFace
{

    public class Redis
    {
        private static Redis instance;

        private RedisClient _client;

        private static String listName = "people2";




        private Redis() {
          _client = new RedisClient();
        }

        public static Redis getInstance() {
            if(Redis.instance == null) {
                Redis.instance = new Redis();
            }
            return Redis.instance;
        }

        public void registerPerson(Person man) {
            var typed = _client.GetTypedClient<Person>();
            var list = typed.Lists[listName];
            typed.AddItemToList(list, man);
        }

        public ServiceStack.Redis.Generic.IRedisList<Person> planes() {
            return _client.GetTypedClient<Person>().Lists[listName];
        }
 
        public void wipe()
        {
            _client.Del(listName);
        }
    }
}
