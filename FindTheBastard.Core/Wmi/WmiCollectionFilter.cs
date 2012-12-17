using System.Collections.Generic;
using System.Management;

namespace FindTheBastard.Core.Wmi
{
    public class WmiCollectionFilter 
    {
        private readonly ManagementObjectCollection _managementObjectCollection;

        public WmiCollectionFilter(ManagementObjectCollection managementObjectCollection)
        {
            _managementObjectCollection = managementObjectCollection;
        }

        public List<ManagementObject> GetAll()
        {
            return GetBy(string.Empty, string.Empty);
        }

        public List<ManagementObject> GetBy(string instance, string lookupKey)
        {
            var resultList = new List<ManagementObject>();

            foreach (ManagementObject wmiObject in _managementObjectCollection)
            {
                if (string.IsNullOrEmpty(instance))
                    resultList.Add(wmiObject);
                else
                {
                    if (wmiObject.Properties[lookupKey].Value.ToString().ToLower() == instance.ToLower())
                        resultList.Add(wmiObject);
                }
            }

            return resultList;
        }
    }
}
