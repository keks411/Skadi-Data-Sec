
namespace FLOR
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BtnDown = new System.Windows.Forms.Button();
            this.tBoxConsole = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.btnCleanC = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDebug = new System.Windows.Forms.Button();
            this.lblDom2 = new System.Windows.Forms.Label();
            this.lblUser2 = new System.Windows.Forms.Label();
            this.lblHost2 = new System.Windows.Forms.Label();
            this.lblVer2 = new System.Windows.Forms.Label();
            this.lblDom = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblVer = new System.Windows.Forms.Label();
            this.btnInetCheck = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.lblLinkW = new System.Windows.Forms.LinkLabel();
            this.gBoxOptions = new System.Windows.Forms.GroupBox();
            this.rBtnOnline = new System.Windows.Forms.RadioButton();
            this.rBtnOffline = new System.Windows.Forms.RadioButton();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnDown
            // 
            this.BtnDown.Location = new System.Drawing.Point(413, 23);
            this.BtnDown.Name = "BtnDown";
            this.BtnDown.Size = new System.Drawing.Size(150, 35);
            this.BtnDown.TabIndex = 0;
            this.BtnDown.Text = "Scan";
            this.BtnDown.UseVisualStyleBackColor = true;
            this.BtnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // tBoxConsole
            // 
            this.tBoxConsole.Location = new System.Drawing.Point(12, 151);
            this.tBoxConsole.Multiline = true;
            this.tBoxConsole.Name = "tBoxConsole";
            this.tBoxConsole.ReadOnly = true;
            this.tBoxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBoxConsole.Size = new System.Drawing.Size(551, 80);
            this.tBoxConsole.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 234);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(575, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // btnCleanC
            // 
            this.btnCleanC.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCleanC.ForeColor = System.Drawing.Color.Red;
            this.btnCleanC.Location = new System.Drawing.Point(413, 122);
            this.btnCleanC.Name = "btnCleanC";
            this.btnCleanC.Size = new System.Drawing.Size(150, 23);
            this.btnCleanC.TabIndex = 3;
            this.btnCleanC.Text = "Clear";
            this.btnCleanC.UseVisualStyleBackColor = true;
            this.btnCleanC.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDebug);
            this.groupBox1.Controls.Add(this.lblDom2);
            this.groupBox1.Controls.Add(this.lblUser2);
            this.groupBox1.Controls.Add(this.lblHost2);
            this.groupBox1.Controls.Add(this.lblVer2);
            this.groupBox1.Controls.Add(this.lblDom);
            this.groupBox1.Controls.Add(this.lblUser);
            this.groupBox1.Controls.Add(this.lblHost);
            this.groupBox1.Controls.Add(this.lblVer);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 132);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "System Info:";
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(89, -1);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 9;
            this.btnDebug.Text = "Debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Visible = false;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // lblDom2
            // 
            this.lblDom2.AutoSize = true;
            this.lblDom2.Location = new System.Drawing.Point(89, 89);
            this.lblDom2.Name = "lblDom2";
            this.lblDom2.Size = new System.Drawing.Size(40, 15);
            this.lblDom2.TabIndex = 7;
            this.lblDom2.Text = "label5";
            // 
            // lblUser2
            // 
            this.lblUser2.AutoSize = true;
            this.lblUser2.Location = new System.Drawing.Point(89, 70);
            this.lblUser2.Name = "lblUser2";
            this.lblUser2.Size = new System.Drawing.Size(40, 15);
            this.lblUser2.TabIndex = 6;
            this.lblUser2.Text = "label6";
            // 
            // lblHost2
            // 
            this.lblHost2.AutoSize = true;
            this.lblHost2.Location = new System.Drawing.Point(89, 51);
            this.lblHost2.Name = "lblHost2";
            this.lblHost2.Size = new System.Drawing.Size(40, 15);
            this.lblHost2.TabIndex = 5;
            this.lblHost2.Text = "label7";
            // 
            // lblVer2
            // 
            this.lblVer2.AutoSize = true;
            this.lblVer2.Location = new System.Drawing.Point(89, 32);
            this.lblVer2.Name = "lblVer2";
            this.lblVer2.Size = new System.Drawing.Size(40, 15);
            this.lblVer2.TabIndex = 4;
            this.lblVer2.Text = "label8";
            // 
            // lblDom
            // 
            this.lblDom.AutoSize = true;
            this.lblDom.Location = new System.Drawing.Point(6, 89);
            this.lblDom.Name = "lblDom";
            this.lblDom.Size = new System.Drawing.Size(40, 15);
            this.lblDom.TabIndex = 3;
            this.lblDom.Text = "label4";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(6, 70);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(40, 15);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "label3";
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(6, 51);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(40, 15);
            this.lblHost.TabIndex = 1;
            this.lblHost.Text = "label2";
            // 
            // lblVer
            // 
            this.lblVer.AutoSize = true;
            this.lblVer.Location = new System.Drawing.Point(6, 32);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(40, 15);
            this.lblVer.TabIndex = 0;
            this.lblVer.Text = "label1";
            // 
            // btnInetCheck
            // 
            this.btnInetCheck.Location = new System.Drawing.Point(413, 93);
            this.btnInetCheck.Name = "btnInetCheck";
            this.btnInetCheck.Size = new System.Drawing.Size(150, 23);
            this.btnInetCheck.TabIndex = 5;
            this.btnInetCheck.Text = "Check Inet connection";
            this.btnInetCheck.UseVisualStyleBackColor = true;
            this.btnInetCheck.Click += new System.EventHandler(this.btnInetCheck_Click);
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(413, 64);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(150, 23);
            this.btnClean.TabIndex = 6;
            this.btnClean.Text = "Clean Env";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // lblLinkW
            // 
            this.lblLinkW.AutoSize = true;
            this.lblLinkW.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLinkW.Location = new System.Drawing.Point(486, 243);
            this.lblLinkW.Name = "lblLinkW";
            this.lblLinkW.Size = new System.Drawing.Size(66, 13);
            this.lblLinkW.TabIndex = 8;
            this.lblLinkW.TabStop = true;
            this.lblLinkW.Text = "Why Skadi?";
            this.lblLinkW.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLinkW_LinkClicked);
            // 
            // gBoxOptions
            // 
            this.gBoxOptions.Controls.Add(this.rBtnOffline);
            this.gBoxOptions.Controls.Add(this.rBtnOnline);
            this.gBoxOptions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.gBoxOptions.Location = new System.Drawing.Point(282, 13);
            this.gBoxOptions.Name = "gBoxOptions";
            this.gBoxOptions.Size = new System.Drawing.Size(125, 132);
            this.gBoxOptions.TabIndex = 9;
            this.gBoxOptions.TabStop = false;
            this.gBoxOptions.Text = "Options";
            // 
            // rBtnOnline
            // 
            this.rBtnOnline.AutoSize = true;
            this.rBtnOnline.Location = new System.Drawing.Point(6, 30);
            this.rBtnOnline.Name = "rBtnOnline";
            this.rBtnOnline.Size = new System.Drawing.Size(61, 19);
            this.rBtnOnline.TabIndex = 0;
            this.rBtnOnline.TabStop = true;
            this.rBtnOnline.Text = "Online";
            this.rBtnOnline.UseVisualStyleBackColor = true;
            // 
            // rBtnOffline
            // 
            this.rBtnOffline.AutoSize = true;
            this.rBtnOffline.Location = new System.Drawing.Point(6, 55);
            this.rBtnOffline.Name = "rBtnOffline";
            this.rBtnOffline.Size = new System.Drawing.Size(64, 19);
            this.rBtnOffline.TabIndex = 1;
            this.rBtnOffline.TabStop = true;
            this.rBtnOffline.Text = "Offline";
            this.rBtnOffline.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 256);
            this.Controls.Add(this.gBoxOptions);
            this.Controls.Add(this.lblLinkW);
            this.Controls.Add(this.btnInetCheck);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnCleanC);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tBoxConsole);
            this.Controls.Add(this.BtnDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "SKADI - Data-Sec GmbH";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gBoxOptions.ResumeLayout(false);
            this.gBoxOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnDown;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox tBoxConsole;
        private System.Windows.Forms.Button btnCleanC;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDom2;
        private System.Windows.Forms.Label lblUser2;
        private System.Windows.Forms.Label lblHost2;
        private System.Windows.Forms.Label lblVer2;
        private System.Windows.Forms.Label lblDom;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button btnInetCheck;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.LinkLabel lblLinkW;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.GroupBox gBoxOptions;
        private System.Windows.Forms.RadioButton rBtnOffline;
        private System.Windows.Forms.RadioButton rBtnOnline;
    }
}

