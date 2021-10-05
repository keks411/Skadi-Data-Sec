using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Security.Principal;
//using Ionic.Zip;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;

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
            //set dark mode
            cBoxDark.Checked = true;
            Globals.darkMode = 1;
            this.BackColor = Color.FromArgb(38, 38, 38);
            tBoxConsole.BackColor = Color.FromArgb(38, 38, 38);
            tBoxConsole.ForeColor = Color.White;
            toolStripProgressBar1.BackColor = Color.FromArgb(38, 38, 38);
            statusStrip1.BackColor = Color.FromArgb(38, 38, 38);
            this.ForeColor = Color.White;
            lblLinkW.BackColor = Color.FromArgb(38, 38, 38);
            gBoxOptional.ForeColor = Color.White;
            groupBox1.ForeColor = Color.White;
            lblDom.ForeColor = Color.White;
            lblDom2.ForeColor = Color.White;
            lblHost.ForeColor = Color.White;
            lblHost2.ForeColor = Color.White;
            lblLinkW.LinkColor = Color.Red;
            lblUser.ForeColor = Color.White;
            lblUser2.ForeColor = Color.White;
            lblVer.ForeColor = Color.White;
            lblVer2.ForeColor = Color.White;
            btnDebug.ForeColor = Color.Black;
            btnInetCheck.ForeColor = Color.Black;
            BtnDown.ForeColor = Color.Black;
            cBoxDark.ForeColor = Color.White;
            cBoxOfflineScan.ForeColor = Color.White;

            //debug
            btnDebug.Visible = false;

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

            lblVer2.Text = "1.1.3";
            lblHost2.Text = hostname;
            lblUser2.Text = userName;
            lblDom2.Text = domain;

            //check for admin or not, do not allow non-admin and close
            tBoxConsole.AppendText("### Checking for permissions ###" + Environment.NewLine);
            bool IsAdm = Convert.ToBoolean(IsAdministrator());
            if (IsAdm == false)
            {
                tBoxConsole.AppendText("### ERROR! Missing permissions! ###" + Environment.NewLine);
                MessageBox.Show("Application must be run as ADMIN!", "INFO");
                System.Windows.Forms.Application.Exit();
            }
            tBoxConsole.AppendText("### SUCCESS! Elevated User! ###" + Environment.NewLine);

            //color statusstrip
            toolStripStatusLabel1.Text = "ELEVATED";
            toolStripStatusLabel1.ForeColor = Color.Green;

            //check inet conn
            tBoxConsole.AppendText("### Checking Internet Connection ###" + Environment.NewLine);
            Globals.iNetCheck = 0;

            Ping("https://dstoolsiocsearch.blob.core.windows.net/ioc1tools/ds.zip");
            Ping("https://github.com/Neo23x0/Loki/releases/download/v0.44.1/loki_0.44.1.zip");

            if (Globals.iNetCheck == 1)
            {
                Globals.isOn = false;
                tBoxConsole.AppendText("### Connection not possible 404 ###" + Environment.NewLine);
                tBoxConsole.AppendText("### Falling back to offline package ###" + Environment.NewLine);
                toolStripStatusLabel2.Text = "InetCheck: OFFLINE";
                toolStripStatusLabel2.ForeColor = Color.Red;
            } else
            {
                toolStripStatusLabel2.Text = "InetCheck: ONLINE";
                toolStripStatusLabel2.ForeColor = Color.Green;
                tBoxConsole.AppendText("### SUCCESS! Connection possible ###" + Environment.NewLine);
            }


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

            //check options radiobuttons
            if (cBoxOfflineScan.Checked == true)
            {
                Globals.isOn = false;

                //check if radiobutton set
                if (cBoxOfflineScan.Checked == true)
                {
                    tBoxConsole.AppendText("### Forcing offline scan ###" + Environment.NewLine);
                }
            }

            //enable pbar
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Value = 0;

            // INSERT ONLINE OR OFFLIEN ROUTINE
            downloadEx();

            // starting scan
            tBoxConsole.AppendText("### Starting scan with default options ###" + Environment.NewLine);
            string lokiPath = Convert.ToString(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\loki");
            string loki = lokiPath + "\\loki.exe";

            int lcount = 0;

            //create task
            //this will move everthing into a new task
            //buttons need to be disabled though to not screw up
            cBoxOfflineScan.Enabled = false;
            BtnDown.Enabled = false;
            btnInetCheck.Enabled = false;
            Task.Factory.StartNew(() =>
            {
                Process p2 = new Process();

                p2.StartInfo.WorkingDirectory = lokiPath;
                p2.StartInfo.Arguments = "--noindicator --csv -l iocscan.csv";
                p2.StartInfo.LoadUserProfile = true;
                p2.StartInfo.FileName = loki;
                p2.StartInfo.UseShellExecute = false;
                p2.StartInfo.CreateNoWindow = true;
                p2.StartInfo.RedirectStandardOutput = true;
                p2.StartInfo.RedirectStandardError = true;
                p2.EnableRaisingEvents = true;
                tBoxConsole.AppendText("### Scanning for IOCs ###" + Environment.NewLine);
                p2.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    // Prepend line numbers to each line of the output.
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        lcount++;
                        //tBoxConsole.AppendText(e.Data + Environment.NewLine);
                    }
                });
                p2.Start();

                // Asynchronously read the standard output of the spawned process.
                // This raises OutputDataReceived events for each line of output.
                p2.BeginOutputReadLine();

                p2.WaitForExit();
                p2.Close();
                toolStripProgressBar1.Value = 70;

                p2.StartInfo.WorkingDirectory = lokiPath;
                p2.StartInfo.Arguments = "--noindicator --csv -l iocscan.csv";
                p2.StartInfo.LoadUserProfile = true;
                p2.StartInfo.FileName = loki;
                p2.StartInfo.UseShellExecute = false;
                p2.StartInfo.CreateNoWindow = true;
                p2.StartInfo.RedirectStandardOutput = true;
                p2.StartInfo.RedirectStandardError = true;
                p2.EnableRaisingEvents = true;


                //running autoruns
                runBinary("-accepteula -a * -c -m -o autoruns.csv", "autorunsc64.exe", "### Scanning autostart ###", 0);
                toolStripProgressBar1.Value = 80;

                //running handle64
                runBinary("-accepteula", "handle64.exe", "### Scanning open Handles ###", 1);
                toolStripProgressBar1.Value = 85;

                //running pslist
                runBinary("-accepteula -d -m -x", "pslist64.exe", "### Scanning running processes ###", 1);
                toolStripProgressBar1.Value = 85;

                //running tcpvcon
                runBinary("-accepteula -c", "tcpvcon64.exe", "### Scanning open connections ###", 1);
                toolStripProgressBar1.Value = 85;

                //extract eventlogs
                runCmd("/c wevtutil epl System " + lokiPath + "\\System.evtx", "### Extracting Event Logs 1/2 ###");
                runCmd("/c wevtutil epl Security " + lokiPath + "\\Security.evtx", "### Extracting Event Logs 2/2 ###");
                toolStripProgressBar1.Value = 87;

                tBoxConsole.AppendText("### Scanning complete ###" + Environment.NewLine);
                tBoxConsole.AppendText("### Encrypting files  ###" + Environment.NewLine);

                packIt();
                //pack it together
                toolStripProgressBar1.Value = 90;
                tBoxConsole.AppendText("### Compressed and ready to ship ###" + Environment.NewLine);
                


                if (Globals.isOn == true)
                {
                    uploadIt();
                    toolStripProgressBar1.Value = 95;
                    tBoxConsole.AppendText("### File uploaded ###" + Environment.NewLine);
                } else
                {
                }
                toolStripProgressBar1.Value = 100;
                cleanUp();

                tBoxConsole.AppendText("### Finished ###" + Environment.NewLine);
                tBoxConsole.AppendText("### Data-Sec GmbH will get back to you shortly ###" + Environment.NewLine);

                //task is done
                            });
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
                    Globals.isOn = true;
                    return true;
                }
            }
            catch
            {
                Globals.isOn = false;
                Globals.iNetCheck = 1;
                return false;
            }
        }

        private void compressDirectory(string DirectoryPath, string OutputFilePath, int CompressionLevel = 9)
        {
            try
            {
                // Depending on the directory this could be very large and would require more attention
                // in a commercial package.
                string[] filenames = Directory.GetFiles(DirectoryPath);

                // 'using' statements guarantee the stream is closed properly which is a big source
                // of problems otherwise.  Its exception safe as well which is great.
                using (ZipOutputStream OutputStream = new ZipOutputStream(File.Create(OutputFilePath)))
                {

                    // Define the compression level
                    // 0 - store only to 9 - means best compression
                    OutputStream.SetLevel(CompressionLevel);

                    byte[] buffer = new byte[4096];

                    foreach (string file in filenames)
                    {

                        // Using GetFileName makes the result compatible with XP
                        // as the resulting path is not absolute.
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                        // Setup the entry data as required.

                        // Crc and size are handled by the library for seakable streams
                        // so no need to do them here.

                        // Could also use the last write time or similar for the file.
                        entry.DateTime = DateTime.Now;
                        OutputStream.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file))
                        {

                            // Using a fixed size buffer here makes no noticeable difference for output
                            // but keeps a lid on memory usage.
                            int sourceBytes;

                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                OutputStream.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }

                    // Finish/Close arent needed strictly as the using statement does this automatically

                    // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                    // the created file would be invalid.
                    OutputStream.Finish();

                    // Close is important to wrap things up and unlock the file.
                    OutputStream.Close();

                    Console.WriteLine("Files successfully compressed");
                }
            }
            catch (Exception ex)
            {
                // No need to rethrow the exception as for our purposes its handled.
                Console.WriteLine("Exception during processing {0}", ex);
            }
        }

        private void compressDirectoryWithPassword(string DirectoryPath, string OutputFilePath, string Password = null, int CompressionLevel = 9)
        {
            try
            {
                // Depending on the directory this could be very large and would require more attention
                // in a commercial package.
                string[] filenames = Directory.GetFiles(DirectoryPath);

                // 'using' statements guarantee the stream is closed properly which is a big source
                // of problems otherwise.  Its exception safe as well which is great.
                using (ZipOutputStream OutputStream = new ZipOutputStream(File.Create(OutputFilePath)))
                {
                    // Define a password for the file (if providen)
                    // set its value to null or don't declare it to leave the file
                    // without password protection
                    OutputStream.Password = Password;

                    // Define the compression level
                    // 0 - store only to 9 - means best compression
                    OutputStream.SetLevel(CompressionLevel);

                    byte[] buffer = new byte[4096];

                    foreach (string file in filenames)
                    {

                        // Using GetFileName makes the result compatible with XP
                        // as the resulting path is not absolute.
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                        // Setup the entry data as required.

                        // Crc and size are handled by the library for seakable streams
                        // so no need to do them here.

                        // Could also use the last write time or similar for the file.
                        entry.DateTime = DateTime.Now;
                        OutputStream.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file))
                        {

                            // Using a fixed size buffer here makes no noticeable difference for output
                            // but keeps a lid on memory usage.
                            int sourceBytes;

                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                OutputStream.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }

                    // Finish/Close arent needed strictly as the using statement does this automatically

                    // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                    // the created file would be invalid.
                    OutputStream.Finish();

                    // Close is important to wrap things up and unlock the file.
                    OutputStream.Close();

                    Console.WriteLine("Files successfully compressed");
                }
            }
            catch (Exception ex)
            {
                // No need to rethrow the exception as for our purposes its handled.
                Console.WriteLine("Exception during processing {0}", ex);
            }
        }

        private void btnInetCheck_Click(object sender, EventArgs e)
        {
            tBoxConsole.AppendText("### Checking Internet Connection ###" + Environment.NewLine);
            MessageBox.Show("Skadi will check the connection to the following sites:\n\n windows.net\n github.com", "INFO");

            Globals.iNetCheck = 0;

            Ping("https://dstoolsiocsearch.blob.core.windows.net/ioc1tools/ds.zip");
            Ping("https://github.com/Neo23x0/Loki/releases/download/v0.44.1/loki_0.44.1.zip");

            if (Globals.iNetCheck == 1)
            {
                Globals.isOn = false;
                tBoxConsole.AppendText("### ERROR! Connection not possible ###" + Environment.NewLine);
                toolStripStatusLabel2.Text = "InetCheck: OFFLINE";
                toolStripStatusLabel2.ForeColor = Color.Red;
            } else
            {
                tBoxConsole.AppendText("### SUCCESS! Connection possible ###" + Environment.NewLine);
                toolStripStatusLabel2.Text = "InetCheck: ONLINE";
                toolStripStatusLabel2.ForeColor = Color.Green;
            }
        }

        private void cleanUp()
        {
            tBoxConsole.AppendText("### Cleaning Environment ###" + Environment.NewLine);
            string downf = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string reportDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string rFolder = Convert.ToString(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)) + "\\report";
            string ds = downf + "\\loki";
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

            //also delete offline folder report
            try
            {
                Directory.Delete(rFolder, true);
            }
            catch
            {

            }
            tBoxConsole.AppendText("### Environment cleaned ###" + Environment.NewLine);
        }

        private void packIt()
        {
            string hostname = System.Environment.GetEnvironmentVariable("Computername");
            string domain = System.Environment.GetEnvironmentVariable("Userdomain");
            string downf = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string report = downf + "\\loki";
            string reportz = report + "\\" + hostname + "---" + domain + "---" + "REPORT.zip";

            //create loot folder
            Directory.CreateDirectory(report + "\\loot");

            //move all loot files into loot
            try
            {
                File.Move(report + "\\autoruns.csv", report + "\\loot\\autoruns.csv");
                File.Move(report + "\\handle64.exe.txt", report + "\\loot\\handle64.exe.txt");
                File.Move(report + "\\iocscan.csv", report + "\\loot\\iocscan.csv");
                File.Move(report + "\\pslist64.exe.txt", report + "\\loot\\pslist64.exe.txt");
                File.Move(report + "\\tcpvcon64.exe.txt", report + "\\loot\\tcpvcon64.exe.txt");

                //it takes time to extract the files. I could program something complicated or just wait until the file is
                //there and then copy it once its done
                while (File.Exists(report + "\\Security.evtx") != true)
                {
                    System.Threading.Thread.Sleep(1000);
                    // For some reason the evtx does not get created every time
                    // Not sure why but workaround is trying to craeate it again
                    //extract eventlogs
                    runCmd("/c wevtutil epl System " + report + "\\System.evtx", "hidden");
                    runCmd("/c wevtutil epl Security " + report + "\\Security.evtx", "hidden");
                }
                File.Move(report + "\\Security.evtx", report + "\\loot\\Security.evtx");
                File.Move(report + "\\System.evtx", report + "\\loot\\System.evtx");
            } catch
            {

            }

            //create cleartext zip
            compressDirectory(@report + "\\loot", reportz, 9);

            //create key AES
            Random num = new Random();
            int akey = num.Next(10000000, 1000000000);
            string sakey = Convert.ToString(akey);

            //encrypt aes key with rsa
            sakey = EncryptStringRSA(sakey);

            //Write encrypted AES key to file
            File.WriteAllText(report + "\\AESKey.txt", sakey);

            //encrypt the file with aes and save as aes
            FileEncrypt(reportz, Convert.ToString(akey));

            //flush out clear key
            akey = 1337;

            //delete old zip first
            //also create AES folder
            Directory.CreateDirectory(report + "\\AES");
            File.Delete(reportz);

            //move key and aes into AES folder
            try
            {
                File.Move(report + "\\AESKey.txt", report + "\\AES\\AESKey.txt");
                File.Move(reportz + ".aes", report + "\\AES\\" + hostname + "---" + domain + "---" + "REPORT.zip.aes");
            } catch
            {

            }

            //create encrypted zip
            string zPassword = "cajcsnj23basc78a2basjhasdhk2jkhasdjhoajhs";
            compressDirectoryWithPassword(report + "\\AES", reportz, zPassword, 9);

            if (Globals.isOn == false)
            {
                try
                {
                    File.Delete(desktop + "\\report.zip");
                } catch
                {

                }
                File.Move(reportz, desktop + "\\report.zip");
                tBoxConsole.AppendText("### The report is located at: ###" + Environment.NewLine);
                tBoxConsole.AppendText("### " + desktop + "\\report.zip" + " ###" + Environment.NewLine);
            }
        }

        private void uploadIt()
        {
            //file exists already therefore renaming it with random num prefix
            Random num = new Random();
            int rand = num.Next(1000, 10000);

            string apd = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string hostname = System.Environment.GetEnvironmentVariable("Computername");
            string domain = System.Environment.GetEnvironmentVariable("Userdomain");
            string downf = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string report = downf + "\\loki";
            string reportz = report + "\\" + hostname + "---" + domain + "---" + "REPORT.zip";
            string rreport = report + "\\" + rand + "---" + hostname + "---" + domain + "---" + "REPORT.zip";
            string zname = rand + " --- " + hostname + "---" + domain + "---" + "REPORT.zip";
            string storageAccntConnection = "DefaultEndpointsProtocol=https;AccountName=dstoolsiocsearch;AccountKey=ubfzvgP0Bnlx/8ADax9ZZVx4DY5O2J5rHbUjgy1+Zquj3/CyC+5D79WKORKx1BjNiwVr7gNi/fUvV1XHTvLk8Q==;EndpointSuffix=core.windows.net";
            Azure.Storage.Blobs.BlobClient blobClient = new Azure.Storage.Blobs.BlobClient(
            connectionString: storageAccntConnection,
            blobContainerName: "reports",
            blobName: zname);

            //upload the zip
            try { 
                File.Move(reportz, rreport);
                blobClient.Upload(rreport);
            } catch
            {

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
                    zip.ExtractAll(DownPath, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
                }
                toolStripProgressBar1.Value = 30;

                //in case of updated rules its better to delete the whole signature folder
                Directory.Delete(DownPath + "\\loki\\signature-base", true);
                Directory.Delete(DownPath + "\\loki\\docs", true);
                Directory.Delete(DownPath + "\\loki\\plugins", true);
                Directory.Delete(DownPath + "\\loki\\tools", true);
                Directory.Delete(DownPath + "\\loki\\config", true);

                //start upgrader
                //put it all into a new process
                Process p = new Process();
                tBoxConsole.AppendText("### Start upgrading process ###" + Environment.NewLine);
                string lupgrader = DownPath + "\\loki\\loki-upgrader.exe";
                

                p.StartInfo.WorkingDirectory = DownPath + "\\loki";
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.StartInfo.LoadUserProfile = false;
                p.StartInfo.FileName = lupgrader;
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.RedirectStandardError = false;
                p.Start();
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
                toolStripProgressBar1.Value = 30;

                File.WriteAllBytes(DownFile, Properties.Resources.ds);
                using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(DownFile))
                {
                    zip.ExtractAll(DownPath, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
                }
            }



        }

        private void runBinary(string args, string exe, string text, int rdirect)
        {
                tBoxConsole.AppendText(text + Environment.NewLine);
                string lokiPath = Convert.ToString(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\loki");
                string loki = lokiPath + "\\" + exe;

                Process p2 = new Process();
            Globals.lfile = exe;

            if (rdirect == 1)
            {
                p2.StartInfo.RedirectStandardOutput = true;
                p2.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p2.StartInfo.RedirectStandardError = true;
                p2.StartInfo.WorkingDirectory = lokiPath;
                p2.StartInfo.Arguments = args;
                //p2.StartInfo.LoadUserProfile = true;
                p2.StartInfo.FileName = loki;
                p2.StartInfo.CreateNoWindow = true;

                // Prepend line numbers to each line of the output.
                int lineCount = 0;
                p2.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                { 
                if (!String.IsNullOrEmpty(e.Data))
                {
                    lineCount++;
                    if (e.Data.Contains("Loki"))
                    {
                        }
                    else
                    {
                            if (e.Data.Contains("loki"))
                            {
                            }
                            else
                            {
                                if (e.Data.Contains("LOKI"))
                                {
                                }
                                else
                                {
                                    if (e.Data.Contains("Directory walk error"))
                                    {
                                    }
                                    else
                                    {
                                        tBoxConsole.AppendText(e.Data + Environment.NewLine);
                                    }
                                }
                            }
                    }
                
                }
            });


            p2.Start();
                string a = p2.StandardOutput.ReadToEnd();
                p2.WaitForExit();
                p2.Close();
                File.WriteAllText(lokiPath + "\\" + Globals.lfile + ".txt", a);
            } else
            {
                //p2.StartInfo.UseShellExecute = true;
                //p2.StartInfo.RedirectStandardOutput = false;
                //p2.StartInfo.RedirectStandardError = false;
                p2.StartInfo.WorkingDirectory = lokiPath;
                p2.StartInfo.Arguments = args;
                p2.StartInfo.LoadUserProfile = true;
                p2.StartInfo.FileName = loki;
                p2.StartInfo.CreateNoWindow = true;
                p2.Start();
                p2.WaitForExit();
                p2.Close();
            }
        }

        private void runCmd(string args, string text)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = args;
            startInfo.CreateNoWindow = true;

            //do not add additional text in case text eq hidden
            //needed for eventlog workaround
            if (text == "hidden") {

            } else
            {
                tBoxConsole.AppendText(text + Environment.NewLine);
            }

            process.StartInfo = startInfo;
            process.Start();
        }

        public static class Globals
        {
            public static bool isOn = false;
            public static string lfile = "none";
            public static int iNetCheck = 0;
            public static int darkMode = 0;
        }

        private void lblLinkW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "https://en.wikipedia.org/wiki/Ska%C3%B0i");
        }

        public string EncryptStringRSA(string data)
        {
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                try
                {
                    rsa.ImportRSAPublicKey(Convert.FromBase64String(Properties.Resources.publicK), out _);
                    var byteData = Encoding.UTF8.GetBytes(data);
                    var encryptData = rsa.Encrypt(byteData, true);
                    return Convert.ToBase64String(encryptData);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        private void FileEncrypt(string inputFile, string password)
        {
            //generate random salt
            byte[] salt = GenerateRandomSalt();

            //create output file name
            FileStream fsCrypt = new FileStream(inputFile + ".aes", FileMode.Create);

            //convert password string to byte arrray
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            //Set Rijndael symmetric encryption algorithm
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;

            //"What it does is repeatedly hash the user password along with the salt." High iteration counts.
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Mode = CipherMode.CFB;

            // write salt to the begining of the output file, so in this case can be random every time
            fsCrypt.Write(salt, 0, salt.Length);

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);

            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            //create a buffer (1mb) so only this amount will allocate in the memory and not the whole file
            byte[] buffer = new byte[1048576];
            int read;

            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Application.DoEvents(); // -> for responsive GUI, using Task will be better!
                    cs.Write(buffer, 0, read);
                }

                // Close up
                fsIn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
            }
        }

        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    // Fille the buffer with the generated data
                    rng.GetBytes(data);
                }
            }

            return data;
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            //extract eventlogs
            string lokiPath = Convert.ToString(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\loki");
            //string loki = lokiPath + "\\" + exe;
            runCmd("/c wevtutil epl System " + lokiPath + "\\System.evtx", "### Extracting Event Logs 1/2 ###");
            runCmd("/c wevtutil epl Security " + lokiPath + "\\Security.evtx", "### Extracting Event Logs 2/2 ###");
            toolStripProgressBar1.Value = 87;
        }

        private void cBoxDark_CheckedChanged(object sender, EventArgs e)
        //dark mode
        //if global.dark == 1 set to dark
        //full dark is shit so take grey
        // rgb(38,38,38)
        {
            Globals.darkMode += 1;
            

            if (Globals.darkMode == 1)
            {
                //dark is checked
                this.BackColor = Color.FromArgb(38,38,38);
                tBoxConsole.BackColor = Color.FromArgb(38, 38, 38);
                tBoxConsole.ForeColor = Color.White;
                toolStripProgressBar1.BackColor = Color.FromArgb(38, 38, 38);
                statusStrip1.BackColor = Color.FromArgb(38, 38, 38);
                this.ForeColor = Color.White;
                lblLinkW.BackColor = Color.FromArgb(38, 38, 38);
                gBoxOptional.ForeColor = Color.White;
                groupBox1.ForeColor = Color.White;
                lblDom.ForeColor = Color.White;
                lblDom2.ForeColor = Color.White;
                lblHost.ForeColor = Color.White;
                lblHost2.ForeColor = Color.White;
                lblLinkW.LinkColor = Color.Red;
                lblUser.ForeColor = Color.White;
                lblUser2.ForeColor = Color.White;
                lblVer.ForeColor = Color.White;
                lblVer2.ForeColor = Color.White;
                btnDebug.ForeColor = Color.Black;
                btnInetCheck.ForeColor = Color.Black;
                BtnDown.ForeColor = Color.Black;
                cBoxDark.ForeColor = Color.White;
                cBoxOfflineScan.ForeColor = Color.White;

            } else
            {
                this.BackColor = SystemColors.Control;
                lblLinkW.BackColor = SystemColors.Control;
                tBoxConsole.BackColor = SystemColors.Control;
                tBoxConsole.ForeColor = Color.Black;
                toolStripProgressBar1.BackColor = SystemColors.Control;
                statusStrip1.BackColor = SystemColors.Control;
                this.ForeColor = Color.Black;
                gBoxOptional.ForeColor = Color.Black;
                groupBox1.ForeColor = Color.Black;
                lblDom.ForeColor = Color.Black;
                lblDom2.ForeColor = Color.Black;
                lblHost.ForeColor = Color.Black;
                lblHost2.ForeColor = Color.Black;
                lblLinkW.LinkColor = Color.Blue;
                lblUser.ForeColor = Color.Black;
                lblUser2.ForeColor = Color.Black;
                lblVer.ForeColor = Color.Black;
                lblVer2.ForeColor = Color.Black;
                cBoxDark.ForeColor = Color.Black;
                cBoxOfflineScan.ForeColor = Color.Black;

                Globals.darkMode = 0;
            }
        }
    }
}
