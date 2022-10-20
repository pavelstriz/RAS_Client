using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunAsAdminTool
{
    static class Program
    {
        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]

        
        static void Main()
        {
                

            Constants.dateToday = DateTime.Now.ToShortDateString();



            //NTP DATE
            var ntpData = new byte[48];
            ntpData[0] = 0x1B; //LeapIndicator = 0 (no warning), VersionNum = 3 (IPv4 only), Mode = 3 (Client Mode)
            try
            {
                var addresses = Dns.GetHostEntry(Constants.ntpServer2).AddressList;
                var ipEndPoint = new IPEndPoint(addresses[0], 123);
                var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);


                socket.Connect(ipEndPoint);
                socket.Send(ntpData);
                socket.Receive(ntpData);
                socket.Close();

                ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
                ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);
                Constants.sOnlineDate = Convert.ToDateTime(networkDateTime.ToShortDateString());

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                Loading1 ucl = new Loading1();
                ucl.Show();

                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                Application.Run();
                /*if (DateTime.Now <= Constants.expirationDate && Constants.dateToday.Equals(Constants.sOnlineDate.ToShortDateString()))
                {

                   Application.EnableVisualStyles();
                   Application.SetCompatibleTextRenderingDefault(false);

                   Loading1 ucl = new Loading1();
                   ucl.Show();

                   AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                   Application.Run();



                }
                else
                {
                   Loading1 ucl = new Loading1(); //no connection to the server
                   ucl.Show();
                    //MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Application.Exit();
               }*/
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("No internet connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


        }
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Load();
        }
        public static Assembly Load()
        {
            byte[] ba = null;
            string resource = "TcpClientPortable.Microsoft.Win32.TaskScheduler.dll";
            Assembly curAsm = Assembly.GetExecutingAssembly();
            using (Stream stm = curAsm.GetManifestResourceStream(resource))
            {
                ba = new byte[(int)stm.Length];
                stm.Read(ba, 0, (int)stm.Length);

                return Assembly.Load(ba);
            }
        }
    }
}

