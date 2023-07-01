


public class DataGenerator
{
    public static void GenerateDataFile(string filePath)
    {
        

        // Tworzenie daty i godziny od 6:00 do 14:59
        DateTime startTime = DateTime.Parse("06:00");  // przekształca 06:00 na obiekt DataTime
        DateTime endTime = DateTime.Parse("15:00");    // przekształca 15:00 na obiekt DataTime
        TimeSpan timeRange = endTime - startTime;
        int timeIntervals = (int)(timeRange.TotalSeconds);  // Liczba minut między 6:00 a 15:00

        // Tworzenie pliku tekstowego
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("data,producing,waiting,partOK,partNOK");

            for (int i = 0; i < 20000; i++)
            {
                DateTime currentTime = startTime.AddSeconds(i % timeIntervals);  // generowanie cykliczne 

                bool isProducing = GetRandomBoolean();
                bool isWaiting = !isProducing && GetRandomBoolean();
                bool isPartsOK = isProducing && !isWaiting && GetRandomBoolean();
                bool isPartsNOK = isProducing && !isPartsOK && GetRandomBoolean();

                if (!isProducing && !isWaiting)
                {
                    // Jeśli maszyna nie jest w stanie producing ani waiting, ustawiamy jedno z nich na true
                    bool randomState = GetRandomBoolean();
                    if (randomState)
                    {
                        isProducing = true;
                    }
                    else
                    {
                        isWaiting = true;
                    }
                }

                if (isWaiting)
                {
                    isPartsOK = false;  // Jeśli maszyna ma status waiting, ustawiamy partsOK na false
                }

                string line = string.Format("{0},{1},{2},{3},{4}", currentTime, isProducing, isWaiting, isPartsOK, isPartsNOK);
                writer.WriteLine(line);
            }
        }

        Console.WriteLine("Plik został wygenerowany: " + filePath);
    }

    private static bool GetRandomBoolean()
    {
        Random random = new Random();
        return random.Next(2) == 0;
    }


}
