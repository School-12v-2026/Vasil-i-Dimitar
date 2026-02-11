using System;

public static class Menu
{
    public static void Show()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("==== TASK MANAGER ====");
        Console.WriteLine("1. Добави задача");
        Console.WriteLine("2. Покажи всички задачи");
        Console.WriteLine("3. Маркирай като завършена");
        Console.WriteLine("4. Изтрий задача");
        Console.WriteLine("5. Сортирай по име");
        Console.WriteLine("6. Сортирай по статус");
        Console.WriteLine("7. Изход");
        Console.ResetColor();
        Console.Write("Избери опция: ");
    }
}
