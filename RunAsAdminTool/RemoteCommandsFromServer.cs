using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunAsAdminTool
{
    class RemoteCommandsFromServer
    {
        //*******WMI
        /*FLAGS
        0 - logoff 
        1 - shutdown
        2 - restart
        8 - power off

        0 + 4 - forced logoff
        1 + 4 - forced shutdown
        2 + 4 - forced reboot
        12 - forced poweroff
        */

        public static void WMI(int flagN)
        {
            ManagementBaseObject mbo = null;
            ManagementClass mcWin32 = new ManagementClass("Win32_OperatingSystem");
            mcWin32.Get();

            // You can't shutdown without security privileges
            mcWin32.Scope.Options.EnablePrivileges = true;
            ManagementBaseObject mboParams =
                     mcWin32.GetMethodParameters("Win32Shutdown");

            // Flag 1 means we want to shut down the system. Use "2" to reboot.
            mboParams["Flags"] = flagN;
            mboParams["Reserved"] = "0";
            foreach (ManagementObject manObj in mcWin32.GetInstances())
            {
                mbo = manObj.InvokeMethod("Win32Shutdown",
                                               mboParams, null);
            }
        }
        //*****Hibernate

        //[DllImport("Powrprof.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        //public static extern bool HibernateM(bool hiberate, bool forceCritical, bool disableWakeEvent);





        //AC / DC 
        public static Guid GUID_SYSTEM_BUTTON_SUBGROUP =
            new Guid("4f971e89-eebd-4455-a8de-9e59040e7347");
        public static Guid GUID_SLEEPBUTTON =
            new Guid("96996bc0-ad50-47ec-923b-6f41874dd9eb ");
        public static Guid GUID_POWERBUTTON =
            new Guid("7648efa3-dd9c-4e3e-b566-50f929386280");
        //Hibernate & Sleep after
        public static Guid GUID_SLEEP_SUBGROUP =
            new Guid("238c9fa8-0aad-41ed-83f4-97be242c8f20");
        public static Guid GUID_HIBERNATEIDLE =
            new Guid("9d7815a6-7ee4-497e-8888-515a05f02364");
        public static Guid GUID_SLEEPAFTER =
            new Guid("29f6c1db-86da-48c5-9fdb-f2b67b1f44da");


        // Hibernate
        public static void DoHibernate()
        {
            Application.SetSuspendState(PowerState.Hibernate, true, true);
        }
        public static void DoSleep()
        {
            Application.SetSuspendState(PowerState.Suspend, true, true);
        }
        //LockSystem
        [DllImport("user32.dll")]
        public static extern void LockWorkStation();




        //HIBERNATE & SLEEP & DISPLAY TIMEOUT

        /*
        Timeout to turn off the display (plugged in): monitor-timeout-ac xx.
        Timeout to turn off the display (battery): monitor-timeout-dc xx.
        Timeout to go to sleep (plugged in): standby-timeout-ac xx.
        Timeout to go to sleep (battery): standby-timeout-dc xx.
        Timeout to go into hibernate (plugged in): hibernate-timeout-ac xx.
        Timeout to go into hibernate (battery): hibernate-timeout-dc xx.
         */
        public static string AC_monitor_timeout = "monitor-timeout-ac"; //vypnutí monitoru (baterie)
        public static string DC_monitor_timeout = "monitor-timeout-dc"; //vypnutí monitoru (síť)
        public static string AC_sleep_timeout = "standby-timeout-ac"; //režim spánku (baterie)
        public static string DC_sleep_timeout = "standby-timeout-dc"; //režim spánku (síť)
        public static string AC_hibernate_timeout = "hibernate-timeout-ac"; //režim hibernace (baterie)
        public static string DC_hibernate_timeout = "hibernate-timeout-dc"; //režim hibernace (síť)


        public static void SetClientTimeout(string action, int? minutes, string never)
        {
            var info = new ProcessStartInfo("cmd", "/C powercfg /change " + action + " " + minutes + " "+ never + "");
            info.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(info);
        }






        // EXECUTE COMMAND PROMT COMMAND
        public static void ExecuteCMD(String executeCMD)
        {
            
                var processInfo = new ProcessStartInfo("cmd.exe", "/c " + executeCMD);
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;

                var process = Process.Start(processInfo);



          /*      Process p = new Process();

            p.StartInfo.UseShellExecute = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = executeCMD;
            p.Start();*/
        }
    }


}
