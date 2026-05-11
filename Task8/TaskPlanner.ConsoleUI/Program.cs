using System;
using TaskPlanner.Core;

namespace TaskPlanner.ConsoleUI
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Project myProject = new Project();
            myProject.Details.Name = "Планувальник завдань";
            
            ProjectReporter reporter = new ProjectReporter(myProject);

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("1. Додати виконавця");
                Console.WriteLine("2. Видалити виконавця");
                Console.WriteLine("3. Показати всіх виконавців");
                Console.WriteLine("4. Додати завдання");
                Console.WriteLine("5. Призначити завдання виконавцю");
                Console.WriteLine("6. Показати всі завдання");
                Console.WriteLine("7. Показати виконані/невиконані завдання");
                Console.WriteLine("8. Показати стан проекту");
                Console.WriteLine("9. Пошук виконавця за іменем");
                Console.WriteLine("0. Вийти");
                Console.WriteLine("=================================");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть ID: ");
                        if (int.TryParse(Console.ReadLine(), out int mId))
                        {
                            Console.Write("Введіть ім'я: ");
                            string fName = Console.ReadLine() ?? "Без імені";
                            Console.Write("Введіть прізвище: ");
                            string lName = Console.ReadLine() ?? "Без прізвища";
                            myProject.AddMember(new TeamMember { Id = mId, FirstName = fName, LastName = lName });
                            Console.WriteLine("Виконавця додано!");
                        }
                        else Console.WriteLine("Некоректний ID.");
                        break;

                    case "2":
                        Console.Write("Введіть ID виконавця для видалення: ");
                        if (int.TryParse(Console.ReadLine(), out int rmId))
                        {
                            bool success = myProject.RemoveMember(rmId);
                            Console.WriteLine(success ? "Видалено." : "Не знайдено.");
                        }
                        break;

                    case "3":
                        reporter.ShowAllMembers();
                        break;

                    case "4":
                        Console.Write("Введіть ID завдання: ");
                        if (int.TryParse(Console.ReadLine(), out int tId))
                        {
                            Console.Write("Назва завдання: ");
                            string title = Console.ReadLine() ?? "Без назви";
                            Console.Write("Дедлайн (у форматі РРРР-ММ-ДД, наприклад 2026-12-31): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime dl))
                            {
                                myProject.AddTask(new ProjectTask { Id = tId, Title = title, Deadline = dl, ProgressPercentage = 0, IsCompleted = false });
                                Console.WriteLine("Завдання додано!");
                            }
                            else Console.WriteLine("Некоректний формат дати.");
                        }
                        break;

                    case "5":
                        Console.Write("Введіть ID завдання: ");
                        int.TryParse(Console.ReadLine(), out int assignTid);
                        Console.Write("Введіть ID виконавця: ");
                        int.TryParse(Console.ReadLine(), out int assignMid);
                        
                        bool assigned = myProject.AssignTask(assignTid, assignMid);
                        Console.WriteLine(assigned ? "Завдання успішно призначено!" : "Помилка: Завдання або виконавця не знайдено.");
                        break;

                    case "6":
                        reporter.ShowTasks();
                        break;

                    case "7":
                        Console.Write("Показати виконані? (y - так, n - ні): ");
                        string compChoice = Console.ReadLine() ?? "";
                        if (compChoice.ToLower() == "y") reporter.ShowTasks(true);
                        else if (compChoice.ToLower() == "n") reporter.ShowTasks(false);
                        break;

                    case "8":
                        reporter.ShowProjectStatus();
                        break;

                    case "9":
                        Console.Write("Введіть ім'я або прізвище для пошуку: ");
                        string query = Console.ReadLine() ?? "";
                        reporter.SearchMembers(query);
                        break;

                    case "0":
                        isRunning = false;
                        Console.WriteLine("Роботу завершено.");
                        break;

                    default:
                        Console.WriteLine("Невідома команда. Спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}