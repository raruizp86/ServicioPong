using System;

namespace ServicioPong
{
    class Program
    {
        static void Main(string[] args)
        {
            var unity = CreateUnityContainerAndRegisterComponents();
            // Explicitly Resolve the "root" component or components
            var program = unity.Resolve<Program>();
            program.Run();
            WebServer server = new WebServer(7000, "web");
            server.Start();
            server.Listen();
        }
    }
}
