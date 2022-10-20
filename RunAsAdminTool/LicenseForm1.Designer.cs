namespace RunAsAdminTool
{
    partial class LicenseForm1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseForm1));
            this.txtVerKey = new System.Windows.Forms.RichTextBox();
            this.txtRegKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSerialN = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.btnVerify = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVerify)).BeginInit();
            this.SuspendLayout();
            // 
            // txtVerKey
            // 
            this.txtVerKey.Location = new System.Drawing.Point(152, 177);
            this.txtVerKey.Name = "txtVerKey";
            this.txtVerKey.Size = new System.Drawing.Size(245, 77);
            this.txtVerKey.TabIndex = 4;
            this.txtVerKey.Text = "";
            // 
            // txtRegKey
            // 
            this.txtRegKey.Location = new System.Drawing.Point(152, 129);
            this.txtRegKey.Name = "txtRegKey";
            this.txtRegKey.ReadOnly = true;
            this.txtRegKey.Size = new System.Drawing.Size(245, 22);
            this.txtRegKey.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(26, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 17);
            this.label3.TabIndex = 28;
            this.label3.Text = "Verification key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(20, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 17);
            this.label2.TabIndex = 27;
            this.label2.Text = "Registration key";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(37, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 17);
            this.label1.TabIndex = 26;
            this.label1.Text = "Serial number";
            // 
            // txtSerialN
            // 
            this.txtSerialN.ForeColor = System.Drawing.Color.Black;
            this.txtSerialN.Location = new System.Drawing.Point(152, 78);
            this.txtSerialN.MaxLength = 10;
            this.txtSerialN.Name = "txtSerialN";
            this.txtSerialN.Size = new System.Drawing.Size(245, 22);
            this.txtSerialN.TabIndex = 2;
            this.txtSerialN.TextChanged += new System.EventHandler(this.txtSerialN_TextChanged);
            this.txtSerialN.Enter += new System.EventHandler(this.txtSerialN_Enter);
            this.txtSerialN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerialN_KeyDown);
            this.txtSerialN.Leave += new System.EventHandler(this.txtSerialN_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(110, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(223, 32);
            this.label4.TabIndex = 24;
            this.label4.Text = "Register product";
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImage = global::RunAsAdminTool.Properties.Resources.storno_license1_1;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Location = new System.Drawing.Point(266, 269);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(53, 53);
            this.btnClose.TabIndex = 34;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // btnVerify
            // 
            this.btnVerify.BackgroundImage = global::RunAsAdminTool.Properties.Resources.verify_license1_2;
            this.btnVerify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnVerify.Location = new System.Drawing.Point(344, 269);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(53, 53);
            this.btnVerify.TabIndex = 33;
            this.btnVerify.TabStop = false;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            this.btnVerify.MouseEnter += new System.EventHandler(this.btnVerify_MouseEnter);
            this.btnVerify.MouseLeave += new System.EventHandler(this.btnVerify_MouseLeave);
            // 
            // textBox1
            // 
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(40, 20);
            this.textBox1.MaxLength = 10;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(12, 22);
            this.textBox1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(29, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(30, 32);
            this.panel1.TabIndex = 35;
            // 
            // LicenseForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(440, 334);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtVerKey);
            this.Controls.Add(this.txtRegKey);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSerialN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LicenseForm1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Activation";
            this.Load += new System.EventHandler(this.LicenseForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnVerify)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox txtVerKey;
        private System.Windows.Forms.TextBox txtRegKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSerialN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox btnVerify;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
    }
}