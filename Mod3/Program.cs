using System;
using static System.Console;
using static System.Math;

namespace Mod3
{
    public delegate double CalculateAreaDelegate();

    public abstract class Shape
    {
        public abstract double CalculateArea();
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double CalculateArea()
        {
            return PI * Pow(Radius, 2);
        }
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea()
        {
            return Width * Height;
        }
    }

    public class Triangle : Shape
    {
        public double Base { get; set; }
        public double Height { get; set; }

        public Triangle(double baseLength, double height)
        {
            Base = baseLength;
            Height = height;
        }

        public override double CalculateArea()
        {
            return 0.5 * Base * Height;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                // Выбор фигуры пользователем
                WriteLine("Выберите фигуру для вычисления площади:");
                WriteLine("1. Круг");
                WriteLine("2. Прямоугольник");
                WriteLine("3. Треугольник");
                WriteLine("0. Выход из программы");

                int choice = int.Parse(ReadLine());

                if (choice == 0)
                {
                    exit = true;
                    continue;
                }

                Shape shape = null;
                CalculateAreaDelegate areaDelegate = null;

                // Динамическое создание фигуры и назначение делегата
                switch (choice)
                {
                    case 1:
                        Clear();
                        Write("Введите радиус круга: ");
                        double radius = double.Parse(ReadLine());
                        shape = new Circle(radius);
                        areaDelegate = shape.CalculateArea;
                        break;

                    case 2:
                        Clear();
                        Write("Введите ширину прямоугольника: ");
                        double width = double.Parse(ReadLine());
                        Write("Введите высоту прямоугольника: ");
                        double height = double.Parse(ReadLine());
                        shape = new Rectangle(width, height);
                        areaDelegate = shape.CalculateArea;
                        break;

                    case 3:
                        Clear();
                        Write("Введите основание треугольника: ");
                        double baseLength = double.Parse(ReadLine());
                        Write("Введите высоту треугольника:");
                        double triHeight = double.Parse(ReadLine());
                        shape = new Triangle(baseLength, triHeight);
                        areaDelegate = shape.CalculateArea;
                        break;

                    default:
                        WriteLine("\nНеверный выбор! Попробуйте снова.");
                        continue;
                }

                // Динамический вызов метода через делегат
                if (areaDelegate != null)
                {
                    WriteLine($"\nПлощадь выбранной фигуры: {areaDelegate()}");
                }

                // Возврат в меню
                WriteLine("\nНажмите любую клавишу для возврата в меню или '0' для выхода.");
                if (ReadKey().KeyChar == '0')
                {
                    exit = true;
                }
                Clear();  // Очищаем экран перед возвратом в меню
            }
        }
    }
}