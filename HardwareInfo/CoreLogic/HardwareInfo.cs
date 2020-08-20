using System;
using System.Management;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace HardwareInfo
{
    public class HardwareInfo
    {

        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        static string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }
        static string GetMemoryType(int MemoryType)
        {
            switch (MemoryType)
            {
                case 20:
                    return "DDR";
                case 21:
                    return "DDR-2";
                default:
                    if (MemoryType == 0 || MemoryType > 22)
                        return "DDR-3";
                    else
                        return "Unknown";
            }
        }
        public static SystemHardwareInfo Get()
        {
            SystemHardwareInfo info = new SystemHardwareInfo();

            #region GetCpu
                var searcher = new ManagementObjectSearcher("select * from Win32_Processor");
                foreach (var obj in searcher.Get())
                {
                    info.CpuName = obj["Name"].ToString();
                    info.CpuModel = obj["ProcessorType"].ToString();
                }
            #endregion

            #region GetHdd
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                long sizeSum = 0;
                foreach (var drive in allDrives)
                {
                    if (drive.IsReady)
                        sizeSum += drive.TotalSize;
                }
                info.HddStorage = SizeSuffix(sizeSum);
            #endregion

            #region GetOsVersion
                searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
                foreach (var obj in searcher.Get())
                {
                    info.WindowsVersion = obj["Version"].ToString();
                }
            #endregion

            #region GetRam
                searcher = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
                foreach (var obj in searcher.Get())
                {
                    info.RamStorage = obj["FormFactor"].ToString();
                    info.RamModel = GetMemoryType(Int32.Parse(obj["MemoryType"].ToString()));

                }
            #endregion

            return info;
        }
    }
}
