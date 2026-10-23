using System;
class Program
{
    static void Main()
    {
        int[,] temperature = new int[12, 30];
        Random random = new Random();

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                temperature[i, j] = random.Next(-20, 36);
            }
        }

        double[] average = GetAverage(temperature);
        Console.WriteLine("Средняя температура по месяцам:");

        for (int i = 0; i < 12; i++)
        {
            Console.WriteLine("Месяц " + (i + 1) + ": " + average[i]);
        }

        Array.Sort(average);

        Console.WriteLine("Температуры по возрастанию:");
        for (int i = 0; i < 12; i++)
        {
            Console.WriteLine(average[i]);
        }
    }
    static double[] GetAverage(int[,] temperature)
    {
        double[] average = new double[12];

        for (int i = 0; i < 12; i++)
        {
            int sum = 0;

            for (int j = 0; j < 30; j++)
            {
                sum += temperature[i, j];
            }

            average[i] = (double)sum / 30;
        }
        return average;
    }
}
