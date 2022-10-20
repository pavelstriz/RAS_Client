using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Management;
using RunAsAdminTool.Properties;
using System.Threading;

namespace RunAsAdminTool
{
    public partial class CommandToDo : Form
    {
        public CommandToDo()
        {
            InitializeComponent();
        }
        DialogResult dialogResult;
        string windowsVersion;


        Process p = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();

        Process p2 = new Process();
        ProcessStartInfo startInfo2 = new ProcessStartInfo();
        private void CommandToDo_Load(object sender, EventArgs e)
        {
            

            /*if(Loading1.client.Connected)
            {
                Properties.Resources.statusOffline.
                Icon icon = Properties.Resources.statusOffline;
                this.Icon = Properties.Resources.statusOnlineMini;
            }
            else
            {
                this.Icon = Properties.Resources.statusOfflineMini;
            }*/


            DateCheck checkDate = new DateCheck();
            checkDate.Start();
            
            GetWindowsVersion();
            GetLicense();
            string _s;

            _s = Constants.expirationDate.ToShortDateString();

            _s = _s.Replace(".", string.Empty); // vymaze/prepise vsechny tecky
            _s = _s.Insert(1, "."); //přidá po 1 cislu tecku
            _s = _s.Substring(0, _s.Length - 3); //vymaze posledni 3 cisla

            this.Text = "ver.: " + _s;


            //Image image = Resources.statusOnlineMini;
            //pbStatus1.BackgroundImage = image;
            //pbReconnect1.Visible = false;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();

            /*if (string.Equals((sender as Button).Name, @"CloseButton"))
            {
                // Do something proper to CloseButton.

            }
            else
            {
                // Then assume that X has been clicked and act accordingly.
            }*/
        }
        public static string licenseFilePath;
        public static string[] lines = new string[3];
        
