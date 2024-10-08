using System;
using static System.Console;

namespace SortingApp
{
    class Program
    {
        // Делегат для методов сортировки
        public delegate void SortMethod(int[] array);

        static void Main(string[] args)
        {
            int[] numbers = null; // Объявляем массив чисел
            int[] sortedNumbers = null; // Объявляем массив для отсортированных чисел

            while (true)
            {
                Clear();
                // Основное меню
                WriteLine("Меню:");
                WriteLine("1. Создать массив");
                WriteLine("2. Сортировка массива");
                WriteLine("3. Показать отсортированный массив");
                WriteLine("0. Выход");

                int choice = int.Parse(ReadLine());

                switch (choice)
                {
                    case 1:
                        numbers = GetNumbersFromUser(); // Ввод массива пользователем
                        sortedNumbers = null; // Обнуляем отсортированный массив при создании нового
                        break;
                    case 2:
                        if (numbers == null)
                        {
                            WriteLine("Массив не создан! Сначала создайте массив.");
                            ReadKey();
                            continue;
                        }
                        sortedNumbers = SortArray(numbers); // Сортировка массива и сохранение результата
                        break;
                    case 3:
                        if (sortedNumbers == null)
                           WriteLine("Отсортированный массив не доступен! Выполните сортировку.");
                        else
                           WriteLine("Отсортированный массив: " + string.Join(" ", sortedNumbers));
                        ReadKey();
                        break;
                    case 0:
                        WriteLine("Выход из программы...");
                        return;
                    default:
                        WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        // Метод для ввода чисел от пользователя
        static int[] GetNumbersFromUser()
        {
            Clear();
            WriteLine("Введите числа через пробел:");
            string input = ReadLine();
            string[] inputs = input.Split(' ');

            int[] numbers = new int[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                numbers[i] = int.Parse(inputs[i]);
            }

            return numbers;
        }

        // Метод для выбора сортировки массива
        static int[] SortArray(int[] numbers)
        {
            int[] sortedNumbers = (int[])numbers.Clone(); // Клонируем массив для сортировки, чтобы не изменять оригинальный
            while (true)
            {
                Clear();
                WriteLine("Исходный массив: " + string.Join(" ", numbers));

                // Меню выбора метода сортировки
                WriteLine("Выберите метод сортировки:");
                WriteLine("1. Сортировка пузырьком");
                WriteLine("2. Быстрая сортировка");
                WriteLine("0. Вернуться в главное меню");

                int choice = int.Parse(ReadLine());

                SortMethod sortMethod;

                switch (choice)
                {
                    case 1:
                        sortMethod = BubbleSort;
                        sortMethod(sortedNumbers);
                        break;
                    case 2:
                        sortMethod = QuickSort;
                        sortMethod(sortedNumbers);
                        break;
                    case 0:
                        return sortedNumbers; // Возвращаем отсортированный массив в главное меню
                    default:
                        WriteLine("Неверный выбор. Попробуйте снова.");
                        ReadKey();
                        continue;
                }

                // Вывод отсортированного массива
                WriteLine("Отсортированный массив: " + string.Join(" ", sortedNumbers));
                WriteLine("Нажмите любую клавишу, чтобы продолжить сортировку или вернуться в меню...");
                ReadKey();
            }
        }

        // Метод сортировки пузырьком
        static void BubbleSort(int[] array)
        {
            int temp;
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        // Метод быстрой сортировки
        static void QuickSort(int[] array)
        {
            QuickSortHelper(array, 0, array.Length - 1);
        }

        private static void QuickSortHelper(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right);
                QuickSortHelper(array, left, pivotIndex - 1);
                QuickSortHelper(array, pivotIndex + 1, right);
            }
        }

        private static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            int temp1 = array[i + 1];
            array[i + 1] = array[right];
            array[right] = temp1;

            return i + 1;
        }
    }
}