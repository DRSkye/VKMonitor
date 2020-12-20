using System;
using VKMonitor;
using VKMonitor.Loaders;
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

        }
    }
}
