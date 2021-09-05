using System;
using System.Linq;
using System.Text;
using Homework_04.Bookkeeping;
using Homework_04.Math;

namespace Homework_04
{
    internal static class Program
    {
        public static void Main()
        {
            //TODO: вынести и распределить логику пользовательского интерфейса
            //TODO избавиться от DRY пренебрежений
            
            //Здравствуйте, извините меня за то, что здесь творится месиво, если необходимо я могу это завернуть
            //В красивый код, просто очень спешил на работу
            Console.WriteLine("Добро пожаловать в мое решение домашней работы #4.");

            while (true)
            {
                try
                {
                    Console.WriteLine("Чтобы рассмотреть решение задачи учета финансов нажмите 1");
                    Console.WriteLine("Чтобы рассмотреть решение задачи с треугольником Паскаля нажмите 2");
                    Console.WriteLine("Чтобы рассмотреть решение задачи с вычислениями над матрицами нажмите 3");
                    Console.WriteLine("Чтобы покинуть программу нажмите 4");

                    var key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Console.WriteLine("Добро пожаловать в программу бух. учета");
                            while (true)
                            {
                                Console.WriteLine("Нажмите любую клавишу чтобы сгенерировать отчет сотрудника " +
                                                  "или Esc чтобы выйти в меню");

                                if (Console.ReadKey(true).Key == ConsoleKey.Escape) break;

                                var employee = new Employee();

                                Console.WriteLine(employee);
                                var top3WorstProfitMonths =
                                    employee.AnnualAccounting.Where(i => 
                                        employee.AnnualAccounting.Select(k => k.Profit).Distinct().OrderByDescending(p => p)
                                            .Take(3).Contains(i.Profit));

                                var sb = new StringBuilder();
                                foreach (var month in top3WorstProfitMonths)
                                    sb.AppendFormat("{0} ", month.Month);

                                Console.WriteLine($"Худшая прибыль в месяцах: {sb}");

                                Console.WriteLine(
                                    $"Месяцев с положительной прибылью: " +
                                    $"{employee.AnnualAccounting.Count(i => i.Profit > 0)}");
                            }

                            break;

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            Console.WriteLine("Добро пожаловать в программу Треугольник Паскаля");
                            while (true)
                            {
                                try
                                {
                                    Console.WriteLine("Введите число строк подлежащих выводу для треугольника Паскаля" +
                                                      "или Esc чтобы выйти в меню");

                                    if (Console.ReadKey(true).Key == ConsoleKey.Escape) break;

                                    var lines = int.Parse(Console.ReadLine());

                                    var pascalsTriangle = PascalsTriangle.BuildTriangle(lines);

                                    Console.WriteLine(pascalsTriangle);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }

                            break;


                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            //TODO: Хранение в стеке результатов предыдущих вычислений(история операций)
                            Console.WriteLine("Добро пожаловать в программу Калькулятор матриц");
                            while (true)
                            {
                                try
                                {
                                    Console.WriteLine("Нажмите " +
                                                      "\n1 - Сложение" +
                                                      "\n2 - Вычитание" +
                                                      "\n3 - Умножение матрицы на число " +
                                                      "\n4 - Матричное произведение" +
                                                      "\nEsc чтобы выйти в меню");

                                    var keyDecision = Console.ReadKey(true).Key;
                                    if (keyDecision == ConsoleKey.Escape) break;

                                    switch (keyDecision)
                                    {
                                        case ConsoleKey.D1:
                                        case ConsoleKey.NumPad1:
                                            while (true)
                                            {
                                                try
                                                {
                                                    Console.WriteLine("Введите размер первой матрицы в формате строка " +
                                                                      "и столбец через пробел 4 4 например");
                                                    var shape1 = Console.ReadLine().Split();
                                                    var rows1 = int.Parse(shape1[0]);
                                                    var cols1 = int.Parse(shape1[1]);
                                                    Console.WriteLine("Введите размер второй матрицы");
                                                    var shape2 = Console.ReadLine().Split();
                                                    var rows2 = int.Parse(shape2[0]);
                                                    var cols2 = int.Parse(shape2[1]);

                                                    var leftMatrix = Matrix.CreateMatrix(rows1, cols1);
                                                    var rightMatrix = Matrix.CreateMatrix(rows2, cols2);
                                                    
                                                    leftMatrix.FillTheMatrix();
                                                    rightMatrix.FillTheMatrix();
                                                    var ret = Matrix.Add(leftMatrix, rightMatrix);

                                                    Console.WriteLine(leftMatrix);
                                                    Console.WriteLine("+");
                                                    Console.WriteLine(rightMatrix);
                                                    Console.WriteLine("=");
                                                    Console.WriteLine(ret);
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine(e.Message);
                                                }
                                                
                                                break;
                                            }
                                            
                                            break;
                                        case ConsoleKey.D2:
                                        case ConsoleKey.NumPad2:
                                            while (true)
                                            {
                                                try
                                                {
                                                    Console.WriteLine("Введите размер первой матрицы в формате строка " +
                                                                      "и столбец через пробел 4 4 например");
                                                    var shape1 = Console.ReadLine().Split();
                                                    var rows1 = int.Parse(shape1[0]);
                                                    var cols1 = int.Parse(shape1[1]);
                                                    Console.WriteLine("Введите размер второй матрицы");
                                                    var shape2 = Console.ReadLine().Split();
                                                    var rows2 = int.Parse(shape2[0]);
                                                    var cols2 = int.Parse(shape2[1]);

                                                    var leftMatrix = Matrix.CreateMatrix(rows1, cols1);
                                                    var rightMatrix = Matrix.CreateMatrix(rows2, cols2);
                                                    
                                                    leftMatrix.FillTheMatrix();
                                                    rightMatrix.FillTheMatrix();
                                                    var ret = Matrix.Subtract(leftMatrix, rightMatrix);

                                                    Console.WriteLine(leftMatrix);
                                                    Console.WriteLine("-");
                                                    Console.WriteLine(rightMatrix);
                                                    Console.WriteLine("=");
                                                    Console.WriteLine(ret);
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine(e.Message);
                                                }
                                                
                                                break;
                                            }
                                            break;
                                        
                                        case ConsoleKey.D3:
                                        case ConsoleKey.NumPad3:
                                            while (true)
                                            {
                                                try
                                                {
                                                    Console.WriteLine("Введите размер первой матрицы в формате строка " +
                                                                      "и столбец через пробел 4 4 например");
                                                    var shape1 = Console.ReadLine().Split();
                                                    var rows1 = int.Parse(shape1[0]);
                                                    var cols1 = int.Parse(shape1[1]);
                                                    Console.WriteLine("Введите множитель");
                                                    var coef = double.Parse(Console.ReadLine());

                                                    var leftMatrix = Matrix.CreateMatrix(rows1, cols1);
                                                    leftMatrix.FillTheMatrix();
                                                    var ret = Matrix.Multiply(leftMatrix, coef);

                                                    Console.WriteLine(leftMatrix);
                                                    Console.WriteLine("*");
                                                    Console.WriteLine(coef);
                                                    Console.WriteLine("=");
                                                    Console.WriteLine(ret);
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine(e.Message);
                                                }
                                                
                                                break;
                                            }
                                            break;
                                        
                                        case ConsoleKey.D4:
                                        case ConsoleKey.NumPad4:
                                            while (true)
                                            {
                                                try
                                                {
                                                    Console.WriteLine("Введите размер первой матрицы в формате строка " +
                                                                      "и столбец через пробел 4 4 например");
                                                    var shape1 = Console.ReadLine().Split();
                                                    var rows1 = int.Parse(shape1[0]);
                                                    var cols1 = int.Parse(shape1[1]);
                                                    Console.WriteLine("Введите размер второй матрицы");
                                                    var shape2 = Console.ReadLine().Split();
                                                    var rows2 = int.Parse(shape2[0]);
                                                    var cols2 = int.Parse(shape2[1]);

                                                    var leftMatrix = Matrix.CreateMatrix(rows1, cols1);
                                                    var rightMatrix = Matrix.CreateMatrix(rows2, cols2);
                                                    
                                                    leftMatrix.FillTheMatrix();
                                                    rightMatrix.FillTheMatrix();
                                                    var ret = Matrix.Dot(leftMatrix, rightMatrix);

                                                    Console.WriteLine(leftMatrix);
                                                    Console.WriteLine("*");
                                                    Console.WriteLine(rightMatrix);
                                                    Console.WriteLine("=");
                                                    Console.WriteLine(ret);
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine(e.Message);
                                                }
                                                
                                                break;
                                            }
                                            break;
                                        
                                    }
                                    
                                    
                                    
                                    
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }

                            break;

                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4:
                            Console.WriteLine("Досвидания! Спасибо за то что пользуетесь нашим программным продуктом!");
                            return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}