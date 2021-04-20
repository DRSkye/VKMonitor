using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using VKLogger;
using VKMonitor.Loaders;
using VKMonitor.Model;

namespace VKMonitorService
{
    public partial class VKMonitorService : ServiceBase
    {
        private Timer timer;

        private static List<long> usersIds = new List<long>();

        private static List<User> lastUsers = new List<User>();
        private static List<User> curUsers = new List<User>();

        public VKMonitorService()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
            Init();
        }

        private void Init()
        {
            var res = VKLoader.Authorize();

            if (!res)
            {
                Logger.WriteError("Авторизация вк не была произведена!");
                return;
            }

            try
            {
                timer = new Timer
                {
                    AutoReset = false,
                    Enabled = false,
                    Interval = 1000
                };
                timer.Elapsed += OnTimer;
            }
            catch (Exception e)
            {
                Logger.WriteError("Ошибка создания таймера", e);
            }

            usersIds = new List<long>
            {
                200353149,
                217856897
            };
        }

        private static void OnTimer(object source, ElapsedEventArgs e)
        {
            var help = curUsers;
            try
            {
                curUsers = VKLoader.GetUsers(usersIds);
            }
            catch (Exception exception)
            {
                Logger.WriteError("Ошибка получения пользователей", exception);
            }

            if (curUsers == null || !curUsers.Any())
            {
                return;
            }

            lastUsers = help;

            Logger.WriteUsers(curUsers);
            if (lastUsers.Any())
            {
                Logger.WriteChanges(lastUsers, curUsers);
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                timer.Start();
            }
            catch (Exception e)
            {
                Logger.WriteError("Ошибка запуска таймера!", e);
            }
            
        }

        protected override void OnStop()
        {
            try
            {
                timer.Stop();
                timer.Dispose();
            }
            catch (Exception e)
            {
                Logger.WriteError("Ошибка остановки таймера!", e);
            }
        }

        protected override void OnPause()
        {
            try
            {
                timer.Stop();
            }
            catch (Exception e)
            {
                Logger.WriteError("Ошибка приостановки таймера!", e);
            }
        }

        protected override void OnContinue()
        {
            try
            {
                timer.Start();
            }
            catch (Exception e)
            {
                Logger.WriteError("Ошибка возобновления таймера!", e);
            }
        }
    }
}
