using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        LinkedList<LinkedList<int>> first = new LinkedList<LinkedList<int>>();
        first.AddLast(new LinkedList<int>(new int[] { 1, 2 }));
        first.AddLast(new LinkedList<int>(new int[] { 3, 4 }));

        LinkedList<LinkedList<int>> second = new LinkedList<LinkedList<int>>();
        second.AddLast(new LinkedList<int>(new int[] { 5, 6 }));
        second.AddLast(new LinkedList<int>(new int[] { 7, 8 }));

        Console.WriteLine("Первая матрица:");
        PrintMatrix(first);
        Console.WriteLine("Вторая матрица:");
        PrintMatrix(second);

        LinkedList<LinkedList<int>> result = MultiplyMatrix(first, second);
        Console.WriteLine("Результат умножения:");
        PrintMatrix(result);
    }
     static void PrintMatrix(LinkedList<LinkedList<int>> matrix)
    {
        foreach (LinkedList<int> row in matrix)
        {
            foreach (int number in row)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine();
        }
    }
    static LinkedList<LinkedList<int>> MultiplyMatrix(
        LinkedList<LinkedList<int>> first,
        LinkedList<LinkedList<int>> second)
    {
        int rows = first.Count;
        int columns = second.First!.Value.Count;
        int common = first.First!.Value.Count;

        LinkedList<LinkedList<int>> result = new LinkedList<LinkedList<int>>();

        for (int i = 0; i < rows; i++)
        {
            LinkedList<int> row = new LinkedList<int>();
            for (int j = 0; j < columns; j++)
            {
                int sum = 0;
                for (int k = 0; k < common; k++)
                {
                    int a = GetValue(first, i, k);
                    int b = GetValue(second, k, j);

                    sum += a * b;
                }
                row.AddLast(sum);
            }
            result.AddLast(row);
        }
         return result;
    }

    static int GetValue(
        LinkedList<LinkedList<int>> matrix,
        int rowIndex,
        int columnIndex)
    {
        LinkedListNode<LinkedList<int>> rowNode = matrix.First!;
        for (int i = 0; i < rowIndex; i++)
        {
            rowNode = rowNode.Next!;
        }
        LinkedListNode<int> numberNode = rowNode.Value.First!;
        for (int j = 0; j < columnIndex; j++)
        {
            numberNode = numberNode.Next!;
        }
        return numberNode.Value;
    }
}
