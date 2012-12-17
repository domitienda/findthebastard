using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace FindTheBastard.Core
{
    public class ManagementObjectToWebDomainConverter
    {
        public IEnumerable<WebDomain> GetWebDomains(List<ManagementObject> managementObjects)
        {
            return managementObjects.Select(managementObject => new WebDomain
            {
                Name = managementObject["Name"].ToString(),
                CurrentConnections = int.Parse(managementObject.Properties["CurrentConnections"].Value.ToString())
            }).ToList();
        }
    }
}
