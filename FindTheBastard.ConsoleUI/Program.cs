using System;
using CommandLine;

namespace FindTheBastard.ConsoleUI
{
    /// <summary>
    /// FindTheBastard
    /// Copyright (C) Domitienda.com, Hosting Valencia S.L.U
    /// Author: Jacob Mendoza (jacob.mendoza at domitienda.com)
    /// 
    /// Little utility that performs WMI queries in order to retrieve an ordered collection
    /// of website processes hosted by an IIS webserver. Makes it easy to find the process
    /// which is consuming more connections. Even though connections can be an important parameter,
    /// this can be a useless metric depending of the conditions.
    /// </summary>
    class Program
    {
        private const int DefaultNumberOfResults = 15;
        private const string VersionNumberString = "v1.0"; //Using a fixed string by now

        static void Main(string[] args)
        {
            var options = new ParsingArgsResult { Number = DefaultNumberOfResults };
            
            var executionController = new ExecFlowController();
            ICommandLineParser parser = new CommandLineParser();
            var consoleVisualization = new ConsoleVisualization();

            if (parser.ParseArguments(args, options))
            {
                executionController.TryToRunNormalFlow(options, consoleVisualization);
            }
            else
            {
                Console.WriteLine(string.Format("FindTheBastard {0}", VersionNumberString));
                Console.WriteLine("Copyright (C) Domitienda.com, Hosting Valencia S.L.U");
                Console.WriteLine("Usage: -s\t server [optional]");
                Console.WriteLine("Usage: -u\t username [optional]");
                Console.WriteLine("Usage: -p\t password [optional]");
                Console.WriteLine(string.Format("Usage: -n\t number of results [optional, default:{0}]", DefaultNumberOfResults));
            }
        }
    }
}
