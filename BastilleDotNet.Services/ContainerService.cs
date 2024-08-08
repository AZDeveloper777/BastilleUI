using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BastilleDotNet.Models;
using System.Diagnostics;

namespace BastilleDotNet.Services
{
    public class ContainerService
    {
        public static List<ContainerInfo> ListContainers()
        {
            List<ContainerInfo> containers = new List<ContainerInfo>();
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/sh",
                    Arguments = "-c \"bastille list\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            string errorOutput = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if(errorOutput != "" || errorOutput != null)
            {
                throw new Exception(errorOutput);
            }

            // Process the result and populate the container list
            // Example result parsing, assuming `bastille list` returns a simple list of container names and statuses
            var lines = result.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                // Assuming the line format: "CONTAINER_NAME  STATUS  IP_ADDRESS"
                var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 3)
                {
                    containers.Add(new ContainerInfo
                    {
                        Name = parts[0],
                        Status = parts[1],
                        IPAddress = parts[2]
                    });
                }
            }

            return containers;
        }
    }
}