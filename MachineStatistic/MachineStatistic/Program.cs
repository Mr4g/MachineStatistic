using System.ComponentModel.DataAnnotations;
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
var dataModel = "";
bool validDataModel = false;
MachineCUPP machineCUPP = null;

while (!validDataModel)
{
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("Wybierz model dodwania danych...");
    bool vlidationEq = false;
    int eqVlidation = 0;
    string departmentVlidation = "";
    bool vlidationDepartment = false;
    dataModel = Console.ReadLine();
    switch (dataModel)
    {
        case "g":
        case "G":
            Console.WriteLine($"Wybrałeś automatyczne generowanie dannych... {dataModel}");
            Console.WriteLine("");
            Console.WriteLine("");

            while (!vlidationEq)
            {
                Console.WriteLine($"Podaj numer 5-cyfrowy EQ maszyny... ");
                string eq = Console.ReadLine();
                try
                {
                    eqVlidation = Vlidation.VlidationEq(eq);
                    vlidationEq = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
            Console.WriteLine($"EQ maszyny...  {eqVlidation}, od teraz już tego parametru nie można zmienić");
            Console.WriteLine("");
            Console.WriteLine("");

            while (!vlidationDepartment)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"Podaj dział na którym znajduje się maszyna...");
                string department = Console.ReadLine();
                try
                {
                    departmentVlidation = Vlidation.VlidationDepartment(department);
                    vlidationDepartment = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd {ex.Message}");
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
            Console.WriteLine($"Dział maszyny...  {departmentVlidation}, od teraz już tego parametru nie można zmienić");
            Console.WriteLine("");
            Console.WriteLine("");
            fileName = $"EQ{eqVlidation}{departmentVlidation}.txt";
            DataGenerator.GenerateDataFile(fileName);
            validDataModel = true;
            break;
        case "h":
        case "H":
            Console.WriteLine($"Wybrałeś ręczne generowanie dannych... {dataModel}");
            Console.WriteLine("");
            Console.WriteLine("");
            while (!vlidationEq)
            {
                Console.WriteLine($"Podaj numer 5-cyfrowy EQ maszyny... ");
                string eq = Console.ReadLine();
                try
                {
                    eqVlidation = Vlidation.VlidationEq(eq);
                    vlidationEq = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
            Console.WriteLine($"EQ maszyny...  {eqVlidation}, od teraz już tego parametru nie można zmienić");
            Console.WriteLine("");
            Console.WriteLine("");

            while (!vlidationDepartment)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"Podaj dział na którym znajduje się maszyna...");
                string department = Console.ReadLine();
                try
                {
                    departmentVlidation = Vlidation.VlidationDepartment(department);
                    vlidationDepartment = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd {ex.Message}");
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
            Console.WriteLine($"Dział maszyny...  {departmentVlidation}, od teraz już tego parametru nie można zmienić");
            Console.WriteLine("");
            Console.WriteLine("");
            validDataModel = true;
            machineCUPP = new MachineCUPP(eqVlidation, departmentVlidation);
            break;
        default:
            Console.WriteLine($"Klawisz nie odpowada żadnemu modelowi danych.. Wprowadz poprawny!");
            break;
    }
}

if (dataModel == "g")
{
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
    Console.ReadKey();
}
else if (dataModel == "h")
{
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine($"Prawidłowo statusy to true lub false, 0 lub 1...");

    while (true)
    {
        var count = 0;
        Console.WriteLine("");
        Console.WriteLine("");

        while (true)
        {
            Console.WriteLine("\nPodaj status Producing ... ");
            string status = Console.ReadLine();
            HandleStatus(status, machineCUPP);

            Console.WriteLine("\nPodaj status Waiting ... ");
            status = Console.ReadLine();
            HandleStatus(status, machineCUPP);

            Console.WriteLine("\nPodaj status PartsOk ... ");
            status = Console.ReadLine();
            HandleStatus(status, machineCUPP);

            Console.WriteLine("\nPodaj status PartsNok ... ");
            status = Console.ReadLine();
            HandleStatus(status, machineCUPP);
            if (machineCUPP.machineStatus.Count == 4)
            {
                try
                {
                    machineCUPP.StatusValidation();
                }
                catch (Exception ex)
                {
                    machineCUPP.ClearStatus();
                    Console.WriteLine(ex);
                }
                if (!machineCUPP.errorValidation)
                {
                    count++;
                    Console.WriteLine($"Dodano prawidłowo {count} status maszyny.. ");
                }
            }
            else
            {
                machineCUPP.ClearStatus();
                Console.WriteLine($"Podano niepoprawne statusy proszę ponowić próbe dodania statusów {machineCUPP.machineStatus}");
            }
            Console.WriteLine($"");
            Console.WriteLine($"Jeśli chcesz przerwać wciśnij 'q' jeśli chcesz kontunować to dowolny klawisz..");
            string inputExit = Console.ReadLine();
            if (inputExit == "q")
            {
                break;
            }
        }
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("Statystyki wygenerowanego pliku...");


        Statistics statisticsFromFile = new Statistics();
        statisticsFromFile.CalculateUtilization(machineCUPP.FileName);
        Console.WriteLine("");
        Console.WriteLine($"Utyliacja wynosi: {statisticsFromFile.Utilization:N2}");
        Console.WriteLine($"OEE wynosi: {statisticsFromFile.OEE:N2}");
        Console.WriteLine($"Waiting wynosi: {statisticsFromFile.Waiting:N2}");
        Console.WriteLine($"Quality wynosi: {statisticsFromFile.Quality:N2}");
        Console.WriteLine($"Parts OK wynosi: {statisticsFromFile.Passed}");
        Console.WriteLine($"Parts NOK wynosi: {statisticsFromFile.Failed}");
        Console.WriteLine($"Parts Total wynosi: {statisticsFromFile.TotalParts}");

        Console.WriteLine($"");
        Console.WriteLine($"Jeśli chcesz przerwać wciśnij 'q' jeśli chcesz kontunować to dowolny klawisz..");
        var inputExitApp = Console.ReadLine();
        if (inputExitApp == "q")
        {
            break;
        }
    }
    static void HandleStatus(string status, MachineCUPP machineCUPP)
    {
        try
        {
            machineCUPP.AddStatusMachine(status);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}");
        }
    }
}


