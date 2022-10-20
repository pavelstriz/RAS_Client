using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RunAsAdminTool
{
    public partial class RunAsAdmin : Form
    {
        public RunAsAdmin()
        {
            InitializeComponent();

            foreach (Control contr in this.Controls)
            {
                if (contr is Panel)
                {

                }
            }

        }
        Thread t;
        private string selectedPath = "C:\\Windows\\System32\\";
        private string FileName = "cmd.exe";
        bool IsOpenFileDialog = false;
        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.InitialDirectory = selectedPath;
            openFileDialog1.FileName = FileName;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedPath = openFileDialog1.InitialDirectory;
                FileName = openFileDialog1.FileName;
                try
                {
                    txtSelectedPath.Text = FileName;
                    
                    
                }
                catch
                {
                    MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
                }
            }
            /*if (File.Exists(combinedDir)) // if there is a file with that name at that directory
            {
                openFileDialog1.InitialDirectory = selectedPath; // setting directory name
                openFileDialog1.FileName = FileName; // filename
                BeginInvoke((Action)(() => openFileDialog1.ShowDialog())); // we need to use BeginInvoke to continue to the following code.
                t = new Thread(new ThreadStart(SendKey)); // Sends Key to Dialog with an seperate Thread.
                t.Start(); // Thread starts.

                

            }
            else // if there is not file with that name works here because no keys need to send.
            {
                openFileDialog1.InitialDirectory = selectedPath;
                openFileDialog1.FileName = FileName;
                openFileDialog1.ShowDialog();
            }*/


        }
        private void SendKey()
        {
            /*Thread.Sleep(100); // Wait for the Dialog shown at the screen
            SendKeys.SendWait("+{TAB}"); // First Shift + Tab moves to Header of DataGridView of OpenFileDialog
            SendKeys.SendWait("+{TAB}"); // Second Shift + Tab moves to first item of list
            SendKeys.SendWait(FileName); // after sending filename will directly moves it to the file that we are looking for
        */}


        private void btnRunAsAdmin_Click(object sender, EventArgs e)
        {
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

            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();

            if (txtSelectedPath.Text == "C:\\Windows\\System32\\cmd.exe")
            {
                try
                {
                    ProcessStartInfo p1 = new ProcessStartInfo();
                    p1.UseShellExecute = true;
                    p1.Verb = "runas";
                    p1.FileName = "cmd.exe";
                    Process.Start(p1);
                }
                catch (Win32Exception ex)
                {
                    if (ex.NativeErrorCode == Constants.ERROR_CANCELED)
                        MessageBox.Show("Operation canceled.", "Error " + ex.NativeErrorCode.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        throw;
                        
                }
            }
            else
            {

                //string command = "netsh advfirewall firewall add rule name=\"Open Remote Desktop\" protocol=TCP dir=in localport=3389 action=allow";
                startInfo.FileName = txtSelectedPath.Text;
                //startInfo.Arguments = "/user:Administrator \"cmd /K " + command + "\"";
                startInfo.UserName = Constants.userName;
                startInfo.Password = Constants.pass;
                startInfo.UseShellExecute = false;
                startInfo.Verb = "runas";
                startInfo.RedirectStandardOutput = true;
                p.StartInfo = startInfo;
                p.Start();
                Constants.pass.Dispose();
                
            }
        }



      
    }
}
