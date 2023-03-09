using Moq;
using VillageOfTesting_JuliaJ;

namespace TestVillage
{
    public class test
    {
        private void DayIterator(int iterations, Village village)
        {
            for (int i = 0; i < iterations; i++)
            {
                village.Day();
            }
        }
        [Fact]
        public void AddWorker1_AddWorkers_CountWorkers()
        {
            //arrange
            Village village = new Village();

            //act - h�r skapar vi tre nya arbetare
            village.AddWorker(1);       // 0 = index
            village.AddWorker(2);
            village.AddWorker(3);

            //assert vi f�rv�ntar oss 3 workers som j�mf�r med village.lista.Count.
            //om det st�mmer s� g�r testet igenom.
            Assert.Equal(3, village.Workers_list.Count);
        }

        [Fact]
        public void AddWorker2_AddWorkerWithoutHouse_CountWorkers()
        {   //testa l�gga till en arbetare men det finns ej
            //tillr�ckligt med hus.

            //arrange
            Village village = new Village();

            //act - n�r man g�r en village f�r man automatiskt tre
            //hus. Allts� f�r vi ha max 6 arbetare n�r vi startar.
            village.AddWorker(1);
            village.AddWorker(2);
            village.AddWorker(3);
            village.AddWorker(4);
            village.AddWorker(1);
            village.AddWorker(2);
            village.AddWorker(3);

            //assert vi f�rv�ntar oss s� m�nga arbetare som det
            //�r godk�nt att ha. Den 7e ska ej r�knas med
            Assert.Equal(6, village.Workers_list.Count);
        }

        [Fact]
        public void AddWorker3_CheckOccupation_DayFunction()
        {
            //arrange
            Village village = new Village();

            //act - 2 = miner
            village.AddWorker(2);
            village.Day();

            //assert
            Assert.Equal(1, village.Metal);
        }

        [Fact]
        public void Day1_NoWorkers_DaysGone()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.Day();

            //Assert
            Assert.Equal(1, village.DaysGone);
        }

        [Fact]
        public void Day2_AddWorkers_EnoughFood()
        {
            //arrange - h�r har jag 10 mat, det g�r en dag (10-2)
            //f�r varje arbetare �ter en mat.
            Village village = new Village();

            //act
            village.AddWorker(1);
            village.AddWorker(2);
            village.Day();

            //assert
            Assert.Equal(8, village.Food);
        }

        [Fact]
        public void Day3_AddWorkers_NotEnoughFood()
        {
            //arrange
            Village village = new Village();

            //act
            village.AddWorker(2);   //2=miner
            village.AddWorker(2);
            village.AddWorker(2);
            village.AddWorker(2);
            village.AddWorker(2);
            village.AddWorker(2);

            DayIterator(50, village);

            //assert
            Assert.Equal(0, village.Food);
            Assert.Equal(10, village.Metal);
            Assert.Empty(village.Workers_list); //kollar s� att listan �r tom p� arbetare
        }

        [Fact]
        public void AddProject1_AddBuilding_CheckResources()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(1);
            village.AddWorker(3);
            village.AddWorker(4);

            DayIterator(5, village);
            village.AddProject(1);
            DayIterator(3, village);

