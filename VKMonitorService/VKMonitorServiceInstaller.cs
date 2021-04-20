using System.ComponentModel;
using System.ServiceProcess;

namespace VKMonitorService
{
    [RunInstaller(true)]
    public partial class VKMonitorServiceInstaller : System.Configuration.Install.Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public VKMonitorServiceInstaller()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller
            {
                StartType = ServiceStartMode.Manual,
                ServiceName = "VKMonitorService"
            };

            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };


            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
