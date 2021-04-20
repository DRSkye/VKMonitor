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
                Stop();

            timer = new Timer
            {
                AutoReset = false,
                Enabled = true,
                Interval = TimeSpan.FromMinutes(5).Milliseconds
            };
            timer.Elapsed += onTimer;

            usersIds = new List<long>
            {
                200353149
            };
        }

        private static void onTimer(object source, ElapsedEventArgs e)
        {
            var help = curUsers;
            curUsers = VKLoader.GetUsers(usersIds);

            if (curUsers == null || !curUsers.Any())
            {
                return;
            }

            lastUsers = help;

            Logger.WriteUsers(curUsers);
            if (lastUsers.Any())
            {
                Logger
            }
        }

        protected override void OnStart(string[] args)
        {
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer.Dispose();
        }

        protected override void OnPause()
        {
            timer.Stop();
        }

        protected override void OnContinue()
        {
            timer.Start();
        }
    }
}
