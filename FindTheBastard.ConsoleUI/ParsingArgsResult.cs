using CommandLine;

namespace FindTheBastard.ConsoleUI
{
    public class ParsingArgsResult
    {
        [Option("u", "username")]
        public string UserName { get; set; }

        [Option("p", "password")]
        public string Password { get; set; }

        [Option("s", "server")]
        public string Server { get; set; }

        [Option("n", "number")]
        public int Number { get; set; }

        public bool IsDataIncluded
        {
            get
            {
                return !string.IsNullOrEmpty(UserName) &&
                       !string.IsNullOrEmpty(Password) &&
                       !string.IsNullOrEmpty(Server);
            }
        }
    }
}
