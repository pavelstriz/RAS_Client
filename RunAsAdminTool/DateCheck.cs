using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunAsAdminTool
{
    class DateCheck
    {
        private System.Threading.Timer m_objTimer;
        private bool m_blnStarted;
        private readonly int m_intTickMs = 10000; //Call back every 30s
        private object m_objLockObject = new object();

            //Create your timer object, but don't start anything yet
            
        public void Start()
        {
            m_objTimer = new System.Threading.Timer(callback, m_objTimer, Timeout.Infinite, Timeout.Infinite);
            if (!m_blnStarted)
            {
                lock (m_objLockObject)
                {
                    if (!m_blnStarted) //double check after lock to be thread safe
                    {
                        m_blnStarted = true;

                        //Make it start in 'm_intTickMs' milliseconds, 
                        //but don't auto callback when it's done (Timeout.Infinite)
                        m_objTimer.Change(m_intTickMs, Timeout.Infinite);



                    }
                }
            }
        }

        public void Stop()
        {
            lock (m_objLockObject)
            {
                m_blnStarted = false;
            }
        }

        private void callback(object state)
        {
            System.Diagnostics.Debug.WriteLine("callback invoked");

            //TODO: your code here

            
            Constants.dateToday = DateTime.Now.ToShortDateString();

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
                //MessageBox.Show(Constants.sOnlineDate.ToShortDateString());



               /* if (DateTime.Now <= Constants.expirationDate && (Constants.dateToday).Equals(Constants.sOnlineDate.ToShortDateString()))
                {

                }
                else
                {
                    MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }*/
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("No internet connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }


            Thread.Sleep(4000);

            //When your code has finished running, wait 'm_intTickMs' milliseconds
            //and call the callback method again, 
            //but don't auto callback (Timeout.Infinite)
            m_objTimer.Change(m_intTickMs, Timeout.Infinite);
        }
    }
}


/*Constants.dateToday = DateTime.Now.ToShortDateString();
            Nullable<DateTime> dateTime = null;
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://www.microsoft.com");
            request.Method = "GET";*/
//request.Accept = "text/html, application/xhtml+xml, */*";
/*request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
request.ContentType = "application/x-www-form-urlencoded";
request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
try
{
    System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
    if (response.StatusCode == System.Net.HttpStatusCode.OK)
    {
        string todaysDates = response.Headers["date"];

        dateTime = DateTime.ParseExact(todaysDates, "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
            System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat, System.Globalization.DateTimeStyles.AssumeUniversal);

        Constants.sOnlineDate = Convert.ToDateTime(dateTime);
    }
}
catch
{
    dateTime = null;
}*/