        public void GetLicense()
        {


            licenseFilePath = Environment.ExpandEnvironmentVariables(@"C:\Temp\RunAs\license.txt");

            if (!File.Exists(licenseFilePath))
            {

                Console.WriteLine("No license detected.");

                lines[0] = "";
                lines[1] = "";
                lines[2] = "";

                
            }
            else
            {

                lines = System.IO.File.ReadAllLines(licenseFilePath);

                // Display the file contents by using a foreach loop.
                //System.Console.WriteLine("Contents of WriteLines2.txt = ");
                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file.
                    Console.WriteLine(line);
                }

                //licenseStatus = "Activated";
                Console.WriteLine("Active license detected.");


            }
            //MessageBox.Show(licenseStatus);
        }
        public Loading1 _loading1;
        private void btnRunAsAdmin_Click(object sender, EventArgs e)
        {
            GetLicense();
            //MessageBox.Show("test");
            /////MessageBox.Show(DateTime.Now.ToShortDateString());
            //MessageBox.Show(Constants.expirationDate.ToString());
            // MessageBox.Show(Constants.sOnlineDate.ToShortDateString());

            //RunAsAdmin f1 = new RunAsAdmin();
            //f1.ShowDialog(); // Shows Form2

            _loading1 = new Loading1();

            //RunAsAdmin raa1 = new RunAsAdmin();
            //raa1.ShowDialog();


            txtStatus1.BackColor = Color.Transparent;
            if (Loading1.licenseFromServer == "NotVerified" || Loading1.licenseFromServer == null)
            {
                LicenseForm1 lf1 = new LicenseForm1(this);
                lf1.ShowDialog();
            }
            else if (Loading1.licenseFromServer == "Verified")
            {
                RunAsAdmin raa1 = new RunAsAdmin();
                raa1.ShowDialog();
            }
        }
        private void ExecuteCommand(string command1, string command2, string command3, string command4, string command5, string command6, string command7, string command8, string command9)
        {
            
            string batchFile = Environment.ExpandEnvironmentVariables(@"C:\Temp\RunAs\temp.bat");

            if (File.Exists(batchFile))
            {
                File.Delete(batchFile);
                
            }
            else
            {
                using (StreamWriter sw = File.CreateText(batchFile))
                {
                    sw.WriteLine("@echo");
                    sw.WriteLine(command1);
                    sw.WriteLine(command2);
                    sw.WriteLine(command3);
                    sw.WriteLine(command4);
                    sw.WriteLine(command5);
                    sw.WriteLine(command6);
                    sw.WriteLine(command7);
                    sw.WriteLine(command8);
                    sw.WriteLine(command9);

                }


                IWshRuntimeLibrary.WshShell wsh = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(Environment.ExpandEnvironmentVariables(@"C:\Temp\RunAs")
                    + "\\temp.lnk") as IWshRuntimeLibrary.IWshShortcut;
                //shortcut.Arguments = "c:\\app\\settings1.xml";
                shortcut.TargetPath = batchFile;
                // not sure about what this is for
                shortcut.WindowStyle = 1;
                shortcut.Description = "temp";
                shortcut.WorkingDirectory = Environment.ExpandEnvironmentVariables(@"C:\Temp\RunAs");
                //shortcut.IconLocation = "specify icon location";
                shortcut.Save();


                string batchFileShortcut = Environment.ExpandEnvironmentVariables(@"C:\Temp\RunAs\temp.lnk");
                Thread.Sleep(1500);

                Constants.pass = new SecureString();
                Constants.pass.AppendChar('a');
                Constants.pass.AppendChar('d');
                Constants.pass.AppendChar('m');
                Constants.pass.AppendChar('i');
                Constants.pass.AppendChar('n');
                //Constants.pass.AppendChar('char');
                //Constants.pass.AppendChar('char');
                //Constants.pass.AppendChar('..');


                startInfo.FileName = batchFile;
                startInfo.UserName = Constants.userName;
                startInfo.Password = Constants.pass;
                startInfo.UseShellExecute = false;
                startInfo.Verb = "runas";
                startInfo.RedirectStandardOutput = true;
                startInfo.CreateNoWindow = true;
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.StartInfo = startInfo;
                p.Start();
                pass.Dispose();
                Thread.Sleep(1000);

               


                File.Delete(batchFile);
                File.Delete(batchFileShortcut);
            }
        }
        private void GetWindowsVersion()
        {
            windowsVersion = "";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection information = searcher.Get();
                if (information != null)
                {
                    foreach (ManagementObject obj in information)
                    {
                        windowsVersion = obj["Caption"].ToString() + " - " + obj["OSArchitecture"].ToString();
                    }
                }
                windowsVersion = windowsVersion.Replace("NT 5.1.2600", "XP");
                windowsVersion = windowsVersion.Replace("NT 5.2.3790", "Server 2003");


                
            }
            // if(OSInfo.Name == )
            // if ()
        }
       
        public string command1;
        public string command2;
        public string command3;
        public string command4;
        public string executeBatPath;

        private void btnAllowRDP_Click(object sender, EventArgs e)
        {
            executeBatPath = Environment.ExpandEnvironmentVariables(@"C:\Temp\RunAs\ARD.bat");

            dialogResult = MessageBox.Show("Do you want to open Remote Desktop on this machine?", "Question", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                if (windowsVersion.Contains("Microsoft Windows XP"))
                {
                    command1 = "netsh advfirewall firewall delete rule name = \"_Allow_Remote_Desktop";
                    command2 = "netsh firewall set portopening protocol = TCP port = 3389 name = \"_Allow_Remote_Desktop\" mode = ENABLE";
                }
                else
                {

                    command1 = "netsh advfirewall firewall delete rule name = \"_Allow_Remote_Desktop";
                    command2 = "netsh advfirewall firewall delete rule name = \"_Block_Remote_Desktop";
                    command3 = "netsh advfirewall firewall add rule name=\"_Allow_Remote_Desktop\" protocol=TCP dir=in localport=3389 action=allow";
                    command4 = "net start TermService";

                    // MessageBox.Show("This action doesn't work in: $windowsVersion", "OS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ExecuteCommand(command1, command2, command3, command4, null, null, null, null, null);

            }
            else if (dialogResult == DialogResult.No)
            {
               /* if (windowsVersion.Contains("Microsoft Windows XP"))
                {
                    command1 = "netsh advfirewall firewall delete rule name = \"_Allow_Remote_Desktop";
                    command2 = "netsh firewall set portopening protocol = TCP port = 3389 name = \"_Allow_Remote_Desktop\" mode = DISABLE";
                }
                else
                {

                    command1 = "netsh advfirewall firewall delete rule name = \"_Allow_Remote_Desktop";
                    command2 = "netsh advfirewall firewall delete rule name = \"_Block_Remote_Desktop";
                    command3 = "netsh advfirewall firewall add rule name=\"_Allow_Remote_Desktop\" protocol=TCP dir=in localport=3389 action=block";
                    command4 = "net start TermService";

                    // MessageBox.Show("This action doesn't work in: $windowsVersion", "OS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               */
            }
        }

        private void btnAllowPings_Click(object sender, EventArgs e)
        {
            //executeBatPath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\RunAs\AP.bat");
            Directory.CreateDirectory(@"C:\Temp");
            Directory.CreateDirectory(@"C:\Temp\RunAs");

            


            dialogResult = MessageBox.Show("Do you want to allow pings on this machine?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                string command1 = "netsh advfirewall firewall delete rule name = \"_Block_Pings";
                string command2 = "netsh advfirewall firewall delete rule name = \"_Allow_Pings";
                string command3 = "netsh advfirewall firewall delete rule name = \"_Povolit_ping";
                string command4 = "netsh advfirewall firewall delete rule name = \"_Zakazat_ping";
                string command5 = "netsh advfirewall firewall add rule name = \"_Allow_Pings\" protocol = icmpv4:8,any dir =in action = allow";


                ExecuteCommand(command1, command2, command3, command4, command5, null, null, null, null);
            }
            else if (dialogResult == DialogResult.No)
            {
                string command1 = "netsh advfirewall firewall delete rule name = \"_Allow_Pings";
                string command2 = "netsh advfirewall firewall delete rule name = \"_Block_Pings";
                string command3 = "netsh advfirewall firewall delete rule name = \"_Povolit_ping";
                string command4 = "netsh advfirewall firewall delete rule name = \"_Zakazat_ping";
                string command5 = "netsh advfirewall firewall add rule name = \"_Block_Pings\" protocol = icmpv4:8,any dir =in action = block";


                ExecuteCommand(command1, command2, command3, command4, command5, null, null, null, null);
            }
        }

        private void btnRestoreDefault_Click(object sender, EventArgs e)
        {
            string command1 = "netsh advfirewall firewall delete rule name = \"_Povolit_ping";
            string command2 = "netsh advfirewall firewall delete rule name = \"_Zakazat_ping";

            string command3 = "netsh advfirewall firewall delete rule name = \"_Allow_Pings";
            string command4 = "netsh advfirewall firewall delete rule name = \"_Allow_Remote_Desktop";
            string command5 = "netsh advfirewall firewall delete rule name = \"_Block_Pings";
            string command6 = "netsh advfirewall firewall delete rule name = \"_Block_Remote_Desktop";

            string command7 = "net start TermService";



            dialogResult = MessageBox.Show("Do you want restore all settings?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                ExecuteCommand(command1, command2, command3, command4, command5, command6, command7, null, null);

            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }
        int count = 0;

        public void wait(int serverSeconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (serverSeconds == 0 || serverSeconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = serverSeconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
        private void pbInvisiblePC_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void pbInvisiblePC_MouseDown(object sender, MouseEventArgs e)
        {
            count++;

            string command1 = "netsh advfirewall firewall delete rule name = \"_Allow_Pings";
            string command2 = "netsh advfirewall firewall delete rule name = \"_Allow_Remote_Desktop";
            string command3 = "netsh advfirewall firewall delete rule name = \"_Block_Pings";
            string command4 = "netsh advfirewall firewall delete rule name = \"_Block_Remote_Desktop";
            string command5 = "netsh advfirewall firewall add rule name = \"_Block_Pings\" protocol = icmpv4:8,any dir =in action = block";
            string command6 = "netsh advfirewall firewall add rule name=\"_Block_Remote_Desktop\" protocol=TCP dir=in localport=3389 action=block";
            string command7 = "net stop TermService";

            if (count == 1)
            {
                Image image = Resources.invisiblePC40Icon;
                pbInvisiblePC.BackgroundImage = image;
            }
            else if (count == 2)
            {
                Image image = Resources.invisiblePC80Icon;
                pbInvisiblePC.BackgroundImage = image;
            }
            if (count == 3)
            {
                Image image = Resources.invisiblePC120Icon;
                pbInvisiblePC.BackgroundImage = image;
            }
            else if (count == 4)
            {
                Image image = Resources.invisiblePC160Icon;
                pbInvisiblePC.BackgroundImage = image;
            }
            else if (count == 5)
            {
                Image image = Resources.invisiblePC200Icon;
                pbInvisiblePC.BackgroundImage = image;

                dialogResult = MessageBox.Show("Do you want make this machine invisible?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    /*try
                    {
                        service.StopService("TermService", 5000);
                    }
                    catch
                    {

                    }*/

                    /*
                    ExecuteCommand(command1);
                    ExecuteCommand(command2);
                    ExecuteCommand(command3);
                    ExecuteCommand(command4);
                    ExecuteCommand(command5);
                    ExecuteCommand(command6);
                    */

                    /// <summary>
                    /// Write your Administrator account password char by char
                    /// </summary>
                    Constants.pass = new SecureString();
                    Constants.pass.AppendChar('a');
                    Constants.pass.AppendChar('d');
                    Constants.pass.AppendChar('m');
                    Constants.pass.AppendChar('i');
                    Constants.pass.AppendChar('n');
                    //Constants.pass.AppendChar('char');
                    //Constants.pass.AppendChar('char');
                    //Constants.pass.AppendChar('..');




                    try
                    {
                        startInfo2.UserName = Constants.userName;
                        startInfo2.Password = Constants.pass;
                        startInfo2.UseShellExecute = false;
                        startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo2.CreateNoWindow = true;
                        startInfo2.FileName = "cmd.exe";
                        startInfo2.Arguments = "/user:Administrator \"cmd /K net stop TermService\"";
                        startInfo2.Verb = "runas";
                        //startInfo2.RedirectStandardOutput = true;
                        startInfo2.RedirectStandardInput = true;

                        p2.StartInfo = startInfo2;
                        p2.Start();
                        wait(5000);

                        p2.StandardInput.WriteLine("A");
                        //p2.WaitForExit();
                        // Console.WriteLine(p2);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    //pass.Dispose();
                    //ExecuteCommand(command7);

                    count = 0;

                    image = Resources.invisiblePCIcon;
                    pbInvisiblePC.BackgroundImage = image;


                }
                else if (dialogResult == DialogResult.No)
                {
                    count = 0;

                    image = Resources.invisiblePCIcon;
                    pbInvisiblePC.BackgroundImage = image;
                }
            }
        }

        
        private void pbReconnect1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //Reconnect();
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
