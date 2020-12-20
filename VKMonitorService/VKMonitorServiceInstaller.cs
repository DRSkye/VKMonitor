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
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "VKMonitorService";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
