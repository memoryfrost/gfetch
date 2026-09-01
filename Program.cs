using System.Runtime.Intrinsics.X86;
using System.Management;

PlatformID platform = Environment.OSVersion.Platform;

if (platform != PlatformID.Win32NT)
{
    Console.WriteLine("Program works only on Windows");
    Environment.Exit(0);
}

switch (platform)
{
    case PlatformID.Win32NT:
        Console.WriteLine("       _.-;;-._\n" +
            "'-..-'|   ||   |\n" +
            "'-..-'|_.-;;-._|\n" +
            "'-..-'|   ||   |\n" +
            "'-..-'|_.-''-._|");
        break;
    default:
        Console.WriteLine("Unknown system");
        break;
}
Console.WriteLine("---------------");
Console.WriteLine($"{Environment.UserName}@{Environment.MachineName}");
Console.WriteLine($"OS: {Environment.OSVersion}");
Console.WriteLine($"Kernel: {Environment.OSVersion.Version} ");
switch (platform)
{
    case PlatformID.Win32NT:
        using (var mos = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor"))
            foreach (ManagementObject mo in mos.Get())
                Console.WriteLine("CPU: " + mo["Name"]);
        using (var mos = new ManagementObjectSearcher("SELECT Name FROM Win32_VideoController"))
            foreach (ManagementObject mo in mos.Get())
                Console.WriteLine("GPU: " + mo["Name"]);
        using (var mos = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize, FreePhysicalMemory FROM Win32_OperatingSystem"))
            foreach (ManagementObject mo in mos.Get())
                Console.WriteLine("RAM: " + ((ulong)mo["TotalVisibleMemorySize"] - (ulong)mo["FreePhysicalMemory"]) / 1024 + "MiB / " + (ulong)mo["TotalVisibleMemorySize"] / 1024 + "MiB");
        break;
    default:
        Console.WriteLine("CPU: Not supported on " + platform);
        Console.WriteLine("GPU: Not supported on " + platform);
        Console.WriteLine("RAM: Not supported on " + platform);
        break;
}