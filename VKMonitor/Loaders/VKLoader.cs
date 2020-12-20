using System.Collections.Generic;
using System.IO;
using System.Linq;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using User = VKMonitor.Model.User;
using System.Linq;
using VkNet.Model.RequestParams;

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

        public User GetUser(long id)
        {
            var user = _api.Users.Get(new List<long> {id}, ProfileFields.All).FirstOrDefault();

            var newUser = new User(user);


            return newUser;
        }

        public List<User> GetUsers(List<long> list)
        {
            var users = _api.Users.Get(list, ProfileFields.All);
            var newUsers = users.AsParallel().Select(x => new User(x)).ToList();

            return newUsers;
        }

        public string GetUserName(long id)
        {
            var name = _api.Users.Get(new List<long> {id}, ProfileFields.ScreenName).FirstOrDefault().ScreenName;
            return name;
        }
    }
}
