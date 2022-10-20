using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RunAsAdminTool
{
    class ServiceStartStop
    {
        public void StartService(string serviceName, int timeoutserverSeconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutserverSeconds);

                service.Start();
                //service.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch
            {
                // ...
            }
        }
        public void StopService(string serviceName, int timeoutserverSeconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutserverSeconds);

                service.Stop();
                //service.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            catch
            {
                // ...
            }
        }
    }
}
