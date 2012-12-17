using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using FindTheBastard.Core;

namespace FindTheBastard.ConsoleUI
{
    public class ConsoleVisualization
    {
        private const int Padding = 50;
        private const string DomainName = "Domain name";
        private const string Connections = "Connections";
        private const char UiChar = '=';

        public void Run(IEnumerable<WebDomain> domains)
        {
            Console.Clear();

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(DomainName);
            stringBuilder.Append(Connections.PadLeft(Padding) + "\r\n");
            stringBuilder.AppendLine("".PadRight(Padding + Connections.Length, UiChar));

            foreach (var webDomain in domains)
            {
                stringBuilder.Append(webDomain.Name.PadRight(Padding));
                stringBuilder.Append(webDomain.CurrentConnections.ToString(CultureInfo.InvariantCulture) + "\r\n");
            }

            Console.Write(stringBuilder.ToString());
        }
    }
}
