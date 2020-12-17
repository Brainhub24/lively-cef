﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace livelywpf.Helpers
{
    /// <summary>
    /// Retrieve system information:- operating system version, cpu, gpu etc..
    /// </summary>
    public static class SystemInfo
    {
        public static string GetGPUInfo()
        {
            try
            {
                using (ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController"))
                {
                    var sb = new StringBuilder();
                    foreach (ManagementObject obj in myVideoObject.Get())
                    {
                        sb.AppendLine("GPU: " + obj["Name"]);
                    }
                    return sb.ToString();
                }
            }
            catch (Exception e)
            {
                return "GPU: " + e.Message;
            }
        }

        public static List<string> GetGpu()
        {
            var result = new List<string>();
            try
            {
                using (ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController"))
                {
                    foreach (ManagementObject obj in myVideoObject.Get())
                    {
                        result.Add(obj["Name"].ToString());
                    }
                }
            }
            catch { }
            return result;
        }

        public static string GetCPUInfo()
        {
            try
            {
                using (ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor"))
                {
                    var sb = new StringBuilder();
                    foreach (ManagementObject obj in myProcessorObject.Get())
                    {
                        sb.AppendLine("CPU: " + obj["Name"]);
                    }
                    return sb.ToString();
                }
            }
            catch (Exception e)
            {
                return "CPU: " + e.Message;
            }
        }

        public static List<string> GetCpu()
        {
            var result = new List<string>();
            try
            {
                using (ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor"))
                {
                    foreach (ManagementObject obj in myProcessorObject.Get())
                    {
                        result.Add(obj["Name"].ToString());
                    }
                }
            }
            catch { }
            return result;
        }

        public static string GetOSInfo()
        {
            try
            {
                using (ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem"))
                {
                    var sb = new StringBuilder();
                    foreach (ManagementObject obj in myOperativeSystemObject.Get())
                    {
                        sb.AppendLine("OS: " + obj["Caption"] + " " + obj["Version"]);
                    }
                    return sb.ToString();
                }
            }
            catch (Exception e)
            {
                return "OS: " + e.Message;
            }
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        /// <summary>
        /// Total installed memory in Megabyte
        /// </summary>
        /// <returns></returns>
        public static long GetTotalInstalledMemory()
        {
            GetPhysicallyInstalledSystemMemory(out long memKb);
            return (memKb / 1024 );
        }
    }
}
