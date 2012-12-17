using System;
using System.Linq;
using FindTheBastard.Core;

namespace FindTheBastard.ConsoleUI
{
    public class ExecFlowController
    {
        private const string Localhost = "localhost";
        private const string NameSpace = "CIMV2";
        private const string Class = "Win32_PerfFormattedData_W3SVC_WebService";

        public void TryToRunNormalFlow(ParsingArgsResult options, ConsoleVisualization consoleVisualization)
        {
            try
            {
                Console.WriteLine(string.Format("Requesting information to {0}", options.Server));
                RunNormalFlow(options, consoleVisualization);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void RunNormalFlow(ParsingArgsResult options, ConsoleVisualization consoleVisualization)
        {
            AuthPair authPair = null;
            var serverToConnect = Localhost;

            if (options.IsDataIncluded)
            {
                authPair = new AuthPair { UserName = options.UserName, Password = options.Password };
                serverToConnect = options.Server;
            }

            var wmiQueryProvider = new Core.Wmi.WmiQueryProvider(serverToConnect, NameSpace, authPair);
            var managementCollection = wmiQueryProvider.GetManagementObjects(Class);
            var wmiQueryFilter = new Core.Wmi.WmiCollectionFilter(managementCollection);
            var all = wmiQueryFilter.GetAll();
            var converter = new ManagementObjectToWebDomainConverter();
            var result = converter.GetWebDomains(all);

            consoleVisualization.Run(result.OrderByDescending(wd => wd.CurrentConnections).Take(options.Number));
        }
    }
}
