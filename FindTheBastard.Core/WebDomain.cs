namespace FindTheBastard.Core
{
    public class WebDomain
    {
        public string Name { get; set; }
        public int CurrentConnections { get; set; }

        public WebDomain()
        {
            
        }

        public WebDomain(string name, int currentConnections)
        {
            Name = name;
            CurrentConnections = currentConnections;
        }
    }
}
