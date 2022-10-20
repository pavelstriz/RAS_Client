namespace RunAsAdminTool
{
    partial class CommandToDo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommandToDo));
            this.btnRunAsAdmin = new System.Windows.Forms.Button();
            this.btnAllowPings = new System.Windows.Forms.Button();
            this.btnAllowRDP = new System.Windows.Forms.Button();
            this.btnRestoreDefault = new System.Windows.Forms.Button();
            this.pbReconnect1 = new System.Windows.Forms.PictureBox();
            this.pbInvisiblePC = new System.Windows.Forms.PictureBox();
            this.pbStatus1 = new System.Windows.Forms.PictureBox();
            this.txtStatus1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbReconnect1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInvisiblePC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRunAsAdmin
            // 
            this.btnRunAsAdmin.Location = new System.Drawing.Point(29, 28);
            this.btnRunAsAdmin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRunAsAdmin.Name = "btnRunAsAdmin";
            this.btnRunAsAdmin.Size = new System.Drawing.Size(183, 36);
            this.btnRunAsAdmin.TabIndex = 1;
            this.btnRunAsAdmin.Text = "Run As";
            this.btnRunAsAdmin.UseVisualStyleBackColor = true;
            this.btnRunAsAdmin.Click += new System.EventHandler(this.btnRunAsAdmin_Click);
            // 
            // btnAllowPings
            // 
            this.btnAllowPings.Location = new System.Drawing.Point(29, 109);
            this.btnAllowPings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAllowPings.Name = "btnAllowPings";
            this.btnAllowPings.Size = new System.Drawing.Size(183, 36);
            this.btnAllowPings.TabIndex = 3;
            this.btnAllowPings.Text = "Allow Ping";
            this.btnAllowPings.UseVisualStyleBackColor = true;
            this.btnAllowPings.Click += new System.EventHandler(this.btnAllowPings_Click);
            // 
            // btnAllowRDP
            // 
            this.btnAllowRDP.Location = new System.Drawing.Point(29, 68);
            this.btnAllowRDP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAllowRDP.Name = "btnAllowRDP";
            this.btnAllowRDP.Size = new System.Drawing.Size(183, 36);
            this.btnAllowRDP.TabIndex = 2;
            this.btnAllowRDP.Text = "Allow Remote Desktop";
            this.btnAllowRDP.UseVisualStyleBackColor = true;
            this.btnAllowRDP.Click += new System.EventHandler(this.btnAllowRDP_Click);
            // 
            // btnRestoreDefault
            // 
            this.btnRestoreDefault.Location = new System.Drawing.Point(29, 150);
            this.btnRestoreDefault.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRestoreDefault.Name = "btnRestoreDefault";
            this.btnRestoreDefault.Size = new System.Drawing.Size(183, 36);
            this.btnRestoreDefault.TabIndex = 4;
            this.btnRestoreDefault.Text = "Restore all settings";
            this.btnRestoreDefault.UseVisualStyleBackColor = true;
            this.btnRestoreDefault.Click += new System.EventHandler(this.btnRestoreDefault_Click);
            // 
            // pbReconnect1
            // 
            this.pbReconnect1.BackgroundImage = global::RunAsAdminTool.Properties.Resources.retry1;
            this.pbReconnect1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbReconnect1.Location = new System.Drawing.Point(214, 202);
            this.pbReconnect1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbReconnect1.Name = "pbReconnect1";
            this.pbReconnect1.Size = new System.Drawing.Size(22, 24);
            this.pbReconnect1.TabIndex = 10;
            this.pbReconnect1.TabStop = false;
            this.pbReconnect1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbReconnect1_MouseClick);
            // 
            // pbInvisiblePC
            // 
            this.pbInvisiblePC.BackgroundImage = global::RunAsAdminTool.Properties.Resources.invisiblePCIcon;
            this.pbInvisiblePC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbInvisiblePC.Location = new System.Drawing.Point(104, 190);
            this.pbInvisiblePC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbInvisiblePC.Name = "pbInvisiblePC";
            this.pbInvisiblePC.Size = new System.Drawing.Size(32, 34);
            this.pbInvisiblePC.TabIndex = 8;
            this.pbInvisiblePC.TabStop = false;
            this.pbInvisiblePC.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbInvisiblePC_MouseClick);
            this.pbInvisiblePC.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbInvisiblePC_MouseDown);
            // 
            // pbStatus1
            // 
            this.pbStatus1.BackgroundImage = global::RunAsAdminTool.Properties.Resources.statusOfflineMini;
            this.pbStatus1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbStatus1.Location = new System.Drawing.Point(2, 211);
            this.pbStatus1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbStatus1.Name = "pbStatus1";
            this.pbStatus1.Size = new System.Drawing.Size(14, 15);
            this.pbStatus1.TabIndex = 11;
            this.pbStatus1.TabStop = false;
            // 
            // txtStatus1
            // 
            this.txtStatus1.AutoSize = true;
            this.txtStatus1.Location = new System.Drawing.Point(15, 211);
            this.txtStatus1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtStatus1.Name = "txtStatus1";
            this.txtStatus1.Size = new System.Drawing.Size(37, 13);
            this.txtStatus1.TabIndex = 12;
            this.txtStatus1.Text = "Offline";
            // 
            // CommandToDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 228);
            this.Controls.Add(this.txtStatus1);
            this.Controls.Add(this.pbStatus1);
            this.Controls.Add(this.pbReconnect1);
            this.Controls.Add(this.pbInvisiblePC);
            this.Controls.Add(this.btnRestoreDefault);
            this.Controls.Add(this.btnRunAsAdmin);
            this.Controls.Add(this.btnAllowPings);
            this.Controls.Add(this.btnAllowRDP);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "CommandToDo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.CommandToDo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbReconnect1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInvisiblePC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRunAsAdmin;
        private System.Windows.Forms.Button btnAllowPings;
        private System.Windows.Forms.Button btnAllowRDP;
        private System.Windows.Forms.Button btnRestoreDefault;
        private System.Windows.Forms.PictureBox pbInvisiblePC;
        public System.Windows.Forms.PictureBox pbStatus1;
        public System.Windows.Forms.PictureBox pbReconnect1;
        public System.Windows.Forms.Label txtStatus1;
    }
}