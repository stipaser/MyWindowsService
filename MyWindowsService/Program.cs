﻿using System.ServiceProcess;


namespace MyWindowsService
{
   static class Program
   {
      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      static void Main()
      {
         ServiceBase[] ServicesToRun;
         ServicesToRun = new ServiceBase[]
         {
                new Service1()
         };
         ServiceBase.Run(ServicesToRun);
      }
   }
}
