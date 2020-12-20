using System;
using System.Collections.Generic;
using VKMonitor.Loaders;
using VKLogger;
using VKMonitor.Model;

namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            VKLoader loader = new VKLoader();

            var res = loader.Authorize();

            if (!res)
            {
                Console.WriteLine($@"Ошибка авторизации!");
            }
            else
            {
                Console.WriteLine($@"Успешная авторизация");
            }

            var users = loader.GetUsers(new List<long>{ 200353149 });
            
            Logger.WriteUsers(users);
        }
    }
}
