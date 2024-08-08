using System;
using BastilleDotNet.Models;
using BastilleDotNet.Services;

namespace BastilleDotNet.ConsoleTestermespace
{
    class Program
    {
        static void Main(string[] args)
        {
            var results = BastilleDotNet.Services.ContainerService.ListContainers();
            foreach(ContainerInfo ci in results)
            {
                Console.WriteLine(ci.Name + " " + ci.Status + " " + ci.IPAddress);
            }
            
        }
    }
}