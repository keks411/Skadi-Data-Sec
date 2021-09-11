using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;
using Ionic.Zip;
using System.Diagnostics;
using System.Threading;



namespace FLOR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            //check for admin or not, do not allow non-admin and close
            bool IsAdm = Convert.ToBoolean(IsAdministrator());
            if (IsAdm == false)
            {
                MessageBox.Show("Application must be run as ADMIN!", "Missing Privs");
                System.Windows.Forms.Application.Exit();
            }

            //color statusstrip
            toolStripStatusLabel1.Text = "ELEVATED";
            toolStripStatusLabel1.ForeColor = Color.Green;

            //clear console window
            tBoxConsole.Text = "";

            //write basic info into window
            tBoxConsole.Text = "hallo";
            tBoxConsole.Text += System.Environment.NewLine + "aaa";
            
            
        }

        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            //downloading scanner zip
            string DownPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string DownFile = DownPath + "\\ds.zip";
            MessageBox.Show(DownPath);
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://dstoolsiocsearch.blob.core.windows.net/ioc1tools/ds.zip", DownFile);

            //extract zip
            using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(DownFile))
            {
                zip.Password = "1234";
                zip.ExtractAll(DownPath, Ionic.Zip.ExtractExistingFileAction.DoNotOverwrite);
            }

            //start upgrader
            MessageBox.Show("Starting Online-Update");
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "loki-upgrader.exe";
            
        }




    }
}
