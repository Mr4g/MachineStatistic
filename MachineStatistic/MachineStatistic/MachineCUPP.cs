using System.Data;
using System.Runtime.CompilerServices;

namespace MachineStatistic
{
    public class MachineCUPP : MachineBase
    {
        private DateTime startTime;
        private DateTime endTime;
        private TimeSpan timeRange;
        private int timeIntervals;
        private DateTime currentTime;       
        private List<float> mchineStatus =  new List<float>();
        public override event StatusAddedDelegate StatusAdded;

        public string FileName { get; private set; }
        
        public MachineCUPP(string eq, string department) 
            : base(eq, department)
        {
            startTime = DateTime.Parse("06:00");  // przekształca 06:00 na obiekt DataTime
            endTime = DateTime.Parse("15:00");
            timeRange = endTime - startTime;
            currentTime = startTime;
            FileName = $"EQ{eq}_{department}.txt";
            timeIntervals = (int)(timeRange.TotalSeconds);
        }

        public override void ManualGenerateDataFile(string status)
        {
            timeIntervals = (int)(timeRange.TotalSeconds);  

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
                    string line = string.Format("{0},{1}", currentTime, status);
                    writer.WriteLine(line);
                }
            }
            else
            {
                using (var writer = File.AppendText(FileName))
                {
                    writer.WriteLine("data,producing,waiting,partOK,partNOK");                  
                    string line = string.Format("{0},{1}", currentTime, status);
                    writer.WriteLine(line);
                }
            }
        }

        public override void AddStatusMachine(bool status)
        {
            throw new NotImplementedException();
        }

        public override void AddStatusMachine(int status)
        {
            throw new NotImplementedException();
        }

        public override void AddStatusMachine(double status)
        {
            throw new NotImplementedException();
        }

        public override void AddStatusMachine(long status)
        {
            throw new NotImplementedException();
        }

        public override void AddStatusMachine(float status)
        {
            throw new NotImplementedException();
        }

        public override void AddStatusMachine(string status)
        {
            throw new NotImplementedException();
        }

        public override void AddStatusMachne(char status)
        {
            throw new NotImplementedException();
        }

        public override Statistics GetStatistic()
        {
            throw new NotImplementedException();
        }
    }
}
