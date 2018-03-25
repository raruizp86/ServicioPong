using System;
using System.Collections.Generic;
using System.Text;

namespace ServicioPong
{
   static class ProgramEntry
    {
        static void Main(string[] args)
        {
            var unity = CreateUnityContainerAndRegisterComponents();
            // Explicitly Resolve the "root" component or components
            var program = unity.Resolve<Program>();
            program.Run();
        }
    }
}
