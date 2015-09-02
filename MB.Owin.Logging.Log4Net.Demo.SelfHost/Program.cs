using System;
using Microsoft.Owin.Hosting;

namespace MB.Owin.Logging.Log4Net.Demo.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            const string baseUri = "http://localhost:8000";

            Console.WriteLine("Starting web Server...");
            WebApp.Start<Startup>(baseUri);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
            Console.ReadLine();
        }
    }
}
