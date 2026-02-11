using System;
using System.Collections.Generic;

public static class DisplayHelper
{
    public static void ShowTasks(List<TaskItem> tasks)
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("Няма добавени задачи.");
            return;
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].IsCompleted)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"{i + 1}. {tasks[i].Name} - " +
                (tasks[i].IsCompleted ? "Завършена" : "Незавършена"));

            Console.ResetColor();
        }
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Натисни клавиш за продължаване...");
        Console.ReadKey();
        Console.Clear();
    }
}
