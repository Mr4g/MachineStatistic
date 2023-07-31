public class DataGenerator
{
    public static void GenerateDataFile(string filePath)
    {      
        DateTime startTime = DateTime.Parse("06:00");  
        DateTime endTime = DateTime.Parse("15:00");   
        TimeSpan timeRange = endTime - startTime;
        int timeIntervals = (int)(timeRange.TotalSeconds);  
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("data,producing,waiting,partOK,partNOK");

            for (int i = 0; i < 20000; i++)
            {
                DateTime currentTime = startTime.AddSeconds(i % timeIntervals); 

                bool isProducing = GetRandomBoolean();
                bool isWaiting = !isProducing && GetRandomBoolean();
                bool isPartsOK = isProducing && !isWaiting && GetRandomBoolean();
                bool isPartsNOK = isProducing && !isPartsOK && GetRandomBoolean();

                if (!isProducing && !isWaiting)
                {
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
                    isPartsOK = false;  
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
