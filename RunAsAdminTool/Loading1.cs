using RunAsAdminTool.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunAsAdminTool
{
    public partial class Loading1 : Form
    {
        public class StateObject
        {
            // Client socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 256;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
            // Received data string.  
            public StringBuilder sb = new StringBuilder();
        }

        public Loading1()
        {
            InitializeComponent();



        }
        Bitmap nullBitmap = new Bitmap(1, 1);



        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent clientInfoDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);
        public static Socket client;

        static CancellationTokenSource ts;
        static CancellationToken ct;

        /// <summary>
        /// One remote command cancels previous command
        /// </summary>
        public static CancellationTokenSource ts_remoteCommands;
        public static CancellationToken ct_remoteCommands;
        public BackgroundWorker backgroundworker1;
        private void UC_Loading1_Load(object sender, EventArgs e)
        {
            backgroundworker1 = new BackgroundWorker();
            //backgroundworker1.WorkerReportsProgress = true;
            backgroundworker1.DoWork += new DoWorkEventHandler(backgroundworker1_DoWork);

            GetLocalIPAddress();
            GetActiveUser();
            GetMacAddress();
            //GetLicense();

            ts = new CancellationTokenSource();
            ct = ts.Token;
            ct.Register(() => Console.WriteLine("Stopping License Check task"));

            ts_remoteCommands = new CancellationTokenSource();
            ct_remoteCommands = ts_remoteCommands.Token;
            ct_remoteCommands.Register(() => Console.WriteLine("Stopping Timed Command"));


            backgroundworker1.RunWorkerAsync();


            _externalFlag = false;
            _externalFlag2 = false;
            //Thread.Sleep(5000);
            ts = new CancellationTokenSource();
            ct = ts.Token;

            Task.Factory.StartNew(() => {

                updateDots();

            }, ct);
            Task.Factory.StartNew(() => {

                updateAntenna();

            }, ct);
        }
        private void backgroundworker1_DoWork(object sender, DoWorkEventArgs e)
        {
            StartClient("192.168.2.134", 5500);//2.134", 5500); //127.0.0.1
            //LicenseCheckTask(TimeSpan.FromSeconds(5), ct);
        }

        public static string IPADDRESS;
        public string GetLocalIPAddress()
        {

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPADDRESS = ip.ToString(); ;
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public static string macAddress;
        private string GetMacAddress()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    macAddress = nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }
        public static string userName;
        public void GetActiveUser()
        {
            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
        }

        public static string licenseStatus;
        public static string[] lines = new string[3];
        public static string licenseFilePath;
        private void GetLicense()
        {
            licenseFilePath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\RunAs\license.txt");

            if (!File.Exists(licenseFilePath)) //not verified 
            {

                licenseStatus = "Deactivated";
                Console.WriteLine("No license detected.");
                lines[0] = "";
                lines[1] = "";
                lines[2] = "";


            }
            else if (File.Exists(licenseFilePath))// if file exists send license keys to the server
            {
                licenseStatus = "Activated";
                lines = System.IO.File.ReadAllLines(licenseFilePath);
            }
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                //Console.WriteLine(line);
            }
            try
            {
                //ctd.pbReconnect1.Visible = false;
                LicenseToVerify(client, lines[0] + "\n" + lines[1] + "\n" + lines[2] + "\n" + macAddress + "<verifyLicense-10s>");

                //MessageBox.Show(lines[0] + lines[1] + lines[2] + macAddress);
                if (licenseFromServer == "NotVerified")
                {
                    licenseStatus = "Deactivated";
                    
                    ctd.BackgroundImage = nullBitmap;

                }
                if (licenseFromServer == "Verified")
                {
                    licenseStatus = "Activated";

                    Image image = Resources.lActivatedBG0;
                    ctd.BackgroundImageLayout = ImageLayout.Zoom;
                    ctd.BackgroundImage = image;

                }
            }
            catch (Exception ex) //SERVER CLOSED CONNECTION
            {
                licenseFromServer = "NotVerified";
                IsConnected = false;
                licenseStatus = "Deactivated";
                ctd.BackgroundImage = nullBitmap;

                Image image = Resources.statusOfflineMini;
                ctd.pbStatus1.BackgroundImage = image;
                ctd.txtStatus1.BackColor = Color.Transparent;
                ctd.txtStatus1.Text = "Offline";

                Image image2 = Resources.retry1;
                ctd.pbReconnect1.Enabled = true;
                ctd.pbReconnect1.BackgroundImage = image2;

                return;
            }

            //}
        }

        //Task to check verify license on server - internet needed
        public async Task LicenseCheckTask(TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                //await FooAsync();
                GetLicense();

                await Task.Delay(interval, cancellationToken);
            }
        }


        public async Task ConnectLoop()
        {
            //while (true)//AsynchronousClient.serverIsReachable)
            //for (var retries = 0; retries < 5; retries++)
            //{
            for (int attempt = 0; attempt <= 5; attempt++)
            {
                try
                {
                    //tcpClient.ConnectAsync("127.0.0.1", 11000);
                    //AsynchronousClient.client.ConnectAsync()
                    StartClient("192.168.2.134", 5500); //127.0.0.1
                    await Task.Delay(5000); //Retry delay
                                            //break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Trying to connect to server");
                    continue;
                }
                if (client.Connected) break;
            }
            //}

        }

        public bool StartClient(string svrAddress, int port)
        {

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(svrAddress), port); //ipAddress

            // Create a TCP/IP socket.  
            client = new Socket(serverEndPoint.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            //IsConnected = true;

            int timeout = 5000; // int.Parse(ConfigurationManager.AppSettings["socketTimeout"].ToString());
            //int ctr = 0;
            IAsyncResult ar = client.BeginConnect(serverEndPoint, new AsyncCallback(Connect_Callback), client);
            ar.AsyncWaitHandle.WaitOne(timeout, true);



            /* while (areWeConnected == null && ctr < timeout)
             {
                 Thread.Sleep(100);
                 ctr += 100;
             }*/ // Given 100ms between checks, it allows 50 checks 
                 // for a 5 second timeout before we give up and return false, below
                 //Thread.Sleep(1000);
            connectionStateText = "Establishing connection";
            Thread.Sleep(1500);
            connectionStateText = "Connected to " + Constants.serverAddress;
            //Thread.Sleep(1000);
            if (IsConnected == true)
            {

                //waitingText.Clear();
                //waitingText.Append("Establishing connection");

                _externalFlag = true;
                _externalFlag2 = true;

                StateObject state = new StateObject();
                state.workSocket = client;

                LicenseCheckTask(TimeSpan.FromSeconds(Constants.licenseCheckAfter), ct);

                return true;
            }
            else
            {
                ts.Cancel(); //cancel license check task
                //IsConnected = false; //false
                return false;
            }

        }
        public static bool? IsConnected = false;
        private static bool? areWeConnected = null;

        private void Connect_Callback(IAsyncResult ar)
        {
            IsConnected = null;
            try
            {
                Socket socket = (Socket)ar.AsyncState;
                IsConnected = socket.Connected;
                socket.EndConnect(ar);

                //    if (_lSerialN == "AA-AA-0000")
                //        _lSerialN = "";
                //SerialN       //RKey            //VKey
                ClientInfo(client, IPADDRESS + "\n" + userName + "\n" + macAddress + "\n" + licenseStatus + "\n" + lines[0] + "\n" + lines[1] + "\n" + lines[2]);// + "<INFO>");


                StateObject state = new StateObject();
                state.workSocket = client;

                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(Receive_Callback), state);

            }
            catch (Exception ex)
            {
                IsConnected = false;
                // log exception 
            }
        }
        private static void ClientInfo(Socket client, String data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendClientInfo), client);

        }
        private static void SendClientInfo(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("ClientInfo {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                clientInfoDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void LicenseToVerify(Socket client, String data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendLicenseToVerify), client);

        }
        private static void SendLicenseToVerify(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("License data {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                //clientInfoDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        int serverSeconds;
        int serverMinutes;
        string formattedTimeSpan;
        private void TimerExecution(string _serverMessage)
        {
            string extractedTime = string.Empty;
            for (int i = 0; i < _serverMessage.Length; i++)
            {
                if (Char.IsDigit(_serverMessage[i]))
                    extractedTime += _serverMessage[i];
            }
            serverSeconds = Int32.Parse(extractedTime) * 1000;
            TimeSpan dateDifference = new TimeSpan(0, 0, 0, 0, serverSeconds);
            formattedTimeSpan = string.Format("{0:D2}h {1:D2}m {2:D2}s", dateDifference.Hours, dateDifference.Minutes, dateDifference.Seconds);

        }
        private void DelayValue(string _serverMessage)
        {
            string extractedDelay = string.Empty;
            for (int i = 0; i < _serverMessage.Length; i++)
            {
                if (Char.IsDigit(_serverMessage[i]))
                    extractedDelay += _serverMessage[i];
            }
            serverMinutes = Int32.Parse(extractedDelay);
            TimeSpan dateDifference = new TimeSpan(0, 0, 0, 0, serverMinutes);
            formattedTimeSpan = string.Format("{0:D2}h {1:D2}m {2:D2}s", dateDifference.Hours, dateDifference.Minutes, dateDifference.Seconds);

        }

        static string serverLicenseValidation;
        static string serverMessage;
        static string serverMessageFormat;
        public static Image image;
        public static CommandToDo ctd;
        public static string licenseFromServer;
        public static Task task;

        private async void Receive_Callback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    serverMessage = Encoding.UTF8.GetString(state.buffer, 0, bytesRead);


                    if (serverMessage.Contains("<licenseVerified>"))
                    {
                        serverLicenseValidation = serverMessage;
                        //serverMessage = "";

                        serverMessage = serverMessage.Remove(serverMessage.Length - 17, 17);

                        Task task = Task.Factory.StartNew(async () =>
                        {

                            licenseFromServer = "Verified";
                            image = Resources.lActivatedBG0;
                            ctd.BackgroundImageLayout = ImageLayout.Zoom;
                            ctd.BackgroundImage = image;
                            //MessageBox.Show("Verified");
                            return;
                        }, ct_remoteCommands);

                    }
                    else if (serverMessage.Contains("<licenseNotVerified>"))
                    {
                        serverLicenseValidation = serverMessage;
                        //serverMessage = "";

                        serverMessage = serverMessage.Remove(serverMessage.Length - 20, 20);

                        Task task = Task.Factory.StartNew(async () =>
                        {

                            licenseFromServer = "NotVerified";
                            ctd.BackgroundImageLayout = ImageLayout.Zoom;

                            ctd.BackgroundImage = nullBitmap;
                            //MessageBox.Show("Not verified");

                        }, ct_remoteCommands);
                    }
                    else if (serverMessage.Contains("<LogoffMachine>"))
                    {
                        try
                        {
                            ts_remoteCommands.Cancel();
                        }
                        catch
                        {

                        }
                        serverMessage = serverMessage.Remove(serverMessage.Length - 15, 15);
                        if (serverMessage.Contains("/f"))
                        {
                            RemoteCommandsFromServer.WMI(4);
                            //MessageBox.Show("forced logoff");
                        }
                        else
                        {
                            //MessageBox.Show("logoff");
                            RemoteCommandsFromServer.WMI(0);
                        }
                        serverMessage = "";
                    }
                    else if (serverMessage.Contains("<LockMachine>"))
                    {
                        ts_remoteCommands.Cancel();
                        serverMessage = serverMessage.Remove(serverMessage.Length - 13, 13);


                        RemoteCommandsFromServer.LockWorkStation();

                        serverMessage = "";

                    }
                    else if (serverMessage.Contains("<ShutdownMachine>"))
                    {
                        try
                        {
                            ts_remoteCommands.Cancel();
                        }
                        catch
                        {

                        }
                        serverMessageFormat = serverMessage.Remove(serverMessage.Length - 17, 17);
                        if (serverMessageFormat.Contains("-t"))
                        {
                            ts_remoteCommands = new CancellationTokenSource();
                            ct_remoteCommands = ts_remoteCommands.Token;
                            TimerExecution(serverMessageFormat);
                            try
                            {
                                Task task = Task.Factory.StartNew(async () =>
                                {
                                    await Task.Delay(serverSeconds, ct_remoteCommands);

                                    if (!serverMessageFormat.Contains("/f"))
                                    {

                                        RemoteCommandsFromServer.WMI(1);
                                        //MessageBox.Show("shutdown");
                                    }
                                    else
                                    {
                                        //MessageBox.Show("forced shutdown");
                                        RemoteCommandsFromServer.WMI(5);
                                    }
                                }, ct_remoteCommands);
                            }
                            catch (OperationCanceledException op)
                            {
                                MessageBox.Show(op.Message);
                            }
                            Task task2 = Task.Run(async () => MessageBox.Show("Administrator: System shutting down in: " + formattedTimeSpan, "Server Message", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                        }
                        else
                        {
                            if (!serverMessageFormat.Contains("/f"))
                            {
                                RemoteCommandsFromServer.WMI(1);
                                //MessageBox.Show("shutdown");
                            }
                            else
                            {
                                //MessageBox.Show("shutdown F");
                                RemoteCommandsFromServer.WMI(5);
                            }
                        }
                        serverMessageFormat = "";
                        serverMessage = "";



                    }
                    else if (serverMessage.Contains("<RestartMachine>"))
                    {
                        try
                        {
                            ts_remoteCommands.Cancel();
                        }
                        catch
                        {

                        }
                        serverMessageFormat = serverMessage.Remove(serverMessage.Length - 16, 16);
                        if (serverMessageFormat.Contains("-t"))
                        {
                            ts_remoteCommands = new CancellationTokenSource();
                            ct_remoteCommands = ts_remoteCommands.Token;
                            TimerExecution(serverMessageFormat);
                            try
                            {
                                Task task = Task.Factory.StartNew(async () =>
                                {
                                    await Task.Delay(serverSeconds, ct_remoteCommands);

                                    if (!serverMessageFormat.Contains("/f"))
                                    {

                                        RemoteCommandsFromServer.WMI(2);
                                        //RemoteCommandsFromServer.LockWorkStation();
                                        //MessageBox.Show("restart");
                                    }
                                    else
                                    {
                                        RemoteCommandsFromServer.WMI(6);
                                        //MessageBox.Show("forced restart");
                                    }
                                }, ct_remoteCommands);
                            }
                            catch (OperationCanceledException op)
                            {
                                MessageBox.Show(op.Message);
                            }
                            Task task2 = Task.Run(async () => MessageBox.Show("Administrator: System restarting in: " + formattedTimeSpan, "Server Message", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                        }
                        else
                        {
                            if (serverMessageFormat.Contains("/f"))
                            {
                                RemoteCommandsFromServer.WMI(6);
                                //MessageBox.Show("forced restart");
                            }
                            else
                            {
                                //MessageBox.Show("restart");
                                RemoteCommandsFromServer.WMI(2);
                            }
                        }
                        serverMessageFormat = "";
                        serverMessage = "";
                    }
                    else if (serverMessage.Contains("<StopTimedOperation>"))
                    {
                        serverMessage = serverMessage.Remove(serverMessage.Length - 20, 20);
                        if (serverMessage.Contains("-a"))
                        {
                            ts_remoteCommands.Cancel();
                            ts_remoteCommands.Dispose(); // Clean up old token source....
                            ts_remoteCommands = new CancellationTokenSource();

                            //MessageBox.Show("stopping operation.");
                            //MessageBox.Show(serverMessage);
                            //MessageBox.Show(serverMessageFormat);
                            //return;
                        }
                        serverMessage = "";
                    }
                    else if (serverMessage.Contains("<PoweroffMachine>"))
                    {
                        try
                        {
                            ts_remoteCommands.Cancel();
                        }
                        catch
                        {

                        }
                        serverMessageFormat = serverMessage.Remove(serverMessage.Length - 17, 17);
                        if (serverMessageFormat.Contains("-t"))
                        {
                            ts_remoteCommands = new CancellationTokenSource();
                            ct_remoteCommands = ts_remoteCommands.Token;
                            TimerExecution(serverMessageFormat);
                            try
                            {
                                Task task = Task.Factory.StartNew(async () =>
                                {
                                    await Task.Delay(serverSeconds, ct_remoteCommands);

                                    if (!serverMessageFormat.Contains("/f"))
                                    {

                                        RemoteCommandsFromServer.WMI(12);
                                        //MessageBox.Show("poweroff");
                                    }
                                    else
                                    {
                                        RemoteCommandsFromServer.WMI(8);
                                        //MessageBox.Show("forced poweroff");
                                    }
                                }, ct_remoteCommands);
                            }
                            catch (OperationCanceledException op)
                            {
                                MessageBox.Show(op.Message);
                            }
                            Task task2 = Task.Run(async () => MessageBox.Show("Administrator: System poweroff in: " + formattedTimeSpan, "Server Message", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                        }
                        else
                        {
                            if (!serverMessageFormat.Contains("/f"))
                            {
                                RemoteCommandsFromServer.WMI(12);
                                //MessageBox.Show("poweroff");
                            }
                            else
                            {
                                RemoteCommandsFromServer.WMI(8);
                                //MessageBox.Show("forced poweroff");
                            }
                        }
                        serverMessageFormat = "";
                        serverMessage = "";
                    }
                    else if (serverMessage.Contains("<HibernateMachine>"))
                    {
                        try
                        {
                            ts_remoteCommands.Cancel();
                        }
                        catch
                        {

                        }
                        serverMessageFormat = serverMessage.Remove(serverMessage.Length - 18, 18);
                        if (serverMessageFormat.Contains("-t"))
                        {
                            ts_remoteCommands = new CancellationTokenSource();
                            ct_remoteCommands = ts_remoteCommands.Token;
                            TimerExecution(serverMessageFormat);
                            try
                            {
                                Task task = Task.Factory.StartNew(async () =>
                                {
                                    await Task.Delay(serverSeconds, ct_remoteCommands);

                                    RemoteCommandsFromServer.DoHibernate();
                                }, ct_remoteCommands);
                            }
                            catch (OperationCanceledException op)
                            {
                                MessageBox.Show(op.Message);
                            }
                            Task task2 = Task.Run(async () => MessageBox.Show("Administrator: System set to hibernate mode in: " + formattedTimeSpan, "Server Message", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                        }
                        else
                        {
                            RemoteCommandsFromServer.DoHibernate();
                        }
                        serverMessageFormat = "";
                        serverMessage = "";
                    }
                    else if (serverMessage.Contains("<SleepMachine>"))
                    {
                        try
                        {
                            ts_remoteCommands.Cancel();
                        }
                        catch
                        {

                        }
                        serverMessageFormat = serverMessage.Remove(serverMessage.Length - 18, 18);
                        if (serverMessageFormat.Contains("-t"))
                        {
                            ts_remoteCommands = new CancellationTokenSource();
                            ct_remoteCommands = ts_remoteCommands.Token;
                            TimerExecution(serverMessageFormat);
                            try
                            {
                                Task task = Task.Factory.StartNew(async () =>
                                {
                                    await Task.Delay(serverSeconds, ct_remoteCommands);

                                    RemoteCommandsFromServer.DoSleep();
                                }, ct_remoteCommands);
                            }
                            catch (OperationCanceledException op)
                            {
                                MessageBox.Show(op.Message);
                            }
                            Task task2 = Task.Run(async () => MessageBox.Show("Administrator: System set to sleep mode in: " + formattedTimeSpan, "Server Message", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                        }
                        else
                        {
                            RemoteCommandsFromServer.DoSleep();
                        }
                        serverMessageFormat = "";
                        serverMessage = "";
                    }
                    else if (serverMessage.Contains("</ras -shutdown -c>"))
                    {
                        serverMessage = "";

                        //serverMessageFormat = serverMessage.Remove(serverMessage.Length - 20, 20);
                        this.Invoke(new MethodInvoker(() =>
                        {

                            ts.Cancel();
                            MessageBox.Show("You have been shutdowned by the server.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Application.Exit();
                        }));
                    }
                    else if (serverMessage.Contains("</ras -restart -c>"))
                    {
                        serverMessage = "";

                        //serverMessageFormat = serverMessage.Remove(serverMessage.Length - 20, 20);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            ts.Cancel();
                            Application.Restart();
                            Environment.Exit(0);

                        }));
                    }
                    else if (serverMessage.Contains("</ras -disconnect -c>"))
                    {
                        //serverMessage = "";

                        serverMessage = serverMessage.Remove(serverMessage.Length - 21, 21);
                        this.Invoke(new MethodInvoker(() =>
                        {
                            //ts.Cancel();
                            client.Shutdown(SocketShutdown.Both);
                            client.Close();

                        }));
                    }
                    else if (serverMessage.Contains("<MESSAGE>"))
                    {

                        serverMessage = serverMessage.Remove(serverMessage.Length - 9, 9);
                        //ts_remoteCommands.Cancel();
                        MessageBox.Show("Administrator: " + serverMessage, "Server Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        /*if(dialogResult == DialogResult.OK)
                        {
                            SendKeys.SendWait("{Enter}");//or Esc
                        }*/
                        serverMessage = "";

                    }
                    else if (serverMessage.Contains("<ExecutePrompt>"))
                    {

                        serverMessage = serverMessage.Remove(serverMessage.Length - 15, 15);

                        //MessageBox.Show(serverMessage);

                        ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd.exe");
                        procStartInfo.UseShellExecute = false;
                        procStartInfo.CreateNoWindow = true;
                        procStartInfo.Verb = "runas";
                        procStartInfo.Arguments = "/c" + serverMessage;//"cmd /K \"net user ABC Admin123# /add";

                        System.Diagnostics.Process proc = new System.Diagnostics.Process();
                        proc.StartInfo = procStartInfo;
                        proc.Start();


                        serverMessage = "";

                    }
                    else if (serverMessage.Contains("<AC_MonitorTimeout>"))
                    {
                        serverMessage = serverMessage.Remove(serverMessage.Length - 18, 18);
                        DelayValue(serverMessage);
                        RemoteCommandsFromServer.SetClientTimeout(RemoteCommandsFromServer.AC_monitor_timeout, serverMinutes, null);
                        serverMessage = "";

                    }
                    else if (serverMessage.Contains("<DC_MonitorTimeout>"))
                    {
                        serverMessage = serverMessage.Remove(serverMessage.Length - 18, 18);
                        DelayValue(serverMessage);
                        RemoteCommandsFromServer.SetClientTimeout(RemoteCommandsFromServer.DC_monitor_timeout, serverMinutes, null);
                        serverMessage = "";

                    }
                    else if (serverMessage.Contains("<AC_SleepTimeout>"))
                    {

                        serverMessage = serverMessage.Remove(serverMessage.Length - 17, 17);
                        DelayValue(serverMessage);

                        RemoteCommandsFromServer.SetClientTimeout(RemoteCommandsFromServer.AC_sleep_timeout, serverMinutes, null);


                        serverMessage = "";

                    }
                    else if (serverMessage.Contains("<DC_SleepTimeout>"))
                    {
                        serverMessage = serverMessage.Remove(serverMessage.Length - 17, 17);
                        DelayValue(serverMessage);

                        RemoteCommandsFromServer.SetClientTimeout(RemoteCommandsFromServer.DC_sleep_timeout, serverMinutes * 10, null);
                        serverMessage = "";

                    }
                    else if (serverMessage.Contains("<AC_HibernateTimeout>"))
                    {
                        serverMessage = serverMessage.Remove(serverMessage.Length - 21, 21);
                        DelayValue(serverMessage);
                        RemoteCommandsFromServer.SetClientTimeout(RemoteCommandsFromServer.AC_hibernate_timeout, serverMinutes, null);
                        serverMessage = "";

                    }
                    else if (serverMessage.Contains("<DC_HibernateTimeout>"))
                    {
                        serverMessage = serverMessage.Remove(serverMessage.Length - 21, 21);
                        DelayValue(serverMessage);
                        RemoteCommandsFromServer.SetClientTimeout(RemoteCommandsFromServer.DC_hibernate_timeout, serverMinutes, null);
                        serverMessage = "";

                    }
                    else if (serverMessage.Contains("<SetTaskSchedulerRule>"))
                    {
                        serverMessage = serverMessage.Remove(serverMessage.Length - 22, 22);

                        string[] arrayTaskScheduleItem = serverMessage.Split( //po každém enteru vytvoří novou string proměnou
                                new string[] { "\r\n", "\r", "\n" },
                                StringSplitOptions.None
                         );
                        string tsTaskName = arrayTaskScheduleItem[0];
                        string tsTaskDesc = arrayTaskScheduleItem[1];
                        string tsTaskProgScript = arrayTaskScheduleItem[2];
                        string tsTaskArgs = arrayTaskScheduleItem[3];

                        string tsTaskStartDate = arrayTaskScheduleItem[4];
                        string tsTaskStartTime = arrayTaskScheduleItem[5];
                        string tsTaskEndDate = arrayTaskScheduleItem[6];
                        string tsTaskEndTime = arrayTaskScheduleItem[7];

                        short tsTaskRepeatNumber = Int16.Parse(arrayTaskScheduleItem[8]);
                        string tsTaskRepeatOption = arrayTaskScheduleItem[9];

           //FUNKČNÍ POUZE U SLUŽBY (VYŽADOVÁNA ZVÝŠENÁ OPRÁVNĚNÍ)
                        /*using (Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService())
                        {
                            // Create a new task definition and assign properties
                            Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                            td.Principal.LogonType = Microsoft.Win32.TaskScheduler.TaskLogonType.InteractiveToken;
                            td.RegistrationInfo.Description = tsTaskDesc;

                            // Create a trigger that will fire the task at this time every other day
                            if (tsTaskRepeatOption == "Days")
                            {
                                td.Triggers.Add(new Microsoft.Win32.TaskScheduler.DailyTrigger { DaysInterval = tsTaskRepeatNumber });
                            }
                            else if (tsTaskRepeatOption == "Weeks")
                            {
                                td.Triggers.Add(new Microsoft.Win32.TaskScheduler.WeeklyTrigger { WeeksInterval = tsTaskRepeatNumber });
                            }/*else if (tsTaskRepeatOption == "Months")
                            {
                                td.Triggers.Add(new Microsoft.Win32.TaskScheduler.MonthlyTrigger { MonthsOfYear = tsTaskRepeatNumber });
                            }

                            // Create an action that will launch Notepad whenever the trigger fires
                            td.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(tsTaskProgScript, tsTaskArgs, null));

                            // Register the task in the root folder
                            ts.RootFolder.RegisterTaskDefinition(tsTaskName, td);

                            // Remove the task we just created
                            //ts.RootFolder.DeleteTask("Test");
                        }*/



                        MessageBox.Show(tsTaskName + tsTaskDesc + tsTaskProgScript + tsTaskArgs + tsTaskStartDate +
                             tsTaskEndDate + tsTaskRepeatNumber);
                        serverMessage = "";



                    }




                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(Receive_Callback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.sb.Length > 1)
                    {

                        MessageBox.Show(state.sb.ToString());
                    }
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
                //client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                //        new AsyncCallback(Receive_Callback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static string connectionStateText;
        private static bool _externalFlag = false;
        private static bool _externalFlag2 = false;
        static StringBuilder waitingText;

        private void updateDots()
        {


            int count = 0;
            waitingText = new StringBuilder();
            connectionStateText = "Connecting to " + Constants.serverAddress;
            waitingText.Append(connectionStateText);
            int baseLen = waitingText.Length;

            while (!_externalFlag)
            {
                Thread.Sleep(600); // time between adding dots
                waitingText.Append(".");
                count++;

                if (ct.IsCancellationRequested)
                {
                    // another thread decided to cancel
                    Console.WriteLine("task canceled");
                    break;
                }

                if (count >= 4) // number of dots
                {

                    waitingText.Remove(baseLen, count);
                    count = 0;

                }

                BeginInvoke(new Action(() => { updateText(waitingText.ToString()); }));

            }
            ctd = new CommandToDo();
            if (IsConnected == true)
            {
                BeginInvoke(new Action(() =>
                {
                    //IsConnected = false;
                    //updateText("done"); 
                    //Thread.Sleep(1000);

                    Image image = Resources.statusOnlineMini;
                    ctd.pbStatus1.BackgroundImage = image;
                    ctd.txtStatus1.BackColor = Color.Transparent;
                    ctd.txtStatus1.Text = "Online";

                    Image image2 = Resources.retry1;
                    ctd.pbReconnect1.Enabled = false;
                    ctd.pbReconnect1.BackgroundImage = null;


                    ctd.Show();
                    this.Hide();

                }));
            }
            else
            {
                BeginInvoke(new Action(() =>
                {
                    //updateText("done"); 
                    //Thread.Sleep(1000);

                    Image image = Resources.statusOfflineMini;
                    ctd.pbStatus1.BackgroundImage = image;
                    ctd.txtStatus1.BackColor = Color.Transparent;
                    ctd.txtStatus1.Text = "Offline";

                    Image image2 = Resources.retry1;
                    ctd.pbReconnect1.Enabled = true;
                    ctd.pbReconnect1.BackgroundImage = image2;


                    ctd.Show();
                    this.Hide();

                }));
            }

        }

        private void updateAntenna()
        {
            int count = 0;


            while (!_externalFlag2)
            {
                Thread.Sleep(200); // time between updating antena signal
                count++;

                if (ct.IsCancellationRequested)
                {
                    // another thread decided to cancel
                    Console.WriteLine("task canceled");
                    break;
                }
                if (count == 1)
                {
                    Image image = Resources.antenna_0;
                    pictureBox2.BackgroundImage = image;
                }
                else if (count == 2)
                {
                    Image image = Resources.antenna_1;
                    pictureBox2.BackgroundImage = image;
                }
                if (count == 3)
                {
                    Image image = Resources.antenna_2;
                    pictureBox2.BackgroundImage = image;
                }
                else if (count == 4)
                {
                    Image image = Resources.antenna_3;
                    pictureBox2.BackgroundImage = image;
                }
                else if (count >= 5)
                {
                    Image image = Resources.antenna_0;
                    pictureBox2.BackgroundImage = image;
                    count = 0;
                }

                //BeginInvoke(new Action(() => { updateText(waitingText.ToString()); }));
            }
            /*BeginInvoke(new Action(() => {

                //updateText("done"); 
                
                CommandToDo ctd = new CommandToDo();
                ctd.Show();
                this.Close();

            }));*/
        }

        public void updateText(string txt)
        {
            label1.Text = txt;
        }

    }
}
