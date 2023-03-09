using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageOfTesting_JuliaJ
{
    public class DBconnectionClass
    {
        public virtual int GetWood()
        {
            return 3;
        }
        public void Save(Village village)
        {
            
        }
        
        public void Load() //denna skall mockas senare. En klass som ej ger tillbaka något.
                                   //men från databasen ska den ladda in värden till sina egna variabler.
        {
            
            
        }
    }
}
