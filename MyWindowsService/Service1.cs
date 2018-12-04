using System;
using System.ServiceProcess;
using System.Timers;
using System.IO;

namespace MyWindowsService
{
   public partial class Service1 : ServiceBase
   {
      public Service1()
      {
         InitializeComponent();
      }

      protected override void OnStart(string[] args)
      {
         Timer timer = new Timer();
         timer.Interval = 10000; //number in milliseconds  : 3600000 = 1 hour
         var onElapsed = new ElapsedEventHandler((sender, eventArgs) => WriteToFile("Service is Recall at " + DateTime.Now));
         timer.Elapsed += onElapsed;
         WriteToFile("Service is Started at " + DateTime.Now);
         timer.Start();
      }

      protected override void OnStop() => WriteToFile("Service is Stopped at " + DateTime.Now);

      
      private static void WriteToFile(string message)
      {
         string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
         if (!Directory.Exists(path))
         {
            Directory.CreateDirectory(path);
         }
         string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
         if (!File.Exists(filepath))
         {
            // Create a file to write to.   
            using (StreamWriter sw = File.CreateText(filepath))
            {
               sw.WriteLine(message);
            }
         }
         else
         {
            using (StreamWriter sw = File.AppendText(filepath))
            {
               sw.WriteLine(message);
            }
         }

         GC.Collect();
      }
   }
}
