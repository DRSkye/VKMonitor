using System.ServiceProcess;

namespace VKMonitorService
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            var servicesToRun = new ServiceBase[]
            {
                new VKMonitorService()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
