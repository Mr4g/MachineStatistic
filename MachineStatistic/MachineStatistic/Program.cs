using MachineStatistic;

string fileName = "";
string windowWidth = "|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||";

Console.WindowWidth = windowWidth.Length;


Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
Console.WriteLine("||                                                                                   ||");
Console.WriteLine("||                                                                                   ||");
Console.WriteLine("||                                                                                   ||");
Console.WriteLine("||                        Welcome in Machine Static Calculate                        ||");
Console.WriteLine("||                                                                                   ||");
Console.WriteLine("||                                                                                   ||");
Console.WriteLine("||                                                                                   ||");
Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
Console.WriteLine("||                                                                                   ||");
Console.WriteLine("||                                                                                   ||");
Console.WriteLine("||   Automatyczne generowanie danych klawisz 'g'                                     ||");
Console.WriteLine("||                                                                                   ||");
Console.WriteLine("||                                                                                   ||");
Console.WriteLine("||   Ręczne dodwanie danych klawisz 'h'                                              ||");
Console.WriteLine("||                                                                                   ||");
Console.WriteLine("||                                                                                   ||");
Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
Console.WriteLine("");
Console.WriteLine("");

bool validDataModel = false;
while (!validDataModel)
{
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("Wybierz model dodwania danych...");

    
    var dataModel = Console.ReadLine();


    switch (dataModel)
    {
        case "g":
        case "G":
            Console.WriteLine($"Wybrałeś automatyczne generowanie dannych... {dataModel}");
            Console.WriteLine("");
            Console.WriteLine("");
            bool validEq = false;
            string eq = "";
            while (!validEq)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"Podaj numer 5-cyfrowy EQ maszyny, znajdziesz go na tabliczce znamionowej");
                eq = Console.ReadLine();
                int eqNumber;
                validEq = int.TryParse(eq, out eqNumber) && eq.Length == 5;
                if (!validEq)
                {
                    Console.WriteLine("Niepoprawny numer EQ. Podaj 5-cyfrowy numer.");
                }
            }
                Console.WriteLine($"EQ maszyny...  {eq}, od teraz już tego parametru nie można zmienić");
                Console.WriteLine("");
                Console.WriteLine("");
            bool validCUPP = false;
            string department = "";
            while (!validCUPP)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"Podaj dział na którym znajduje się maszyna...");
                department = Console.ReadLine();
                validCUPP = !string.IsNullOrEmpty(department) && department.All(c => char.IsLetter(c));
                if (!validCUPP)
                {
                    Console.WriteLine("Podaj prawidłowy dział gdzie znajduje się maszyna!");
                }
            }
            department = department.ToUpper();
            Console.WriteLine($"Dział na którym jest maszyna {department}, od teraz już tego parametru nie można zmienić");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"Nazwa Twojego pliku to: EQ{eq}_{department}.txt ");
            fileName = $"EQ{eq}_{department}.txt";
            DataGenerator.GenerateDataFile(fileName);
            validDataModel = true;
            break;
        default:
            Console.WriteLine($"Klawisz nie odpowada żadnemu modelowi danych.. Wprowadz poprawny!");
            break;
    }
}

Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("Statystyki wygenerowanego pliku...");


Statistics statisticsFromFile = new Statistics();
statisticsFromFile.CalculateUtilization(fileName);
Console.WriteLine("");
Console.WriteLine($"Utyliacja wynosi: {statisticsFromFile.Utilization:N2}");
Console.WriteLine($"OEE wynosi: {statisticsFromFile.OEE:N2}");
Console.WriteLine($"Waiting wynosi: {statisticsFromFile.Waiting:N2}");
Console.WriteLine($"Quality wynosi: {statisticsFromFile.Quality:N2}");
Console.WriteLine($"Parts OK wynosi: {statisticsFromFile.Passed}");
Console.WriteLine($"Parts NOK wynosi: {statisticsFromFile.Failed}");
Console.WriteLine($"Parts Total wynosi: {statisticsFromFile.TotalParts}");





//var Stern1 = new MachineCUPP("85454", "CUPP");

//while (true)
//{
//    Console.WriteLine("Podaj status maszyny : ");
//    var input = Console.ReadLine();

//    if (input == "q")
//    {
//        break;
//    }

//    try
//    {
//        Stern1.ManualGenerateDataFile(input);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine(ex.ToString());
//    }


//}






//DataGenerator Stern1 = new DataGenerator();
//DataGenerator.GenerateDataFile();
//Console.WriteLine($"Plik został wygenerowany");

//string filePath = "Stern1.txt";
//Statistics statistics = new Statistics();
//statistics.CalculateUtilization(filePath);
//statistics.MostEfficient(filePath);


// Możesz teraz odczytać wartość utylizacji

//Console.WriteLine($"Utilization = {statistics.Utilization}");
//Console.WriteLine($"PartsOK = {statistics.Passed}");
//Console.WriteLine($"PartsNOK = {statistics.Failed}");
//Console.WriteLine($"Quality = {statistics.Quality}");
//Console.WriteLine($"MostEfficientPeriod = {statistics.MostEfficientPeriod}");
