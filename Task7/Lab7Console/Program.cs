using System;
using DataStructures;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        DoublyLinkedList list = new DoublyLinkedList();

        list.AddFirst(5.5);
        list.AddFirst(-2.0);
        list.AddFirst(101.1);
        list.AddFirst(11.2);

        Console.WriteLine("--- Початковий список ---");
        foreach (var x in list) Console.Write($"{x}  ");

        Console.WriteLine($"\n\n1. Перший позитивний: {list.GetFirstPositive()}");

        double avg = list.GetAverage();
        Console.WriteLine($"2. Кількість менших за середнє ({avg:F2}): {list.CountLessThanAverage()}");

        Console.WriteLine("\n3. Новий список (елементи > середнього):");
        DoublyLinkedList newList = list.GetNewListGreaterThanAverage();
        foreach (var x in newList) Console.Write($"{x}  ");

        Console.WriteLine("\n\n4. Видаляємо елементи до максимального ...");
        list.RemoveBeforeMax();
        Console.WriteLine("Результат:");
        foreach (var x in list) Console.Write($"{x}  ");
        Console.WriteLine();
    }
}