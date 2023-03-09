using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageOfTesting_JuliaJ
{
    public class RunGame
    {
        public Village Village { get; set; }

        public RunGame()
        {
            Village = new Village();
        }

        public void Start()
        {
            do
            // det som man vill göra direkt när man startar.
            // detta görs en gång först, sedan kollar den while-loopen om den stämmer.
            {
                if (Village.Workers_list.Count == 0 && Village.DaysGone > 0)
                {
                    Console.WriteLine("You lost!");
                    break;
                }
                else
                {
                    Console.Clear();
                    GameWindow();
                    Console.WriteLine("Choose action:");
                    Console.WriteLine();
                    Console.WriteLine("1. Add worker.");
                    Console.WriteLine("2. Add building.");
                    Console.WriteLine("3. Done for one day!");
                    StartMenu(int.Parse(Console.ReadLine()));
                }

            } while (!Village.Win());
            Console.WriteLine($"You win! It took you {Village.DaysGone} days.");

            if (Village.Workers_list.Count == 0 && Village.DaysGone > 0)
            {
                Console.WriteLine("");
            }
        }

        public void StartMenu(int number)
        {
            switch (number)
            {
                case 1:
                    PlayerAddWorker();
                    Village.AddWorker(int.Parse(Console.ReadLine()));
                    break;
                case 2:
                    PlayerAddBuilding();
                    Village.AddProject(int.Parse(Console.ReadLine()));
                    break;
                case 3:
                    Village.Day();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }

        public void PlayerAddWorker()
        {
            Console.Clear();
            Console.WriteLine("Choose worker:");
            Console.WriteLine();
            Console.WriteLine("1. Lumberjack");
            Console.WriteLine("2. Miner");
            Console.WriteLine("3. Farmer");
            Console.WriteLine("4. Builder");
        }

        public void PlayerAddBuilding()
        {
            Console.Clear();
            Console.WriteLine("Choose building:");
            Console.WriteLine();
            Console.WriteLine("1. House");
            Console.WriteLine("2. Woodmill");
            Console.WriteLine("3. Quarry");
            Console.WriteLine("4. Farm");
            Console.WriteLine("5. Castle");
        }

        public void GameWindow()
        {
            Console.WriteLine("\n" +
                $"Food {Village.GetFood()}\n" +
                $"Wood: {Village.GetWood()}\n" +
                $"Metal: {Village.GetMetal()}\n" +
                "------------------------------------");
            Console.WriteLine($"\n" +
                $"Builders: {TotalWorker("Builder")}         House: {TotalBuilding("House")}\n" +
                $"Farmer: {TotalWorker("Farmer")}           Farm: {TotalBuilding("Farm")}\n" +
                $"Lumberjack: {TotalWorker("Lumberjack")}       Woodmill: {TotalBuilding("Woodmill")}\n" +
                $"Miner: {TotalWorker("Miner")}            Quarry: {TotalBuilding("Quarry")}\n" +
                $"\nDays gone: {Village.DaysGone}\n" +
                "------------------------------------\n");
            Console.WriteLine("\n" +
                $"Project House: {TotalProject("House")}\n" +
                $"Project Farm: {TotalProject("Farm")}\n" +
                $"Project Woodmill: {TotalProject("Woodmill")}\n" +
                $"Project Quarry: {TotalProject("Quarry")}\n" +
                $"Project Castle: {TotalProject("Castle")}\n" +
                "------------------------------------\n");
        }

        public int TotalWorker(string occupation)
        {
            int sum = 0;
            foreach (Worker work in Village.Workers_list)
            {
                if (work.Occupation == occupation)
                { sum++; }
            }
            return sum;
        }

        public int TotalBuilding(string name)
        {
            int sum = 0;
            foreach (var building in Village.Buildings_list)
            {
                if (building.Name == name)
                { sum++; }
            }
            return sum;
        }

        public int TotalProject(string name)
        {
            int sum = 0;
            foreach (var building in Village.Projects_list)
            {
                if (building.Name == name)
                { sum++; }
            }
            return sum;
        }
    }
}
