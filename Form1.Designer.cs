
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
            this.lblDom2 = new System.Windows.Forms.Label();
            this.lblUser2 = new System.Windows.Forms.Label();
            this.lblHost2 = new System.Windows.Forms.Label();
            this.lblVer2 = new System.Windows.Forms.Label();
            this.lblDom = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblHost = new System.Windows.Forms.Label();
            this.lblVer = new System.Windows.Forms.Label();
            this.btnDebug = new System.Windows.Forms.Button();
            this.btnInetCheck = new System.Windows.Forms.Button();
            this.lblLinkW = new System.Windows.Forms.LinkLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnDown
            // 
            this.BtnDown.Enabled = false;
            this.BtnDown.Location = new System.Drawing.Point(413, 23);
            this.BtnDown.Name = "BtnDown";
            this.BtnDown.Size = new System.Drawing.Size(120, 31);
            this.BtnDown.TabIndex = 0;
            this.BtnDown.Text = "Scan";
            this.BtnDown.UseVisualStyleBackColor = true;
            this.BtnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // tBoxConsole
            // 
            this.tBoxConsole.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tBoxConsole.Location = new System.Drawing.Point(12, 151);
            this.tBoxConsole.Multiline = true;
            this.tBoxConsole.Name = "tBoxConsole";
            this.tBoxConsole.ReadOnly = true;
            this.tBoxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBoxConsole.Size = new System.Drawing.Size(521, 136);
            this.tBoxConsole.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 296);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(545, 22);
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
            this.btnCleanC.Location = new System.Drawing.Point(413, 89);
            this.btnCleanC.Name = "btnCleanC";
            this.btnCleanC.Size = new System.Drawing.Size(120, 23);
            this.btnCleanC.TabIndex = 3;
            this.btnCleanC.Text = "Clear";
            this.btnCleanC.UseVisualStyleBackColor = true;
            this.btnCleanC.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
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
            this.groupBox1.Size = new System.Drawing.Size(242, 132);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "System Info:";
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
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(413, 118);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 9;
            this.btnDebug.Text = "Debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Visible = false;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // btnInetCheck
            // 
            this.btnInetCheck.Location = new System.Drawing.Point(413, 60);
            this.btnInetCheck.Name = "btnInetCheck";
            this.btnInetCheck.Size = new System.Drawing.Size(120, 23);
            this.btnInetCheck.TabIndex = 5;
            this.btnInetCheck.Text = "Check connection";
            this.btnInetCheck.UseVisualStyleBackColor = true;
            this.btnInetCheck.Click += new System.EventHandler(this.btnInetCheck_Click);
            // 
            // lblLinkW
            // 
            this.lblLinkW.AutoSize = true;
            this.lblLinkW.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLinkW.Location = new System.Drawing.Point(457, 305);
            this.lblLinkW.Name = "lblLinkW";
            this.lblLinkW.Size = new System.Drawing.Size(66, 13);
            this.lblLinkW.TabIndex = 8;
            this.lblLinkW.TabStop = true;
            this.lblLinkW.Text = "Why Skadi?";
            this.lblLinkW.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLinkW_LinkClicked);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 318);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.lblLinkW);
            this.Controls.Add(this.btnInetCheck);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCleanC);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tBoxConsole);
            this.Controls.Add(this.BtnDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "SKADI - Data-Sec GmbH";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.LinkLabel lblLinkW;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

