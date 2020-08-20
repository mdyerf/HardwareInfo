using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace HardwareInfo
{
    public partial class SystemInfo : Form
    {
        public SystemInfo()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label1.Text = "Loading...";

            var info = HardwareInfo.Get();

            string url = this.textBox1.Text;

            WebClient wc = new WebClient();

            wc.Headers["ContentType"] = "application/json";

            wc.QueryString.Add("UserName", this.textBox2.Text);
            wc.QueryString.Add("WindowsVersion", info.WindowsVersion);
            wc.QueryString.Add("CpuName", info.CpuName);
            wc.QueryString.Add("CpuModel", info.CpuModel);
            wc.QueryString.Add("RamStorage", info.RamStorage);
            wc.QueryString.Add("RamModel", info.RamModel);
            wc.QueryString.Add("HddStorage", info.HddStorage);

            ServicePointManager.ServerCertificateValidationCallback =
                (senderX, certificate, chain, sslPolicyErrors) => { return true; };

            try
            {
                var data = wc.UploadValues(url, "POST", wc.QueryString);

                this.label1.Text = "Done";
            }
            catch(Exception)
            {
                this.label1.Text = "somthing went wrong";
            }

        }
    }
}
