using System.Management;

namespace FindTheBastard.Core.Wmi
{
    public class WmiQueryProvider
    {
        private const string WmiRoot = "\\\\{0}\\root\\{1}";
        private const string SimpleQuery = "SELECT * FROM {0}";
        private const string QueryWithWhere = "SELECT * FROM {0} WHERE {1} = \"{2}\"";

        private readonly AuthPair _authPair;
        private readonly string _ipOrName;
        private readonly string _namespaceToQuery;

        public WmiQueryProvider(string ipOrName, string namespaceToQuery)
        {
            _ipOrName = ipOrName;
            _namespaceToQuery = namespaceToQuery;
        }

        public WmiQueryProvider(string ipOrName, string namespaceToQuery, AuthPair authPair) : this(ipOrName, namespaceToQuery)
        {
            _authPair = authPair;
        }

        public ManagementObjectCollection GetManagementObjects(string classToQuery)
        {
            return GetManagementObjects(classToQuery, string.Empty, string.Empty);
        }

        public ManagementObjectCollection GetManagementObjects(string classToQuery, string lookupKey, string instance)
        {
            var oql = string.IsNullOrEmpty(instance) ? 
                string.Format(SimpleQuery, classToQuery) : 
                string.Format(QueryWithWhere, classToQuery, lookupKey, instance);

            var searcher = new ManagementObjectSearcher(GetOpenScope(), new WqlObjectQuery(oql), null);

            return searcher.Get();
        }

        private ManagementScope GetOpenScope()
        {
            var routeString = string.Format(WmiRoot, _ipOrName, _namespaceToQuery.ToLower());

            var connectionOptions = new ConnectionOptions
            {
                Authentication = AuthenticationLevel.Packet, 
                Impersonation = ImpersonationLevel.Impersonate
            };

            if ((_authPair != null) && (!string.IsNullOrEmpty(_authPair.UserName)))
            {
                connectionOptions.Username = _authPair.UserName;
                connectionOptions.Password = _authPair.Password;
            }

            connectionOptions.EnablePrivileges = true;

            var scope = new ManagementScope(routeString, connectionOptions);
            scope.Connect();

            return scope;
        }
    }
}
