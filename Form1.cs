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
using System.Net.NetworkInformation;



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
            //basic setup for start
            lblVer.Text = "Version:";
            lblHost.Text = "Hostname:";
            lblUser.Text = "Username:";
            lblDom.Text = "Domain:";

            //read basic info and store in system info labels
            string hostname = System.Environment.GetEnvironmentVariable("Computername");
            string userName = System.Environment.GetEnvironmentVariable("username");
            string domain = System.Environment.GetEnvironmentVariable("Userdomain");

            lblVer2.Text = "1.0";
            lblHost2.Text = hostname;
            lblUser2.Text = userName;
            lblDom2.Text = domain;
            

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

            //check inet conn
            Ping("https://google.com");

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
            //clear files
            cleanUp();

            //downloading scanner zip
            tBoxConsole.Text = "### Downloading scanner..." + Environment.NewLine;
            string DownPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string DownFile = DownPath + "\\ds.zip";
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://dstoolsiocsearch.blob.core.windows.net/ioc1tools/ds.zip", DownFile);

            //extract zip
            tBoxConsole.AppendText("### Extracting scanner..." + Environment.NewLine);
            using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(DownFile))
            {
                zip.Password = "kjsvlkankvknl43klsdbshioafwlwgl4kfasklbf";
                zip.ExtractAll(DownPath, Ionic.Zip.ExtractExistingFileAction.DoNotOverwrite);
            }

            //start upgrader
            tBoxConsole.AppendText("### Start upgrading process..." + Environment.NewLine);
            int lineCount = 0;
            string lupgrader = DownPath + "\\ds\\loki-upgrader.exe";
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            p.StartInfo.WorkingDirectory = DownPath + "\\ds";
            p.StartInfo.LoadUserProfile = true;
            p.StartInfo.FileName = lupgrader;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            p.EnableRaisingEvents = true;

            p.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                // Prepend line numbers to each line of the output.
                if (!String.IsNullOrEmpty(e.Data))
                {
                    lineCount++;
                    tBoxConsole.AppendText(e.Data + Environment.NewLine);
                }
            });
            p.Start();

            // Asynchronously read the standard output of the spawned process.
            // This raises OutputDataReceived events for each line of output.
            p.BeginOutputReadLine();
            p.WaitForExit();

            p.WaitForExit();
            p.Close();

            // starting scan
            tBoxConsole.AppendText("### Starting scan with default options..." + Environment.NewLine);
            MessageBox.Show("Click OK to start the scan. The scan itself may take several hours!", "INFO");
            string lokiPath = Convert.ToString(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ds");
            //create report folder first before scan
            System.IO.Directory.CreateDirectory(lokiPath + "\\report");
            string loki = lokiPath + "\\loki.exe";
            string lokicmd = loki + " --logfolder report";

            int lcount = 0;
            System.Diagnostics.Process p2 = new System.Diagnostics.Process();

            p2.StartInfo.WorkingDirectory = lokiPath;
            p2.StartInfo.LoadUserProfile = true;
            p2.StartInfo.FileName = loki;
            p2.StartInfo.UseShellExecute = false;
            p2.StartInfo.CreateNoWindow = true;
            p2.StartInfo.RedirectStandardOutput = true;
            p2.StartInfo.RedirectStandardError = true;

            p2.EnableRaisingEvents = true;

            p2.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                // Prepend line numbers to each line of the output.
                if (!String.IsNullOrEmpty(e.Data))
                {
                    lcount++;
                    tBoxConsole.AppendText(e.Data + Environment.NewLine);
                }
            });
            p2.Start();

            // Asynchronously read the standard output of the spawned process.
            // This raises OutputDataReceived events for each line of output.
            p2.BeginOutputReadLine();

            p2.WaitForExit();
            p2.Close();
            tBoxConsole.Text = "### Scanning complete..." + Environment.NewLine;

            //pack it together
            packIt();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            tBoxConsole.Text = "";
        }

        private bool Ping(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = 3000;
                request.AllowAutoRedirect = false; // find out if this site is up and don't follow a redirector
                request.Method = "HEAD";

                using (var response = request.GetResponse())
                {
                    toolStripStatusLabel2.Text = "InetCheck: ONLINE";
                    toolStripStatusLabel2.ForeColor = Color.Green;
                    return true;
                }
            }
            catch
            {
                toolStripStatusLabel2.Text = "InetCheck: OFFLINE";
                toolStripStatusLabel2.ForeColor = Color.Red;
                return false;
            }
        }

        private void btnInetCheck_Click(object sender, EventArgs e)
        {
            Ping("https://google.com");
        }

        private void cleanUp()
        {
            string downf = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string ds = downf + "\\ds";
            string dsz = downf + "\\ds.zip";

            try
            {
                //delte directory ds
                Directory.Delete(ds, true);
            }
            catch (Exception e)
            {
            }

            try
            {
                //delete ds.zip
                File.Delete(dsz);
            }
            catch (Exception e)
            {
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            cleanUp();
            tBoxConsole.Text = "";
        }

        private void packIt()
        {
            string downf = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string report = downf + "\\ds\\report";
            using (var zip = new ZipFile(report + "\\report.zip")) {

                zip.Password = "cajcsnj23basc78a2basjhasdhk2jkhasdjhoajhs";
                zip.AddDirectory(report);
                zip.Save();
            }
            
        }
    }
}
