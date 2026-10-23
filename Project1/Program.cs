using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Укажите имя файла.");
            return;
        }

        string text = File.ReadAllText(args[0]);
        char[] letters = text.ToCharArray();

        CountLetters(letters);
    }

    static void CountLetters(char[] letters)
    {
        int vowels = 0;
        int consonants = 0;

        string vowelLetters = "аеёиоуыэюяАЕЁИОУЫЭЮЯ";

        foreach (char letter in letters)
        {
            if (char.IsLetter(letter))
            {
                if (vowelLetters.Contains(letter))
                {
                    vowels++;
                }
                else
                {
                    consonants++;
                }
            }
        }

        Console.WriteLine("Гласных букв: " + vowels);
        Console.WriteLine("Согласных букв: " + consonants);
    }
}

