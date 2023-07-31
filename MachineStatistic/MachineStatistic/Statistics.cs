
using System.Globalization;
using System.Runtime.CompilerServices;

namespace MachineStatistic
{
    public class Statistics
    {
        public float Utilization { get; private set; }
        public float OEE { get; private set; }
        public float Waiting { get; private set; }
        public float Quality { get; private set; }
        public int Passed { get; private set; }
        public int Failed { get; private set; }
        public int TotalParts { get; private set; }

        public string MostEfficientPeriod { get; private set; }

        public void CalculateUtilization(string filePath)
        {

            int totalTime = 0;
            int workingTime = 0;
            int partsOK = 0;
            int partsNOK = 0;
            int totalParts = 0;
            int waitingTime = 0;
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');

                bool isProducing = bool.Parse(data[1]);
                bool isWaiting = bool.Parse(data[2]);
                bool isPartsOk = bool.Parse(data[3]);
                bool isPartsNok = bool.Parse(data[4]);

                if (isProducing)
                {
                    workingTime++;
                }
                if (isWaiting)
                {
                    waitingTime++;
                }
                if (isProducing || isWaiting)
                {
                    totalTime++;
                }
                if (isPartsOk)
                {
                    partsOK++;
                }
                if (isPartsNok)
                {
                    partsNOK++;
                }

                totalParts = partsOK + partsNOK;
            }
            Utilization = (float)workingTime / totalTime * 100;
            Waiting = (float)waitingTime / totalTime * 100;
            OEE = (float)partsOK / totalParts * Utilization;
            Passed = (int)partsOK;
            Failed = (int)partsNOK;
            TotalParts = totalParts;
            Quality = (float)partsOK / totalParts * 100;
        }

        public void MostEfficient(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            int startLineIndex = 1;
            int maxPartsOK = 0;
            string mostEfficientPeriod = "";

            while (startLineIndex < lines.Length)
            {
                int partsOK = 0;

                int endLineIndex = startLineIndex;
                DateTime startTime = DateTime.ParseExact(lines[startLineIndex].Split(',')[0], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime endTime = startTime.AddMinutes(15);

                while (endLineIndex < lines.Length)
                {
                    DateTime currentTime = DateTime.ParseExact(lines[endLineIndex].Split(',')[0], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                  
                    if (currentTime >= endTime)
                        break;

                    bool isPartsOk = bool.Parse(lines[endLineIndex].Split(',')[3]);

                    if (isPartsOk)
                        partsOK++;

                    endLineIndex++;
                }
                if (partsOK > maxPartsOK)
                {
                    maxPartsOK = partsOK;
                    mostEfficientPeriod = startTime.ToString("dd.MM.yyyy HH:mm:ss") + " - " + endTime.ToString("dd.MM.yyyy HH:mm:ss");
                }

                startLineIndex++;
            }
            Passed = maxPartsOK;
            Failed = lines.Length - 2 - Passed; 
            MostEfficientPeriod = mostEfficientPeriod;
        }
    }
}

