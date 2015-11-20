using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace System.Robots
{
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();
                x.UseLog4Net();
                x.Service<ServiceRunner>();
                x.SetDescription("Service");
                x.SetDisplayName("ServiceDisplayName");
                x.SetServiceName("ServiceName");
                x.EnablePauseAndContinue();
            });
        }
    }
}
