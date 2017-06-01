using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automatedTellerMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            mainMenu run = new automatedTellerMachine.mainMenu();
            run.initializeBills();
            run.main();
        }
    }
}
