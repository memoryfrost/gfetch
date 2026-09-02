using System.Reflection;

class Linux
{
    public static void CpuName()
    {
        string cpuinfo = File.ReadAllText("/proc/cpuinfo");

        string cpuname = cpuinfo
            .Split('\n')
            .FirstOrDefault(x => x.StartsWith("model name"))?
            .Split(':', 2)[1]
            .Trim();

        Console.WriteLine($"CPU: {cpuname}");
    }
    public static void Ram()
    {
        string meminfo = File.ReadAllText("/proc/meminfo");

        ulong ramavailable = ulong.Parse(
            meminfo
                .Split('\n')
                .First(x => x.StartsWith("MemAvailable"))
                .Split(':')[1]
                .Trim()
                .Split(' ')[0]
        ) / 1024;
        ulong ramtotal = ulong.Parse(
            meminfo
                .Split('\n')
                .First(x => x.StartsWith("MemTotal"))
                .Split(':')[1]
                .Trim()
                .Split(' ')[0]
        ) / 1024;
        ulong ramused = ramtotal - ramavailable;
        Console.WriteLine($"RAM: {ramused}MiB / {ramtotal}MiB");
    }
    public static void OSName()
    {
        string osrelease = File.ReadAllText("/etc/os-release");

        string osname = osrelease
            .Split('\n')
            .FirstOrDefault(x => x.StartsWith("PRETTY_NAME="))?
             .Split('=', 2)[1]
             .Trim('"');

        Console.WriteLine($"OS: {osname}");
    }
    public static void GpuName()
    {
        foreach (string path in Directory.GetDirectories("/sys/bus/pci/devices"))
        {
            string classPath = Path.Combine(path, "class");
            string vendorPath = Path.Combine(path, "vendor");
            string devicePath = Path.Combine(path, "device");

            if (!File.Exists(classPath) ||
                !File.Exists(vendorPath) ||
                !File.Exists(devicePath))
                continue;

            string deviceClass = File.ReadAllText(classPath).Trim();

            if (!deviceClass.StartsWith("0x03"))
                continue;

            string vendor = File.ReadAllText(vendorPath).Trim()[2..].ToUpper();
            string device = File.ReadAllText(devicePath).Trim()[2..].ToUpper();

            string? name = FindPciDevice(vendor, device);

            if (name != null)
                Console.WriteLine($"GPU: {name}");
        }
    }

    static string? FindPciDevice(string vendorId, string deviceId)
    {
        using Stream? stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("gfetch.pci.ids");

        if (stream == null)
            return null;

        using StreamReader reader = new(stream);

        string? currentVendor = null;

        while (reader.ReadLine() is string line)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue;

            if (!char.IsWhiteSpace(line[0]))
            {
                string[] parts = line.Split(' ', 2);

                if (parts.Length == 2)
                    currentVendor = parts[0].ToUpper();

                continue;
            }

            if (currentVendor != vendorId)
                continue;

            string[] parts2 = line.Trim().Split(' ', 2);

            if (parts2.Length == 2 && parts2[0].ToUpper() == deviceId)
                return parts2[1].Trim();
        }

        return null;
    }
}