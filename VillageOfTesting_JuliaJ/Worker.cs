using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageOfTesting_JuliaJ
{
    public class Worker
    {
        public delegate void DoWorkDelegate();

        public DoWorkDelegate Addresource { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public bool IsHungry { get; set; }
        public bool IsAlive { get; set; }
        public int DaysHungry { get; set; }


        public Worker(string name, string occupation, DoWorkDelegate doWorkDelegate)
        {
            Name = name;
            Occupation = occupation;
            IsHungry = false;
            IsAlive = true;
            DaysHungry = 0;
            Addresource = doWorkDelegate;
        }

        public void DoWork()
        {
            // om du inte är hungrig - arbetar du.
            if (IsHungry == false)
            {
                Addresource();
                // _addresource() är en delegate.
                // _addresource är en variabel som innehåller en funktion.
            }
        }
    }
}
