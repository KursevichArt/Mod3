using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;

namespace Mod3_4
{
    class Program
    {
        // Определим делегат для фильтрации данных
        public delegate bool DataFilter<T>(T item);

        // Метод для фильтрации данных
        public static List<T> FilterData<T>(List<T> data, DataFilter<T> filter)
        {
            return data.Where(item => filter(item)).ToList();
        }

        // Пример фильтрации по ключевым словам
        public static bool KeywordFilter(string item, List<string> keywords)
        {
            return keywords.Any(keyword => item.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        // Пример фильтрации по дате
        public static bool DateFilter(DateTime item, DateTime filterDate)
        {
            return item.Date == filterDate;
        }

        // Метод для выделения ключевых слов из пользовательского ввода
        public static List<string> ExtractKeywords(string userInput)
        {
            // Ключевые слова, которые ищем в данных
            string[] commonKeywords = { "C#", "Unity", "App", "Filter", "Mobile", "System" };

            List<string> foundKeywords = new List<string>();

            // Проходим по каждому ключевому слову и проверяем его наличие в пользовательском вводе
            foreach (var keyword in commonKeywords)
            {
                if (userInput.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    foundKeywords.Add(keyword);
            }

            return foundKeywords;
        }

        static void Main(string[] args)
        {
            // Пример с текстовыми данными
            List<string> textData = new List<string> { "C#", "Unity", "Mobile App", "Filter System", "2024" };

            // Пример с датами
            List<DateTime> dateData = new List<DateTime>
            {
                new DateTime(2024, 9, 24),
                new DateTime(2024, 9, 25),
                new DateTime(2024, 9, 26)
            };

            while (true)
            {
                // Выбор типа фильтрации
                WriteLine("Выберите тип фильтрации:\n1. По ключевым словам\n2. По дате\n0. Выход");
                string choice = ReadLine();

                if (choice == "0")
                {
                    WriteLine("Выход из программы.");
                    break;
                }

                if (choice == "1")
                {
                    // Фильтрация по ключевым словам
                    Write("Введите текст для выделения ключевых слов: ");
                    string userInput = ReadLine();
                    List<string> keywords = ExtractKeywords(userInput);

                    if (keywords.Count > 0)
                    {
                        List<string> filteredText = FilterData(textData, item => KeywordFilter(item, keywords));
                        WriteLine("Отфильтрованные данные по ключевым словам:");
                        if (filteredText.Count > 0)
                            filteredText.ForEach(WriteLine);
                        else
                            WriteLine("Нет совпадений.");
                    }
                    else
                        WriteLine("Ключевые слова не найдены.");
                }
                else if (choice == "2")
                {
                    // Фильтрация по дате
                    Write("Введите дату для фильтрации (ГГГГ-ММ-ДД): ");
                    DateTime filterDate;
                    if (DateTime.TryParse(ReadLine(), out filterDate))
                    {
                        List<DateTime> filteredDates = FilterData(dateData, item => DateFilter(item, filterDate));
                        WriteLine("Отфильтрованные даты:");
                        if (filteredDates.Count > 0)
                            filteredDates.ForEach(d => WriteLine(d.ToShortDateString()));
                        else
                            WriteLine("Нет совпадений.");
                    }
                    else
                        WriteLine("Неверный формат даты.");
                }
                else
                    WriteLine("Неверный выбор.");
            }
        }
    }
}