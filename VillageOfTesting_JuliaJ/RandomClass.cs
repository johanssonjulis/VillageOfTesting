using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageOfTesting_JuliaJ
{
    public class RandomClass
    {
        public virtual int RandomInt()
        {
            Random random = new Random();
            int randomInt = random.Next(0, 4);
            return randomInt;
        }
    }
}
