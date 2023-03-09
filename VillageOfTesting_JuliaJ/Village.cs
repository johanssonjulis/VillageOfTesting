using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageOfTesting_JuliaJ
{
    public class Village
    {
        // properties
        public RandomClass RandomCounter { get; set; }
        public DBconnectionClass DbConnection { get; set; }
        public int DaysGone { get; set; }
        public int Food { get; set; }
        public int Wood { get; set; }
        public int Metal { get; set; }
        public List<Worker> Workers_list { get; set; } = new List<Worker>();
        // Workers listan används för att lagra de workers som spelaren lägger till.
        public List<Worker> Workers_to_choose_list { get; set; } = new List<Worker>();
        // _Workers_choice listan är de workers man kan välja mellan (Lumberjack, Miner, Farmer, Builder).
        public List<Building> Buildings_list { get; set; } = new List<Building>();
        // _Buildings listan används för att lagra de byggander som är fördigbyggda (_isComplete == true).
        public List<Building> Buildings_to_choose_list { get; set; } = new List<Building>();
        // _Buildings_choice listan innehåller de olika byggnader man kan bygga (House, Woodmill, Quarry, Farm, Castle).
        public List<Building> Projects_list { get; set; } = new List<Building>();
        // _Projects listan används för att lagra de byggnader som spelaren vill bygga.

        public Village() // constructor, ge byn 10 mat samt 3 hus när spelet startar.
        {
            DaysGone = 0;
            Food = 10;
            Wood = 0;
            Metal = 0;

            DbConnection = new DBconnectionClass();
            RandomCounter = new RandomClass();

            VillageStartUp();
            // i denna funktionen läggs de tre husen till samt att vi har en lista 
            // med resterande byggnader som spelaren kan välja att bygga.
        }

        public void VillageStartUp()
        {
            for (int i = 0; i < 3; i++)
            {
                Buildings_list.Add(new Building("House", 5, 0, 3, 3, true));
            }

            //_Buildings_choice.Add(new Building("House", 5, 0, 3, 3, false));
            //_Buildings_choice.Add(new Building("Woodmill", 5, 1, 0, 5, false));
            //_Buildings_choice.Add(new Building("Quarry", 3, 5, 0, 7, false));
            //_Buildings_choice.Add(new Building("Farm", 5, 2, 0, 5, false));
            //_Buildings_choice.Add(new Building("Castle", 50, 50, 0, 50, false));

            // string name = "worker" + _Workers.Count.ToString();
            // denna fungerar ej då construktorn bara körs en gång.

            //_Workers_choice.Add(new Worker("Worker", "Lumberjack", AddWood));
            //_Workers_choice.Add(new Worker("Worker", "Miner", AddMetal));
            //_Workers_choice.Add(new Worker("Worker", "Farmer", AddFood));
            //_Workers_choice.Add(new Worker("Worker", "Builder", Build));

        }
        public int GetDaysGone() { return DaysGone; }
        public int GetFood() { return Food; }
        public int GetWood() { return Wood; }
        public int GetMetal() { return Metal; }
        public void AddWorker(int pickedWorker)
        {
            //if (IsNewWorkerAllowed()) // om detta stämmer görs nedan.
            //{
            //    _Workers.Add(_Workers_choice[pickedWorker]);
            //    // här läggs den valda arbetaren till vår lista av arbetare.
            //    // _Workers_choice är referenslistan, där spelaren kan göra sitt val.
            //
            //    if(pickedWorker == 0)
            //    {
            //
            //    }
            //}


            // Vi skickar in ett val som användaren gör för att välja 
            // vilken sorts arbetare man vill ha.

            string name = "worker" + Workers_list.Count.ToString();

            if (pickedWorker == 1 && IsNewWorkerAllowed() == true)
            {
                Workers_list.Add(new Worker(name, "Lumberjack", AddWood));
            }
            else if (pickedWorker == 2 && IsNewWorkerAllowed() == true)
            {
                Workers_list.Add(new Worker(name, "Miner", AddMetal));
            }
            else if (pickedWorker == 3 && IsNewWorkerAllowed() == true)
            {
                Workers_list.Add(new Worker(name, "Farmer", AddFood));
            }
            else if (pickedWorker == 4 && IsNewWorkerAllowed() == true)
            {
                Workers_list.Add(new Worker(name, "Builder", Build));
            }

        }
        public void Day()
        {
            FeedWorkers();

            foreach (Worker worker in Workers_list)
            {
                worker.DoWork();
            }

            BuryDead();

            DaysGone++;

        }
        public void AddFood()
        {
            if (IncreaseWork("Farm"))
            {
                Food += 15;
            }
            else
            {
                Food += 5;
            }
        }
        public void AddMetal()
        {
            if (IncreaseWork("Quarry"))
            {
                Metal += 3;
            }
            else
            {
                Metal++;
            }
        }
        public void AddWood()
        {
            if (IncreaseWork("Woodmill"))
            {
                Wood += 3;
            }
            else
            {
                Wood++;
            }
        }
        public bool IncreaseWork(string BuildingChoice)
        {
            foreach (Building building in Buildings_list)
            {
                if (building.Name == BuildingChoice)
                {
                    return true;
                }
            }
            return false;
        }
        public void Build()
        {
            if (Projects_list.Count > 0) // om listan innehåller element lägg till en arbetad dag på den första byggnaden.
            {
                Projects_list[0].DaysWorkedOn++;

                foreach (Building building in Projects_list.ToList())
                {
                    if (building.DaysWorkedOn == building.DaysToComplete)
                    {
                        building.IsComplete = true;
                        Buildings_list.Add(building);
                        Projects_list.Remove(building);
                        return;
                    }
                }
            }
        }
        public void FeedWorkers()
        {
            foreach (Worker worker in Workers_list)
            {
                if (Food > 0)
                {
                    Food--;
                    worker.IsHungry = false;
                }
                else
                {
                    worker.DaysHungry++;
                    worker.IsHungry = true;
                }

                if (worker.DaysHungry >= 40)
                {
                    worker.IsAlive = false;
                }
            }
        }
        public void BuryDead()
        {
            foreach (Worker worker in Workers_list.ToList())
            //.ToList används då den annars crashar. Lägger informationen i en cash (tillfällig lista)
            {
                if (worker.IsAlive == false)
                {
                    Workers_list.Remove(worker);
                }
                //if (Workers_list.Count == 0)
                //{
                //    Console.WriteLine("GAME OVER");
                //}
            }
        }
        public bool IsNewWorkerAllowed()
        {
            // Hur många hus som finns *2 = så många workers du får lov att ha.
            // den summan minus antalet arbetare man har -> hur många platser finns kvar för nya arbetare.
            // om 0 -> fullt inga nya accepteras.

            int totalWorkerSlots = 0;

            foreach (Building building in Buildings_list)
            {
                if (building.Name == "House" && building.IsComplete == true)
                {
                    totalWorkerSlots += 2;
                }
            }

            if (totalWorkerSlots - Workers_list.Count > 0)
            {
                return true;
            }
            return false;
        }
        public bool ResourceCalculation(Building buildingToCheck)
        {
            if (Wood >= buildingToCheck.WoodCost && Metal >= buildingToCheck.MetalCost)
            // här kollar vi så att vi har råd och bygga det vi vill.
            // Om byn har så mycket wood &/eller metal som den valda byggnaden kostar görs nedan:
            {
                Wood -= buildingToCheck.WoodCost;
                Metal -= buildingToCheck.MetalCost;
                return true;
                // Här sker betalningen för byggnaden.
            }
            return false;
            // Om byn ej har tillräckligt med tillgångar returnas false och byggnaden byggs ej.
        }
        public void AddProject(int chosenProject) // funktion för att lägga till den valda byggnaden i listan av projekt.
        {
            if (chosenProject == 1)
            {
                if (Wood >= 5)
                {
                    Projects_list.Add(new Building("House", 5, 0, 0, 3, false));
                    Wood -= 5;
                }
            }
            else if (chosenProject == 2)
            {
                if (Wood >= 5 && Metal >= 1)
                {
                    Projects_list.Add(new Building("Woodmill", 5, 1, 0, 5, false));
                    Wood -= 5;
                    Metal -= 1;
                }
            }
            else if (chosenProject == 3)
            {
                if (Wood >= 3 && Metal >= 5)
                {
                    Projects_list.Add(new Building("Quarry", 3, 5, 0, 7, false));
                    Wood -= 3;
                    Metal -= 5;
                }
            }
            else if (chosenProject == 4)
            {
                if (Wood >= 5 && Metal >= 2)
                {
                    Projects_list.Add(new Building("Farm", 5, 2, 0, 5, false));
                    Wood -= 5;
                    Metal -= 2;
                }
            }
            else if (chosenProject == 5)
            {
                if (Wood >= 50 && Metal >= 50)
                {
                    Projects_list.Add(new Building("Castle", 50, 50, 0, 50, false));
                    Wood -= 50;
                    Metal -= 50;
                }
            }
        }
        public bool Win()
        {
            foreach (var building in Buildings_list)
            {
                if (building.Name.Equals("Castle"))
                {
                    return true;
                }
            }
            return false;
        }

        //VG-DEL
        public void AddRandomWorker()
        {
            AddWorker(RandomCounter.RandomInt());
            //RandomCounter är för att kunna använda randomInt-siffran i RandomClass-klassen.
        }
        public void SaveProgress()
        {
            DbConnection.Save(this);
            //this är för att spara denna versionen av village
        }
        public void LoadProgress()
        {
            //return DbConnection.Db_Food;
            Wood = DbConnection.GetWood();
        }

    }
}
