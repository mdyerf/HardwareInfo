using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net;

namespace HardwareInfo
{
    public class SystemHardwareInfo
    {
        public string WindowsVersion { get; set; }
        public string CpuName { get; set; }
        public string CpuModel { get; set; }
        public string RamModel { get; set; }
        public string RamStorage { get; set; }
        public string HddStorage { get; set; }
    }
}
