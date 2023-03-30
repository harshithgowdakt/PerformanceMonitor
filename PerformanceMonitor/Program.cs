using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace PerformanceMonitor
{
    class Program
    { 
        static void Main(string[] args)
        {
            // Create a new PerformanceCounter for the target process's CPU usage
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
            var memoryCounter = new PerformanceCounter("Process", "Working Set - Private", Process.GetCurrentProcess().ProcessName);

            var timer = new System.Timers.Timer(500);
            timer.Elapsed += (sender, e) =>
            {
                Thread.Sleep(1000);

                float cpuUsage = cpuCounter.NextValue() / Environment.ProcessorCount;
                float memoryUsage = memoryCounter.NextValue() / (1024 * 1024); // convert to megabytes

                Console.WriteLine($" {Process.GetCurrentProcess().ProcessName}   CPU : {cpuUsage:F2}%   MEM :  {memoryUsage:F2} MB");
            };

            timer.Start();

            // Wait for user input before exiting
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            // Stop the timer
            timer.Stop();
            timer.Dispose();
        }
    }
}
