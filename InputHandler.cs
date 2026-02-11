using System;

public static class InputHandler
{
    public static int GetMenuChoice()
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Моля въведете валидно число!");
            Console.ResetColor();
            Console.Write("Избери опция: ");
        }
        return choice;
    }

    public static string GetTaskName()
    {
        Console.Write("Въведи име на задача: ");
        string name = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Името не може да бъде празно!");
            Console.ResetColor();
            Console.Write("Въведи име на задача: ");
            name = Console.ReadLine();
        }

        return name;
    }

    public static int GetTaskIndex()
    {
        Console.Write("Въведи номер на задача: ");
        int index;
        while (!int.TryParse(Console.ReadLine(), out index))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Моля въведете валидно число!");
            Console.ResetColor();
            Console.Write("Въведи номер на задача: ");
        }
        return index;
    }
}
