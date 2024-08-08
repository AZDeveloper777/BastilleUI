using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BastilleDotNet.Services
{
public static class BastilleMgmtService
    {
        /// <summary>
        /// Checks if BastilleBSD is installed on the system by attempting to execute the 'bastille' command.
        /// </summary>
        /// <returns>True if BastilleBSD is installed; otherwise, false.</returns>
        public static bool IsBastilleInstalled()
        {
            try
            {
                // Initialize a new process to execute the 'bastille' command
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/sh",
                        Arguments = "-c \"command -v bastille\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                // Start the process
                process.Start();

                // Read the output of the command
                string output = process.StandardOutput.ReadToEnd();
                string errorOutput = process.StandardError.ReadToEnd();

                // Wait for the process to exit
                process.WaitForExit();

                // Check if the command found the 'bastille' binary
                return !string.IsNullOrEmpty(output) && string.IsNullOrEmpty(errorOutput);
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                Console.WriteLine($"Error checking for BastilleBSD: {ex.Message}");
                return false;
            }
        }
    }
}