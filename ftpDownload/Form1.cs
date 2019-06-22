using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ftpDownload
{
    public partial class Form1 : Form
    {
        System.Diagnostics.Stopwatch sw;//= new System.Diagnostics.Stopwatch();
        private int a = 0;

        public Form1()
        {
            InitializeComponent();
            sw = new System.Diagnostics.Stopwatch();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Download();
            
            //Task.Run(() => Download());

        }

        private void Download()
        {

            try
            {
                sw.Start();
                const string url = "ftp://speedtest.tele2.net/512KB.zip";

                // Query size of the file to be downloaded
                WebRequest sizeRequest = WebRequest.Create(url);
                sizeRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                int size = (int)sizeRequest.GetResponse().ContentLength;

                progressBar1.Invoke(
                  (MethodInvoker)(() => progressBar1.Maximum = size));

                // Download the file
                WebRequest request = WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.DownloadFile;


                Stream ftpStream = request.GetResponse().GetResponseStream();
                Stream fileStream = File.Create(@"D:\file.zip");

                byte[] buffer = new byte[size];
                int read;
                double rate, timeLeft;
                while ((read = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, read);
                    int position = (int)fileStream.Position;

                    rate = fileStream.Length / sw.Elapsed.TotalSeconds;
                    timeLeft = (size - fileStream.Length) / rate;

                    label2.Invoke((MethodInvoker)(() => label2.Text = $"{rate.ToString("0.00")} bytes/sec"));
                    label4.Invoke((MethodInvoker)(() => label4.Text = $"{timeLeft.ToString()} sec"));
                    label6.Invoke((MethodInvoker)(() => label6.Text = $"{fileStream.Length.ToString()} bytes"));

                    progressBar1.Invoke((MethodInvoker)(() => progressBar1.Value = position));

                    label4.Text = sw.Elapsed.TotalSeconds.ToString();
                }
                sw.Stop();

                label4.Text = "0 sec";

                MessageBox.Show("File successfully downloaded");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
