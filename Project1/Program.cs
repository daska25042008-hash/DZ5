using System;
class Program
{
    static void Main()
    {
        int[,] first = {
            { 1, 2 },
            { 3, 4 }
        };

        int[,] second = {
            { 5, 6 },
            { 7, 8 }
        };

        Console.WriteLine("Первая матрица:");
        PrintMatrix(first);
        Console.WriteLine("Вторая матрица:");
        PrintMatrix(second);

        int[,] result = MultiplyMatrix(first, second);

        Console.WriteLine("Результат умножения:");
        PrintMatrix(result);
    }

    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }

            Console.WriteLine();
        }
    }

    static int[,] MultiplyMatrix(int[,] first, int[,] second)
    {
        int rows = first.GetLength(0);
        int columns = second.GetLength(1);
        int[,] result = new int[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                for (int k = 0; k < first.GetLength(1); k++)
                {
                    result[i, j] += first[i, k] * second[k, j];
                }
            }
        }

        return result;
    }
}

