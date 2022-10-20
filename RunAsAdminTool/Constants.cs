using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunAsAdminTool
{
    class Constants
    {
        public static string userName = "Administrator";

        public static SecureString pass;
        

        public static DateTime expirationDate = DateTime.Parse("30.11.2022");

        public static string serverAddress = "192.168.2.134";//2.134";
        public static int serverPort = 5500;

        public static int licenseCheckAfter = 3;


        public static string ntpServer1 = "time.windows.com";
        public static string ntpServer2 = "time.nist.gov";
        public static string ntpServer3 = "pool.ntp.org";

        public static String macAdress = NetworkInterface
                        .GetAllNetworkInterfaces()
                        .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                        .Select(nic => nic.GetPhysicalAddress().ToString())
                        .FirstOrDefault();

        

        public static string dateToday;
        public static DateTime sOnlineDate;



        #region ERRORS
        public const int ERROR_CANCELED = 1223; //The operation was canceled by the user.
        #endregion
    }

}
