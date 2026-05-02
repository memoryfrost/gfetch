package main

import (
	"fmt"
	"os"
	"os/user"

	"github.com/jaypipes/ghw"

	"github.com/shirou/gopsutil/v4/cpu"
	"github.com/shirou/gopsutil/v4/host"
	"github.com/shirou/gopsutil/v4/mem"
)

var distros map[string]string = map[string]string{
	"arch":      "Arch Linux",
	"cachyos":   "CachyOS",
	"void":      "Void Linux",
	"ubuntu":    "Ubuntu",
	"linuxmint": "Linux Mint",
	"debian":    "Debian",
	"fedora":    "Fedora Linux",
	"nixos":     "NixOS",
	"gentoo":    "Gentoo Linux",
}

func main() {
	ram, _ := mem.VirtualMemory()
	cpu, _ := cpu.Info()
	gpu, _ := ghw.GPU()
	hostname, _ := os.Hostname()
	username, _ := user.Current()
	hostinfo, _ := host.Info()
	var distro string
	distro, ok := distros[hostinfo.Platform]
	if ok == false {
		distro = hostinfo.Platform
	}
	fmt.Println(`    .--.
   |o_o |
   |:_/ |
  //   \ \
 (|     | )
/'\\_   _/'\
\___)=(___/`)
	fmt.Println("---------------")
	fmt.Printf("%s@%s\n", username.Username, hostname)
	fmt.Println("OS:", distro, hostinfo.KernelArch)
	fmt.Println("Kernel:", hostinfo.KernelVersion)
	fmt.Println("CPU:", cpu[0].ModelName)
	for _, card := range gpu.GraphicsCards {
		fmt.Println("GPU:", card.DeviceInfo.Product.Name)
	}
	fmt.Println("RAM:", ram.Used/1024/1024, "MiB /", ram.Total/1024/1024, "MiB")
}
