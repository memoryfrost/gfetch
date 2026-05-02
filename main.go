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

var systems map[string]string = map[string]string{
	"arch":      "Arch Linux",
	"cachyos":   "CachyOS",
	"void":      "Void Linux",
	"ubuntu":    "Ubuntu",
	"linuxmint": "Linux Mint",
	"debian":    "Debian",
	"fedora":    "Fedora Linux",
	"nixos":     "NixOS",
	"gentoo":    "Gentoo Linux",
	"darwin":    "MacOS",
}

func main() {
	ram, _ := mem.VirtualMemory()
	cpu, _ := cpu.Info()
	gpu, _ := ghw.GPU()
	hostname, _ := os.Hostname()
	username, _ := user.Current()
	hostinfo, _ := host.Info()
	var system string
	system, ok := systems[hostinfo.Platform]
	if ok == false {
		system = hostinfo.Platform
	}
	if system == "MacOS" {
		fmt.Println("          .:'\n" +
			"      __ :'__\n" +
			"   .'`__`-'__``.\n" +
			"  :__________.-'\n" +
			"  :_________:\n" +
			"   :_________`-;\n" +
			"    `.__.-.__.'")
	} else {
		fmt.Println("    .--.\n" +
			"   |o_o |\n" +
			"   |:_/ |\n" +
			"  //   \\ \\\n" +
			" (|     | )\n" +
			"/'\\_   _/`\\\n" +
			"\\___)=(___/")
	}
	fmt.Println("---------------")
	fmt.Printf("%s@%s\n", username.Username, hostname)
	fmt.Println("OS:", system, hostinfo.KernelArch)
	fmt.Println("Kernel:", hostinfo.KernelVersion)
	fmt.Println("CPU:", cpu[0].ModelName)
	if system != "MacOS" {
		for _, card := range gpu.GraphicsCards {
			fmt.Println("GPU:", card.DeviceInfo.Product.Name)
		}
	} else {
		fmt.Println("GPU: Not supported on MacOS")
	}
	fmt.Println("RAM:", ram.Used/1024/1024, "MiB /", ram.Total/1024/1024, "MiB")
}