            //Assert
            Assert.Equal(3, village.Wood);
        }

        [Fact]
        public void AddProject2_AddBuildingWithoutResources_ShouldNotWork()
        {
            //Arrange - n�r vi skapar en ny village har vi inga byggresurser
            //Allts� ska det inte g� att l�gga till byggnader i projects_list
            Village village = new Village();

            //Act
            village.AddProject(1);
            village.AddProject(2);
            village.AddProject(3);
            village.AddProject(4);
            village.AddProject(5);


            //Assert
            Assert.Empty(village.Projects_list);
        }

        [Fact]
        public void AddProject3_AddWoodmill_MoreWood()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(4); //4 = builder
            village.AddWorker(1); //1 = Lumberjack
            village.AddWorker(2); //2 = Miner 
            village.AddWorker(3); //3 = Farmer

            DayIterator(5, village); //Vi har nu fem wood
            village.AddProject(2); //L�gger till en woodmill. Wood = 0
            DayIterator(4, village); //Tar 5 dagar att bygga, s� dagen innan har vi 4 wood

            int expectedWoodBeforeWoodmill = 4;
            int actuallWoodBeforeWoodmill = village.Wood;

            village.Day(); //H�r blir woodmill klar och antal wood �kar med 2/dag.
                           //Femte dagen har vi 5 wood, sj�tte dagen har vi 2 mer = 7

            int expectedWoodAfterWoodMill = 7;
            int actuallWoodAfterWoodMill = village.Wood;

            //Assert
            Assert.Equal(expectedWoodBeforeWoodmill, actuallWoodBeforeWoodmill);
            Assert.Equal(expectedWoodAfterWoodMill, actuallWoodAfterWoodMill);
        }

        [Fact]
        public void AddProject4_AddQuarry_MoreMetal()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(4); //4 = builder
            village.AddWorker(1); //1 = Lumberjack
            village.AddWorker(2); //2 = Miner 
            village.AddWorker(3); //3 = Farmer

            DayIterator(5, village); //Nu har vi fem metall
            village.AddProject(3); //L�gger till en quarry
            DayIterator(6, village); // quarry tar 7 dagar att bygga, dagen innan har vi 6 metall.

            int expectedMetalBeforeQuarry = 6;
            int actuallMetalBeforeQuarry = village.Metal;

            village.Day(); //H�r blir v�r quarry klar och antal metal �kar med 2/dag.
                           //Sjunde dagen har vi 7 metal, 8e dagen har vi 9.

            int expectedMetalAfterQuarry = 9;
            int actuallMetalAfterQuarry = village.Metal;

            //Assert
            Assert.Equal(expectedMetalBeforeQuarry, actuallMetalBeforeQuarry);
            Assert.Equal(expectedMetalAfterQuarry, actuallMetalAfterQuarry);

        }

        [Fact]
        public void AddProject5_AddFarm_MoreFood()
        {
            //Arrange
            Village village = new Village(); //food = 10

            //Act
            village.AddWorker(4); //4 = builder
            village.AddWorker(1); //1 = Lumberjack
            village.AddWorker(2); //2 = Miner 
            village.AddWorker(3); //3 = Farmer

            DayIterator(5, village); //Nu har vi fem wood, fem metall och 15 food.
            village.AddProject(4); //L�gger till en farm. Kostar fem wood och 2 metall = 0 wood 3 metall
            DayIterator(4, village); //Farm tar 4 dagar att bygga. Dagen innan �r food = 19

            int expectedFoodBeforeFarm = 19;
            int actuallFoodBeforeFarm = village.Food;

            village.Day(); //H�r blir v�r farm klar och food �kar med 15/dag.
                           //Femte dagen har vi 19 food, sj�tte dagen har vi 34 food.
                           //Eftersom v�ra workers �ter s� har vi 30 mat.

            int expectedFoodAfterFarm = 30;
            int actuallFoodAfterFarm = village.Food;

            //Assert
            Assert.Equal(expectedFoodBeforeFarm, actuallFoodBeforeFarm);
            Assert.Equal(expectedFoodAfterFarm, actuallFoodAfterFarm);
        }

        [Fact]
        public void AddProject6_AddHouse_CompleteInRightAmountOfDays()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(3);   //3= farmer
            village.AddWorker(1);   //1= lumberjack

            DayIterator(5, village);    //nu har vi fem wood och kan betala f�r huset

            village.AddProject(1);  //l�gger till ett hus
            village.AddWorker(4);   //4 = builder
            village.AddWorker(4);   //4 = builder

            DayIterator(2, village);//tar tre dagar att bygga ett hus. Eftersom vi l�gger till
                                    //tv� builders tar det 1.5 dag.
                                    //Assert
            Assert.Empty(village.Projects_list); //projektlistan ska vara tom n�r projektet �r klart.
        }

        [Fact]
        public void Worker1_HungryWorker_DoNotWork()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(1);   //1 = lumberjack
            DayIterator(11, village); //12e dagen. isHungry = true;
            village.Day();  //h�r b�r han ej h�mta mer wood. Wood=10

            //Assert
            Assert.Equal(10, village.Wood);
        }

        [Fact]
        public void Worker2_WorkerHungryFor40Days_IsGone()
        {
            //Arrange
            Village village = new Village(); //f�r 10 mat vid uppstart

            //Act
            village.AddWorker(1); //lumberjack
            DayIterator(50, village); //Varit hungrig i 40 dagar.

            //Assert
            Assert.Empty(village.Workers_list);
        }

        [Fact]
        public void Worker3_FeedWorkerIsAliveFalse_DoNotWork()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(1); //1=lumberjack
            DayIterator(50, village);

            //Assert
            Assert.Empty(village.Workers_list);
        }

        [Fact]
        public void BuryDead1_RemoveDeadWorkers()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(1); //lumberjack
            village.AddWorker(2); //miner
            village.AddWorker(4); //builder

            DayIterator(50, village); //varit hungriga i mer �n 40 dagar

            //Assert
            Assert.Empty(village.Workers_list);
        }

        [Fact]
        public void RunAllFunctions_BuildCastle_YouWin()
        {
            //K�r spelet, en combo av AddWorker, AddProject och Day. Bygg ett slott
            //Arrange
            Village village = new Village(); //+10 mat +3 hus

            //Act
            village.AddWorker(4);//Builder
            village.AddWorker(1);//lumberjack
            village.AddWorker(2);//Miner
            village.AddWorker(3);//Farmer

            DayIterator(50, village);
            village.AddProject(5);
            DayIterator(50, village);

            //Assert
            Assert.Equal(100, village.DaysGone);
        }

        [Fact]
        public void TestAddRandomWorker_MockTheRandomGenerator_RunDayFunction()
        {
            //Arrange
            //Vi skapar en ny klass av RandomClass som finns i village,
            //eftersom den �r random och vi vill kunna sj�lva kontrollera
            //och s�tta ett v�rde - f�r att kunna testa klassen.

            Village village = new Village();
            Mock<RandomClass> mock = new Mock<RandomClass>();

            village.RandomCounter = mock.Object;
                                    //mock.Object �r den som best�mmer att
                                    //addrandomworker

            //
            mock.Setup(mock => mock.RandomInt()).Returns(1);
            //Act
            village.AddRandomWorker();
            village.Day();

            //Assert
            Assert.Equal(1, village.Wood);
        }

        [Fact]
        public void TestLoadProgress_ReturnFunctionsInDBconnection_WeChooseTheValues()
        {
            //Arrange
            Village village = new();
            Mock<DBconnectionClass> mock = new Mock<DBconnectionClass>();

            village.DbConnection = mock.Object;

            mock.Setup(mock => mock.GetWood()).Returns(100);

            //Act
            village.LoadProgress();

            //Assert
            Assert.Equal(100, village.Wood);
        }

    }
}