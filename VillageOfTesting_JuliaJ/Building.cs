using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageOfTesting_JuliaJ
{
    public class Building
    {
        public string Name { get; set; }
        public int WoodCost { get; set; }
        public int MetalCost { get; set; }
        public int DaysWorkedOn { get; set; }
        public int DaysToComplete { get; set; }
        public bool IsComplete { get; set; }

        public Building(string name, int woodCost, int metalCost, int daysWorkedOn, int daysToComplete, bool isComplete)
        {
            Name = name;
            WoodCost = woodCost;
            MetalCost = metalCost;
            DaysWorkedOn = daysWorkedOn;
            DaysToComplete = daysToComplete;
            IsComplete = isComplete;

        }
    }
}
