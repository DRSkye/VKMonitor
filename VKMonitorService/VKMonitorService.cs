using System.ServiceProcess;

namespace VKMonitorService
{
    public partial class VKMonitorService : ServiceBase
    {
        public VKMonitorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {

        }
    }
}
