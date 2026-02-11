using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static List<TaskItem> tasks = new List<TaskItem>();
    static string filePath = "tasks.txt";

    static void Main(string[] args)
    {
        LoadFromFile();

        while (true)
        {
            ShowMenu();
            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    AddTask();
                    break;

                case 2:
                    DisplayTasks();
                    break;

                case 3:
                    MarkAsCompleted();
                    break;

                case 4:
                    DeleteTask();
                    break;

                case 5:
                    SortByName();
                    break;

                case 6:
                    SortByStatus();
                    break;

                case 7:
                    SaveToFile();
                    return;

                default:
                    Console.WriteLine("Невалидна опция!");
                    break;
            }

            Pause();
        }
    }

    static void ShowMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("==== TASK MANAGER ====");
        Console.WriteLine("1. Добави задача");
        Console.WriteLine("2. Покажи всички задачи");
        Console.WriteLine("3. Маркирай като завършена");
        Console.WriteLine("4. Изтрий задача");
        Console.WriteLine("5. Сортирай по име (A-Z)");
        Console.WriteLine("6. Сортирай по статус");
        Console.WriteLine("7. Изход");
        Console.ResetColor();
        Console.Write("Избери опция: ");
    }

    static int GetUserChoice()
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

    static void AddTask()
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

        tasks.Add(new TaskItem { Name = name, IsCompleted = false });
        SaveToFile();
    }

    static void DisplayTasks()
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

    static void MarkAsCompleted()
    {
        DisplayTasks();
        Console.Write("Избери номер на задача: ");

        if (int.TryParse(Console.ReadLine(), out int index))
        {
            if (index > 0 && index <= tasks.Count)
            {
                tasks[index - 1].IsCompleted = true;
                SaveToFile();
            }
        }
    }

    static void DeleteTask()
    {
        DisplayTasks();
        Console.Write("Избери номер на задача за изтриване: ");

        if (int.TryParse(Console.ReadLine(), out int index))
        {
            if (index > 0 && index <= tasks.Count)
            {
                tasks.RemoveAt(index - 1);
                SaveToFile();
            }
        }
    }

    static void SortByName()
    {
        tasks = tasks.OrderBy(t => t.Name).ToList();
    }

    static void SortByStatus()
    {
        tasks = tasks.OrderBy(t => t.IsCompleted).ToList();
    }

    static void SaveToFile()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var task in tasks)
            {
                writer.WriteLine($"{task.Name}|{task.IsCompleted}");
            }
        }
    }

    static void LoadFromFile()
    {
        if (!File.Exists(filePath))
            return;

        string[] lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 2)
            {
                tasks.Add(new TaskItem
                {
                    Name = parts[0],
                    IsCompleted = bool.Parse(parts[1])
                });
            }
        }
    }

    static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Натисни клавиш за продължаване...");
        Console.ReadKey();
    }
}

class TaskItem
{
    public string Name { get; set; }
    public bool IsCompleted { get; set; }
}
