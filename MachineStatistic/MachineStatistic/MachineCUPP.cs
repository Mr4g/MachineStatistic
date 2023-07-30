using System.ComponentModel.Design;
using System.Data;
using System.Runtime.CompilerServices;

namespace MachineStatistic
{
    public class MachineCUPP : MachineBase
    {
        private DateTime startTime;
        private TimeSpan timeRange;
        private DateTime currentTime;
        public List<string> machineStatus { get; private set; }

        public override event StatusAddedDelegate StatusAdded;

        public string FileName { get; private set; }

        public MachineCUPP(int eq, string department)
            : base(eq, department)
        {
            startTime = DateTime.Now;
            currentTime = startTime;
            FileName = $"EQ{eq}_{department}.txt";
            machineStatus = new List<string>();
        }

        public override void ManualGenerateDataFile(List<string> status)
        {
            if (File.Exists($"{FileName}"))
            {
                using (var reader = File.OpenRead($"{FileName}"))
                {
                    string lastLine = File.ReadLines(FileName).LastOrDefault();
                    string[] data = lastLine.Split(',');
                    DateTime.TryParse(data[0], out currentTime);
                }
                using (var writer = File.AppendText(FileName))
                {
                    currentTime = currentTime.AddSeconds(1);
                    string line = string.Format("{0},{1},{2},{3},{4}", currentTime, status[0], status[1], status[2], status[3]);
                    writer.WriteLine(line);
                }
            }
            else
            {
                using (var writer = File.AppendText(FileName))
                {
                    writer.WriteLine("data,producing,waiting,partOK,partNOK");
                    string line = string.Format("{0},{1},{2},{3},{4}", currentTime, status[0], status[1], status[2], status[3]);
                    writer.WriteLine(line);
                }   
            }
        }

        public override void AddStatusMachine(string status)
        {
            status = status.ToLower();
            if (machineStatus.Count < 4)
            {
                switch (status)
                {
                    case "1":
                        status = "true";
                        this.machineStatus.Add(status);
                        break;
                    case "0":
                        status = "false";
                        this.machineStatus.Add(status);
                        break;
                    case var status1 when status == "true":
                        this.machineStatus.Add(status1);
                        break;
                    case var status1 when status == "false":
                        this.machineStatus.Add(status1);
                        break;
                    default:
                        throw new Exception("Nieprawidłowy status maszyny");
                }
            }
        }

        public void StatusValidation()
        {
            var index = 0;
            foreach (var stat in machineStatus)
            {
                switch (index)
                {
                    case 0:
                        break;                  
                    case 1: // Waiting
                        if (machineStatus[1] == "true" && machineStatus[0] == "true")
                        {
                            Console.WriteLine("Błąd walidacji: Waiting nie może występować, gdy Producing występuje..");
                            return;
                        }
                        break;
                    case 2: // Parts NOK
                        if (machineStatus[1] == "true" && machineStatus[3] == "true")
                        {
                            Console.WriteLine("Błąd walidacji: Waiting nie może wystapić gdy PartsOK jest true..");
                            return;
                        }
                        break;
                    case 3:
                        {
                            if (machineStatus[1] == "false" && machineStatus[0] == "false")
                            {
                                Console.WriteLine("Błąd walidacji: Maszyna musi zgłosić któryś z sygnałów... Producing/Waiting \nw tej wersji nie zakładamy, że maszyna jest wyłączona wykup dodatkową subskrybcję a dodamy ten stgnał...");
                                return;
                            }
                            if (machineStatus[2] == "true" && machineStatus[3] == "true")
                            {
                                Console.WriteLine("Błąd walidacji: nie mogą być jednoszcześnie PartsOK i PartsNOK");
                                return;
                            }
                        }
                        break;
                    default:
                        Console.WriteLine($"Nieznany status: {stat}");
                        break;
                }
                index++;
            }
            ManualGenerateDataFile(machineStatus);
            machineStatus.Clear();
        }

        public void ClearStatus()
        {
            machineStatus.Clear();
        }
        public override void AddStatusMachine(bool status)
        {
            string boolStatus = status.ToString();
            this.AddStatusMachine(boolStatus);
        }

        public override void AddStatusMachine(int status)
        {
            string intStatus = status.ToString();
            this.AddStatusMachine(intStatus);
        }

        public override void AddStatusMachine(double status)
        {
            string doubleStatus = status.ToString();
            this.AddStatusMachine(doubleStatus);
        }

        public override void AddStatusMachine(long status)
        {
            string longStatus = status.ToString();
            this.AddStatusMachine(longStatus);
        }

        public override void AddStatusMachine(float status)
        {
            string floatStatus = status.ToString();
            this.AddStatusMachine(floatStatus);
        }

        public override void AddStatusMachne(char status)
        {
            string charStatus = status.ToString();
            this.AddStatusMachine(charStatus);
        }

        public override Statistics GetStatistic()
        {
            throw new NotImplementedException();
        }
    }
}
