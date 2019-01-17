using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Heimdall.Overseer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.{env.EnvironmentName}.json")
                .Build();
        }
    }
}
