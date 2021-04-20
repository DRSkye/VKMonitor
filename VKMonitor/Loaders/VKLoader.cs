using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using User = VKMonitor.Model.User;

namespace VKMonitor.Loaders
{
    public static class VKLoader
    {
        private static VkApi _api;

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <returns>Возвращает true, если успешно</returns>
        public static bool Authorize()
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

        private static User GetUser(long id)
        {
            var user = _api.Users.Get(new List<long> {id}, ProfileFields.All).FirstOrDefault();
            var newUser = new User(user);

            return newUser;
        }

        public static List<User> GetUsers(List<long> list)
        {
            List<User> users = new List<User>();
            try
            {
                users = list.AsParallel().Select(GetUser).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return users;
        }

        public static string GetUserName(long id)
        {
            var name = _api.Users.Get(new List<long> {id}, ProfileFields.ScreenName).FirstOrDefault().ScreenName;
            return name;
        }
    }
}
