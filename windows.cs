using System.Management;

class Windows
{
    public static void CpuName()
    {
        using (var mos = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor"))
            foreach (ManagementObject mo in mos.Get())
                Console.WriteLine("CPU: " + mo["Name"]);
    }
    public static void GpuName()
    {
        using (var mos = new ManagementObjectSearcher("SELECT Name FROM Win32_VideoController"))
            foreach (ManagementObject mo in mos.Get())
                Console.WriteLine("GPU: " + mo["Name"]);
    }
    public static void RamInfo()
    {
        using (var mos = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize, FreePhysicalMemory FROM Win32_OperatingSystem"))
            foreach (ManagementObject mo in mos.Get())
                Console.WriteLine("RAM: " + ((ulong)mo["TotalVisibleMemorySize"] - (ulong)mo["FreePhysicalMemory"]) / 1024 + "MiB / " + (ulong)mo["TotalVisibleMemorySize"] / 1024 + "MiB");
    }
}