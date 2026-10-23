using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        string[] months =
        {
            "Январь", "Февраль", "Март", "Апрель",
            "Май", "Июнь", "Июль", "Август",
            "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };
        Dictionary<string, int[]> temperature = new Dictionary<string, int[]>();
        Random random = new Random();
        foreach (string month in months)
        {
            int[] days = new int[30];
            for (int i = 0; i < days.Length; i++)
            {
                days[i] = random.Next(-20, 36);
            }
            temperature.Add(month, days);
        }
        Dictionary<string, double> average = new Dictionary<string, double>();
        foreach (string month in months)
        {
            int sum = 0;
            foreach (int day in temperature[month])
            {
                sum += day;
            }
            average.Add(month, (double)sum / temperature[month].Length);
        }
        Console.WriteLine("Средняя температура по месяцам:");
        foreach (string month in months)
        {
            Console.WriteLine(month + ": " + average[month].ToString("F2"));
        }
        List<double> sorted = new List<double>(average.Values);
        sorted.Sort();

        Console.WriteLine();
        Console.WriteLine("Средние температуры по возрастанию:");
        foreach (double value in sorted)
        {
            Console.WriteLine(value.ToString("F2"));
        }
    }
}

