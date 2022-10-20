using RunAsAdminTool.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunAsAdminTool
{
    public partial class LicenseForm1 : Form
    {
        private CommandToDo _masterForm;
        public LicenseForm1(CommandToDo masterForm)
        {
            InitializeComponent();
            _masterForm = masterForm;
        }

        private void LicenseForm1_Load(object sender, EventArgs e)
        {
            txtSerialN.Text = "AA-AA-0000";
            txtSerialN.ForeColor = Color.Silver;

            GetLocalIPAddress();
            GetActiveUser();
            GetMacAddress();
            //GetLicense();
            //MessageBox.Show(userName);
            //try
            //{
            /*   client = new Socket(SocketType.Stream, ProtocolType.Tcp);

               client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);


               ipAddress = IPAddress.Parse("127.0.0.1");//((IPEndPoint)tcpclient.Client.RemoteEndPoint).Address;
               remoteEP = new IPEndPoint(ipAddress, 5500);


               client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);

               connectDone.WaitOne();
            */


            /*}
            catch
            {

            }*/

        }

        public string IPADDRESS;



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
        /*private string GetMacAddress() ***Returns every network adapter
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = string.Empty;
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                MessageBox.Show("Found MAC Address: " + nic.GetPhysicalAddress() +" Type: " + nic.NetworkInterfaceType);

                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed &&
                    !string.IsNullOrEmpty(tempMac) &&
                    tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                    MessageBox.Show("New Max Speed = " + nic.Speed + ", MAC: " + tempMac);
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }

            return macAddress;
        }*/
        string macAddress;
        private string GetMacAddress()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                //MessageBox.Show("Found MAC Address: " + nic.GetPhysicalAddress());// + " Type: " + nic.NetworkInterfaceType);
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    macAddress = nic.GetPhysicalAddress().ToString();
                    break;
                }
            }


            return macAddresses;
        }
        public string licenseStatus;
        string[] lines = new string[3];
       /*private void GetLicense()
        {


            licenseFilePath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\RunAs\license.txt");

            if (!File.Exists(licenseFilePath))
            {
                licenseStatus = "Deactivated";
                Console.WriteLine("No license detected.");

                lines[0] = "";
                lines[1] = "";
                lines[2] = "";


            }
            else
            {

                lines = System.IO.File.ReadAllLines(licenseFilePath);

                // Display the file contents by using a foreach loop.
                System.Console.WriteLine("Contents of WriteLines2.txt = ");
                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file.
                    Console.WriteLine(line);
                }

                licenseStatus = "Activated";
                Console.WriteLine("Active license detected.");


            }
        }*/

        string userName;
        public void GetActiveUser()
        {
            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
        }
        /*public void GetLicenseKey()  //AUTOMATIC DETECTION
        {

        }*/



        Thread thread;
        
        public static string _lSerialN;
        
        private string license;
        private string regKey;
        private string licenseNoFormat;


        private void moveToEnd()
        {

            txtSerialN.Select(txtSerialN.Text.Length, 0);

            txtSerialN.Focus();

            txtSerialN.ScrollToCaret();
            return;
        }
        private bool Added = false;
        private bool Added2 = false;
        private void txtSerialN_TextChanged(object sender, EventArgs e)
        {
            regKey = txtSerialN.Text;

            if (regKey.Length == 3 && Added == false)
            {

                regKey = regKey.Insert(2, "-");
                txtSerialN.Text = regKey;
                moveToEnd();
                Added = true;

            }
            if (regKey.Length == 6 && Added2 == false)
            {

                regKey = regKey.Insert(5, "-");
                txtSerialN.Text = regKey;
                moveToEnd();
                Added2 = true;
            }
            if(regKey.Length == 10 && Added2 == true)
            {
                licenseNoFormat = txtSerialN.Text + macAddress + "AXaG#99u";
                GenerateKey gKey = new GenerateKey();
                license = gKey.FormatLicenseKey(gKey.GetMd5Sum(macAddress));
                txtRegKey.Text = license;

            }




        }
        private void txtSerialN_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                Console.WriteLine("testttt");

                //e.SuppressKeyPress = true;
                if (regKey.Length == 3)
                {
                    Added = false;
                }
                if (regKey.Length == 6)
                {
                    Added2 = false;
                }
            }
        }

        private void txtSerialN_Enter(object sender, EventArgs e)
        {
            if (txtSerialN.ForeColor == Color.Silver)
            {
                txtSerialN.ForeColor = Color.Black;
                txtSerialN.Text = String.Empty;
            }

        }

        private void txtSerialN_Leave(object sender, EventArgs e)
        {
            Added = false;
            Added2 = false;
            if (regKey.Length == 0)
            {
                txtSerialN.ForeColor = Color.Silver;
                txtSerialN.Text = "AA-AA-AAAA";
            }
        }
        //public static string licenseFilePath;

        public static string _txtSerialN;
        public static string _txtRegKey;
        public static string _txtVerKey;
        public static string _licenseStatus;
        public static bool licenseSent = false;

        private void btnVerify_Click(object sender, EventArgs e)
        {
            this.Close();

            //CommandToDo ctd = new CommandToDo();
            //ctd.GetLicense();
            //((PictureBox)sender).Visible = false;



            //GetLicense();

            //if(serverVerify == true
            //{
            _lSerialN = txtSerialN.Text;
            //}
            //licenseFilePath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\RunAs\license.txt");

            if (!File.Exists(CommandToDo.licenseFilePath))
            {
                //Image image = Resources.lActivatedBG0;
                //_masterForm.BackgroundImageLayout = ImageLayout.Zoom; //Command to do image
                //_masterForm.BackgroundImage = image;

                VerifySend(Loading1.client, txtSerialN.Text + "\n" + txtRegKey.Text + "\n" + txtVerKey.Text + "\n" + licenseStatus + "<LICENSE2>");
                /*_txtSerialN = txtSerialN.Text;
                _txtRegKey = txtRegKey.Text;
                _txtVerKey = txtVerKey.Text;
                _licenseStatus = licenseStatus;
                */
                //MessageBox.Show("You have successfully activated license.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string filePath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Documents\RunAs\license.txt");
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(txtSerialN.Text);
                    sw.WriteLine(txtRegKey.Text);
                    sw.WriteLine(txtVerKey.Text);
                }


            }
            else
            {
                
                //MessageBox.Show("File license.txt already exists.");

                using (StreamReader sr = File.OpenText(CommandToDo.licenseFilePath))
                {
                    string[] lines = File.ReadAllLines(CommandToDo.licenseFilePath);
                    bool isMatch = false;
                    for (int x = 0; x <= lines.Length - 2; x++)
                    {
                        if (txtVerKey.Text == lines[x])
                        {
                            sr.Close();
                            MessageBox.Show("The license is already activated on this machine.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            isMatch = true;
                        }
                    }
                    if (!isMatch)
                    {
                        sr.Close();
                        MessageBox.Show("You have already activated this product.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }


                //SEND EXISTING KEY TO SERVER AND VERIFY
            }

        }
        private void VerifySend(Socket client, String data)
        {
            if (client != null)
            {
                try
                {
                    // Convert the string data to byte data using ASCII encoding.  

                    byte[] bytesToSend = Encoding.UTF8.GetBytes(data);

                    // Begin sending the data to the remote device.  


                    client.Send(bytesToSend);
                    //int bytesSent = client.EndSend(ar);
                    Console.WriteLine("Sent {0} bytes to server. (verify)", bytesToSend.Length);


                    // Begin sending the data toi the remote device.  
                    //client.BeginSend(byteData, 0, byteData.Length, 0,
                    //    new AsyncCallback(VerifyCallback), client);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Connection to the server timed out.");
                }
            }

        }
        private static void VerifyCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server (verify).", bytesSent);

                // Signal that all bytes have been sent.  
                //verifyDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine("error");
                Console.WriteLine(e.ToString());
            }
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.storno_license1_1_dark; 
        }


        private void btnVerify_MouseEnter(object sender, EventArgs e)
        {
            btnVerify.BackgroundImage = Properties.Resources.verify_license1_2_dark; 
        }



        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.storno_license1_1;
        }

        private void btnVerify_MouseLeave(object sender, EventArgs e)
        {
            btnVerify.BackgroundImage = Properties.Resources.verify_license1_2;
        }

        private void LicenseForm1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}