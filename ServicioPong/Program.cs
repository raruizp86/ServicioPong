using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioPong
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServer server = new WebServer(7000, "web");
           
          
            server.Start();
            server.Listen();
        }
    }
}
