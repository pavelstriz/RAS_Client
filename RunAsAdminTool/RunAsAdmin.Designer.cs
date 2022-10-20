namespace RunAsAdminTool
{
    partial class RunAsAdmin
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunAsAdmin));
            this.txtSelectedPath = new System.Windows.Forms.TextBox();
            this.btnRunAsAdmin = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSelectedPath
            // 
            this.txtSelectedPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSelectedPath.Location = new System.Drawing.Point(37, 58);
            this.txtSelectedPath.Name = "txtSelectedPath";
            this.txtSelectedPath.Size = new System.Drawing.Size(274, 27);
            this.txtSelectedPath.TabIndex = 0;
            this.txtSelectedPath.Text = "C:\\Windows\\System32\\cmd.exe";
            // 
            // btnRunAsAdmin
            // 
            this.btnRunAsAdmin.Location = new System.Drawing.Point(145, 117);
            this.btnRunAsAdmin.Name = "btnRunAsAdmin";
            this.btnRunAsAdmin.Size = new System.Drawing.Size(150, 45);
            this.btnRunAsAdmin.TabIndex = 1;
            this.btnRunAsAdmin.Text = "Run As Admin";
            this.btnRunAsAdmin.UseVisualStyleBackColor = true;
            this.btnRunAsAdmin.Click += new System.EventHandler(this.btnRunAsAdmin_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(317, 58);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(82, 27);
            this.btnSelectPath.TabIndex = 2;
            this.btnSelectPath.Text = "Change...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtSelectedPath);
            this.panel1.Controls.Add(this.btnSelectPath);
            this.panel1.Controls.Add(this.btnRunAsAdmin);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 207);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(33, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select path:";
            // 
            // RunAsAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 207);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RunAsAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administrator";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSelectedPath;
        private System.Windows.Forms.Button btnRunAsAdmin;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}

