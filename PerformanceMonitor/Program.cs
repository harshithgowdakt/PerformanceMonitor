using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace PerformanceMonitor
{
    class Program
    {
        protected static PerformanceCounter cpuCounter;
        protected static PerformanceCounter ramCounter;
        static void Main(string[] args)
        {
            cpuCounter = new PerformanceCounter("Process", "% Processor Time",Process.GetCurrentProcess().ProcessName);


            ramCounter = new PerformanceCounter("Memory", "Available MBytes", Process.GetCurrentProcess().ProcessName);

            try
            {
                System.Timers.Timer t = new System.Timers.Timer(500);
                t.Elapsed += new ElapsedEventHandler(TimerElapsed);
                t.Start();
                Thread.Sleep(10000);
            }
            catch (Exception e)
            {
                Console.WriteLine($"catched exception {e}");
            }
            Console.ReadLine();

        }

        public static void TimerElapsed(object source, ElapsedEventArgs e)
        {
            float cpu = cpuCounter.NextValue();
            float ram = ramCounter.NextValue();
            Console.WriteLine("RAM: " + (ram /1024/1024) + " MB; CPU: " + (cpu) + " %");
        }
    }
}
