using System.Management;
using System.Runtime.Intrinsics.X86;

internal class Gfetch
{
    private static void Main(string[] args)
    {
        PlatformID platform = Environment.OSVersion.Platform;
        //string osrelease = "";
        //string cpuinfo = "";
        //string meminfo = "";

        //if (platform == PlatformID.Unix)
        //{
        //    osrelease = File.ReadAllText("/etc/os-release");
        //    //cpuinfo = File.ReadAllText("/proc/cpuinfo");
        //    //meminfo = File.ReadAllText("/proc/meminfo");
        //}

        //if (platform != PlatformID.Win32NT)
        //{
        //    Console.WriteLine("Program works only on Windows");
        //    Environment.Exit(0);
        //}

        switch (platform)
        {
            case PlatformID.Win32NT:
                Console.WriteLine("       _.-;;-._\n" +
                    "'-..-'|   ||   |\n" +
                    "'-..-'|_.-;;-._|\n" +
                    "'-..-'|   ||   |\n" +
                    "'-..-'|_.-''-._|");
                break;
            case PlatformID.Unix:
                Console.WriteLine("    .--.\n" +
                    "   |o_o |\n" +
                    "   |:_/ |\n" +
                    "  //   \\ \\\n" +
                    " (|     | )\n" +
                    "/'\\_   _/`\\\n" +
                    "\\___)=(___/");
                break;
            default:
                Console.WriteLine("Unknown system");
                break;
        }
        Console.WriteLine("---------------");
        Console.WriteLine($"{Environment.UserName}@{Environment.MachineName}");

        switch (platform)
        {
            case PlatformID.Win32NT:
                Console.WriteLine($"OS: {Environment.OSVersion}");
                break;
            case PlatformID.Unix:
                Linux.OSName();
                break;
        }
        Console.WriteLine($"Kernel: {Environment.OSVersion.Version} ");
        switch (platform)
        {
            case PlatformID.Win32NT:
                Windows.CpuName();
                Windows.GpuName();
                Windows.RamInfo();
                break;
            case PlatformID.Unix:
                Linux.CpuName();
                //Console.WriteLine("GPU: Not supported on " + platform);
                Linux.GpuName();
                Linux.Ram();
                break;
            default:
                Console.WriteLine("CPU: Not supported on " + platform);
                Console.WriteLine("GPU: Not supported on " + platform);
                Console.WriteLine("RAM: Not supported on " + platform);
                break;
        }
    }
}