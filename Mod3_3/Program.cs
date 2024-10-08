using System;
using static System.Console;
using System.Collections.Generic;


namespace Mod3_3
{
    // Делегат, представляющий действие над задачей
    public delegate void TaskAction(Task task);

    // Класс задачи
    public class Task
    {
        public string Description { get; set; }

        public Task(string description)
        {
            Description = description;
        }

        public override string ToString()
        {
            return Description;
        }
    }

    // Класс, управляющий выполнением задач
    public class TaskManager
    {
        // Метод для отправки уведомления
        public void SendNotification(Task task)
        {
            WriteLine($"Отправлено уведомление о задаче: {task.Description}");
        }

        // Метод для записи задачи в журнал
        public void LogTask(Task task)
        {
            WriteLine($"Задача записана в журнал: {task.Description}");
        }
    }

    // Класс для управления списком задач и выбора действий
    public class TaskApplication
    {
        private List<Task> tasks = new List<Task>();
        private TaskManager taskManager = new TaskManager();

        // Метод для добавления задачи
        public void AddTask(string description)
        {
            Task task = new Task(description);
            tasks.Add(task);
            WriteLine($"Задача добавлена: {task.Description}");
        }

        // Метод для выполнения выбранного действия над всеми задачами
        public void ExecuteTaskActions(TaskAction action)
        {
            foreach (Task task in tasks)
            {
                action(task);
            }
        }

        // Метод для выбора и выполнения действия
        public void ChooseAndExecuteAction()
        {
            WriteLine("Выберите действие для выполнения:");
            WriteLine("1. Отправить уведомление");
            WriteLine("2. Записать в журнал");

            string choice = ReadLine();

            switch (choice)
            {
                case "1":
                    ExecuteTaskActions(taskManager.SendNotification);
                    break;
                case "2":
                    ExecuteTaskActions(taskManager.LogTask);
                    break;
                default:
                    WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    // Основная программа
    class Program
    {
        static void Main(string[] args)
        {
            TaskApplication taskApp = new TaskApplication();

            while (true)
            {
                WriteLine("1. Добавить задачу");
                WriteLine("2. Выполнить действия над задачами");
                WriteLine("3. Выход");

                string input = ReadLine();

                switch (input)
                {
                    case "1":
                        WriteLine("Введите описание задачи:");
                        string description = ReadLine();
                        taskApp.AddTask(description);
                        break;

                    case "2":
                        taskApp.ChooseAndExecuteAction();
                        break;

                    case "3":
                        return;

                    default:
                        WriteLine("Неверный выбор.");
                        break;
                }
            }
        }
    }
}