
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
            // Wczytaj dane z pliku
            string[] lines = File.ReadAllLines(filePath);

            // Pomijamy pierwszą linijkę z etykietami
            for (int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');

                // Sprawdź wartości kolumny "producing" i "waiting"
                bool isProducing = bool.Parse(data[1]);
                bool isWaiting = bool.Parse(data[2]);
                bool isPartsOk = bool.Parse(data[3]);
                bool isPartsNok = bool.Parse(data[4]);

                // Jeśli jednostka była w stanie "producing" lub "waiting", zwiększ czas pracy
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

            // Oblicz utylizację
            Utilization = (float)workingTime / totalTime * 100;
            Waiting = (float)waitingTime / totalTime * 100;
            OEE = (float)partsOK / totalParts * 100;
            Passed = (int)partsOK;
            Failed = (int)partsNOK;
            TotalParts = totalParts;
            Quality = (float)partsOK / totalParts * 100;
        }

        public void MostEfficient(string filePath)
        {
            // Wczytaj dane z pliku
            string[] lines = File.ReadAllLines(filePath);

            // Pomijamy pierwszą linijkę z etykietami
            int startLineIndex = 1;

            // Przechowuje informacje o najwyższej wartości partsOK i odpowiadającym mu okresie czasu
            int maxPartsOK = 0;
            string mostEfficientPeriod = "";

            while (startLineIndex < lines.Length)
            {
                // Zeruj licznik partsOK dla każdego nowego okresu 15-minutowego
                int partsOK = 0;

                // Określ koniec okresu 15-minutowego
                int endLineIndex = startLineIndex;
                DateTime startTime = DateTime.ParseExact(lines[startLineIndex].Split(',')[0], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime endTime = startTime.AddMinutes(15);

                // Oblicz sumę partsOK dla danego okresu
                while (endLineIndex < lines.Length)
                {
                    DateTime currentTime = DateTime.ParseExact(lines[endLineIndex].Split(',')[0], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                    // Jeśli aktualny czas przekracza koniec okresu 15-minutowego, przerwij pętlę
                    if (currentTime >= endTime)
                        break;

                    bool isPartsOk = bool.Parse(lines[endLineIndex].Split(',')[3]);

                    if (isPartsOk)
                        partsOK++;

                    endLineIndex++;
                }

                // Sprawdź, czy obecny okres jest bardziej wydajny od dotychczasowego
                if (partsOK > maxPartsOK)
                {
                    maxPartsOK = partsOK;
                    mostEfficientPeriod = startTime.ToString("dd.MM.yyyy HH:mm:ss") + " - " + endTime.ToString("dd.MM.yyyy HH:mm:ss");
                }

                // Przesuń wskaźniki na następny okres 15-minutowy
                startLineIndex++;
            }

            Passed = maxPartsOK;
            Failed = lines.Length - 2 - Passed; // Odjęcie 1 z powodu etykiety i 1 z powodu pominięcia pierwszego wiersza danych
            MostEfficientPeriod = mostEfficientPeriod;
        }
    }
}

