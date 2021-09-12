using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Security.Principal;
using Ionic.Zip;
using System.Diagnostics;
using Azure.Storage;


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
            //clear console window
            tBoxConsole.Text = "";

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
            tBoxConsole.AppendText("### Checking for permsissions ###" + Environment.NewLine);
            bool IsAdm = Convert.ToBoolean(IsAdministrator());
            if (IsAdm == false)
            {
                tBoxConsole.AppendText("### ERROR! Wrong Token! ###" + Environment.NewLine);
                MessageBox.Show("Application must be run as ADMIN!", "Missing Privs");
                System.Windows.Forms.Application.Exit();
            }
            tBoxConsole.AppendText("### SUCCESS! Elevated Token! ###" + Environment.NewLine);

            //color statusstrip
            toolStripStatusLabel1.Text = "ELEVATED";
            toolStripStatusLabel1.ForeColor = Color.Green;

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
            MessageBox.Show("Click OK to start the scan. The scan itself may take several hours!", "INFO");

            //clear files
            cleanUp();

            //enable pbar
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Value = 0;

            // INSERT ONLINE OR OFFLIEN ROUTINE
            downloadEx();

            // starting scan
            tBoxConsole.AppendText("### Starting scan with default options ###" + Environment.NewLine);
            string lokiPath = Convert.ToString(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ds");
            string loki = lokiPath + "\\loki.exe";

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
            toolStripProgressBar1.Value = 80;
            tBoxConsole.AppendText("### Scanning complete ###" + Environment.NewLine);

            //pack it together
            packIt();
            toolStripProgressBar1.Value = 90;
            tBoxConsole.AppendText("### Compressed and ready to ship ###" + Environment.NewLine);

            uploadIt();
            toolStripProgressBar1.Value = 95;
            tBoxConsole.AppendText("### File uploaded ###" + Environment.NewLine);


            toolStripProgressBar1.Value = 100;
            cleanUp();

            tBoxConsole.AppendText("### Finished ###" + Environment.NewLine);
            tBoxConsole.AppendText("### Data-Sec GmbH will get back to you shortly ###" + Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tBoxConsole.Text = "";
        }

        private bool Ping(string url)
        {
            tBoxConsole.AppendText("### Checking Internet Connection ###" + Environment.NewLine);
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
                    Globals.isOn = true;
                    return true;
                }
            }
            catch
            {
                tBoxConsole.AppendText("### Connection not possible 404 ###" + Environment.NewLine);
                tBoxConsole.AppendText("### Falling back to offline package ###" + Environment.NewLine);
                toolStripStatusLabel2.ForeColor = Color.Red;
                Globals.isOn = false;
                return false;
            }
        }

        private void btnInetCheck_Click(object sender, EventArgs e)
        {
            Ping("https://google.com");
        }

        private void cleanUp()
        {
            tBoxConsole.AppendText("### Cleaning Environment ###" + Environment.NewLine);
            string downf = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string ds = downf + "\\ds";
            string dsz = downf + "\\ds.zip";

            try
            {
                //delte directory ds
                Directory.Delete(ds, true);
            }
            catch (Exception)
            {
            }

            try
            {
                //delete ds.zip
                File.Delete(dsz);
            }
            catch (Exception)
            {
            }
            tBoxConsole.AppendText("### Environment cleaned ###" + Environment.NewLine);
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            cleanUp();
            tBoxConsole.Text = "";
            toolStripProgressBar1.Value = 0;
        }

        private void packIt()
        {
            string hostname = System.Environment.GetEnvironmentVariable("Computername");
            string domain = System.Environment.GetEnvironmentVariable("Userdomain");
            string downf = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string report = downf + "\\ds";
            string reportz = report + "\\" + hostname + "---" + domain + "---" + "REPORT.zip";

            //add file with pw
            ZipFile zip = new ZipFile(reportz);

            //add .log files
            string[] filesl =
            Directory.GetFiles(report, "*.log", SearchOption.TopDirectoryOnly);

            //add .html files
            string[] filesh =
            Directory.GetFiles(report, "*.html", SearchOption.TopDirectoryOnly);

            zip.Password = "cajcsnj23basc78a2basjhasdhk2jkhasdjhoajhs";
            zip.AddFiles(filesl, "");
            zip.AddFiles(filesh, "");
            zip.Save();

        }
        private void uploadIt()
        {
            string apd = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string hostname = System.Environment.GetEnvironmentVariable("Computername");
            string domain = System.Environment.GetEnvironmentVariable("Userdomain");
            string downf = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string report = downf + "\\ds";
            string zname = hostname + "---" + domain + "---" + "REPORT.zip";
            string reportz = report + "\\" + hostname + "---" + domain + "---" + "REPORT.zip";

            string storageAccntConnection = "DefaultEndpointsProtocol=https;AccountName=dstoolsiocsearch;AccountKey=ubfzvgP0Bnlx/8ADax9ZZVx4DY5O2J5rHbUjgy1+Zquj3/CyC+5D79WKORKx1BjNiwVr7gNi/fUvV1XHTvLk8Q==;EndpointSuffix=core.windows.net";
            Azure.Storage.Blobs.BlobClient blobClient = new Azure.Storage.Blobs.BlobClient(
            connectionString: storageAccntConnection,
            blobContainerName: "reports",
            blobName: zname);


            //upload the zip
            try
            {
                blobClient.Upload(reportz);

            } catch
            {
                MessageBox.Show("Something went wrong. Please contact Data-Sec!");
            }

        }

        private void downloadEx()
        {
            if (Globals.isOn == true)
            {
                //downloading scanner zip
                tBoxConsole.AppendText("### Seems to be ONLINE ###" + Environment.NewLine);
                tBoxConsole.AppendText("### Starting Online-Routine ###" + Environment.NewLine);
                tBoxConsole.AppendText("### Downloading engine ###" + Environment.NewLine);
                string DownPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string DownFile = DownPath + "\\ds.zip";
                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://dstoolsiocsearch.blob.core.windows.net/ioc1tools/ds.zip", DownFile);
                toolStripProgressBar1.Value = 15;

                //extract zip
                tBoxConsole.AppendText("### Extracting scanner ###" + Environment.NewLine);
                using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(DownFile))
                {
                    zip.Password = "kjsvlka1";
                    zip.ExtractAll(DownPath, Ionic.Zip.ExtractExistingFileAction.DoNotOverwrite);
                }
                toolStripProgressBar1.Value = 30;

                //start upgrader
                tBoxConsole.AppendText("### Start upgrading process ###" + Environment.NewLine);
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
                toolStripProgressBar1.Value = 45;
                tBoxConsole.AppendText("### Upgrade complete ###" + Environment.NewLine);

            } else //system is offline
            {
                //extract zip
                string DownPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string DownFile = DownPath + "\\ds.zip";
                tBoxConsole.AppendText("### Extracting scanner ###" + Environment.NewLine);
                File.WriteAllBytes(DownFile, Properties.Resources.ds);
                using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(DownFile))
                {
                    zip.Password = "kjsvlka1";
                    zip.ExtractAll(DownPath, Ionic.Zip.ExtractExistingFileAction.DoNotOverwrite);
                }
                toolStripProgressBar1.Value = 30;
            }



        }
        private void btnPack_Click(object sender, EventArgs e)
        {
            packIt();
            uploadIt();
        }

        public static class Globals
        {
            public static bool isOn = false;
        }
    }
}
