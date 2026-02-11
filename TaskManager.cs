using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleTaskManager
{
    class TaskManager
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>();
        private readonly string _filePath;

        // ✅ fileName: "tasks.txt"
        public TaskManager(string fileName = "tasks.txt")
        {
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = "tasks.txt";

            // ✅ най-сигурното място за конзолно приложение (в папката на .exe)
            _filePath = Path.Combine(AppContext.BaseDirectory, fileName);

            EnsureFileExists();
            LoadFromFile();
        }

        public IReadOnlyList<TaskItem> GetAll() => _tasks;

        public void AddTask(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Името на задачата не може да е празно.");

            _tasks.Add(new TaskItem(name.Trim(), false));
            SaveToFile();
        }

        public bool MarkTaskDone(int number)
        {
            int index = number - 1; // потребителят дава 1..N
            if (!IsValidIndex(index)) return false;

            _tasks[index].IsDone = true;
            SaveToFile();
            return true;
        }

        public bool DeleteTask(int number)
        {
            int index = number - 1;
            if (!IsValidIndex(index)) return false;

            _tasks.RemoveAt(index);
            SaveToFile();
            return true;
        }

        public void SortByNameAZ()
        {
            _tasks.Sort((a, b) =>
                string.Compare(a.Name, b.Name, StringComparison.CurrentCultureIgnoreCase));
            SaveToFile();
        }

        public void SortByStatus()
        {
            // незавършени първо (false), после завършени (true)
            var sorted = _tasks
                .OrderBy(t => t.IsDone)
                .ThenBy(t => t.Name, StringComparer.CurrentCultureIgnoreCase)
                .ToList();

            _tasks.Clear();
            _tasks.AddRange(sorted);
            SaveToFile();
        }

        private bool IsValidIndex(int index) => index >= 0 && index < _tasks.Count;

        private void EnsureFileExists()
        {
            // ✅ ако папката я няма → създай я
            string? dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // ✅ ако файла го няма → създай празен
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, string.Empty);
        }

        public void LoadFromFile()
        {
            _tasks.Clear();

            try
            {
                // ✅ ReadLines е по-лек от ReadAllLines
                foreach (string line in File.ReadLines(_filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // формат: име|0 или име|1
                    string[] parts = line.Split('|');
                    if (parts.Length != 2) continue;

                    string name = parts[0].Trim();
                    string statusPart = parts[1].Trim();

                    if (string.IsNullOrWhiteSpace(name)) continue;

                    bool isDone = statusPart == "1";
                    _tasks.Add(new TaskItem(name, isDone));
                }
            }
            catch (IOException)
            {
                // файлът е заключен/проблем с диска -> оставяме празно
                _tasks.Clear();
            }
            catch (UnauthorizedAccessException)
            {
                _tasks.Clear();
            }
        }

        public void SaveToFile()
        {
            try
            {
                var lines = _tasks.Select(t => $"{t.Name}|{(t.IsDone ? "1" : "0")}");
                File.WriteAllLines(_filePath, lines);
            }
            catch (IOException)
            {
                // може UI-то да покаже съобщение, но тук не крашваме
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        // ✅ полезно за дебъг (къде е tasks.txt)
        public string GetFilePath() => _filePath;
    }
}
