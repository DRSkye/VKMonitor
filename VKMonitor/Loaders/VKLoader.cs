using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VKMonitor.Model;
using User = VKMonitor.Model.User;

namespace VKMonitor.Loaders
{
    public class VKLoader
    {
        private VkApi _api;

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <returns>Возвращает true, если успешно</returns>
        public bool Authorize()
        {
            try
            {
                string token;
                using (var reader = new StreamReader("AccesToken.txt"))
                {
                    token = reader.ReadLine();
                    if (string.IsNullOrEmpty(token))
                    {
                        return false;
                    }
                    
                }

                _api = new VkApi();
                _api.Authorize(new ApiAuthParams
                {
                    AccessToken = token
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Model.User GetUser(long id)
        {
            var user = _api.Users.Get(new List<long>() {id}, ProfileFields.All).FirstOrDefault();

            User newUser = new User(user);
        }
    }
}
