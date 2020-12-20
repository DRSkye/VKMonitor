using System;
using System.Collections.Generic;
using VKMonitor.Loaders;

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

            var user = loader.GetUsers(new List<long>{ 76333002, 200353149 });
        }
    }
}
