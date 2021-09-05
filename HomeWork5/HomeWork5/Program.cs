using System;
using System.Linq;
using System.Text;

namespace HomeWork5
{
    internal static class Program
    {
        //Очень нравится оператор goto который на самом деле лежит под капотом всех for while do while if else))
        //Он в таких ситуациях как эта уменьшает степень вложенности и следовательно увеличивает читаемость
        //Это и так понятно, что интерфейс в принципе реализовывать стоит по-другому, но я не буду 
        //Так как хорошее решение не ценится там, где можно обойтись камнем и палкой
        //Таков мой опыт
        private static void Main()
        {
            Start:
            Console.WriteLine();
            Console.WriteLine("Здравствуй, cтаринный друг!\n" +
                              "Выберите интересующую Вас программу:\n1.Програма работы с текстом" +
                              "\n2.Программа работы с подпоследовательностями\n3.Функция Аккермана" +
                              "\nEsc.Выйти из программы");

            while (true)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        goto TextProcessor;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        goto SequenceChecker;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        goto Ackerman;
                    case ConsoleKey.Escape:
                        goto End;
                    default:
                        continue;
                }
            }


            TextProcessor:
            {
                Console.WriteLine("Добро пожаловать в программу обработки текстов");

                Console.WriteLine("Введи текст и узнай о нем много интересного");

                var text = Console.ReadLine();

                try
                {
                    var txtProcessor = new TextProcessor(text);

                    var sb = new StringBuilder();
                    sb.AppendLine("Самые короткие слова:");
                    sb.AppendLine(string.Join(" ", txtProcessor.FindWordWithMinLettersNum()));
                    sb.AppendLine("Самые длинные слова:");
                    sb.AppendLine(string.Join(" ", txtProcessor.FindWordWithMaxLettersNum()));
                    sb.AppendLine("Текст без дублирующихся символов:");
                    sb.AppendLine(string.Join(" ", txtProcessor.GetTextWithoutRepeatingLetters()));
                    Console.WriteLine(sb.ToString());
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Попробуй еще раз");

                    goto TextProcessor;
                }
            }

            goto Start;


            SequenceChecker:
            {
                Console.WriteLine("Добро пожаловать в программу обработки подпоследовательностей");
                Console.WriteLine(
                    "Введи подпоследовательность через пробел и узнай является ли она геометрической " +
                    "или арифметической прогрессией " +
                    "(На данный момент последовательность ограничена типом int)");

                var rawSequence = Console.ReadLine();

                try
                {
                    var sequence = rawSequence.Split().Where(i => !string.IsNullOrWhiteSpace(i))
                        .Select(int.Parse).ToArray();

                    Console.WriteLine("Последовательность является арифметической прогрессией? "
                                      + SequenceChecker.IsArithmeticProgression(sequence));

                    Console.WriteLine("Последовательность является геометрической прогрессией? "
                                      + SequenceChecker.IsGeometricProgression(sequence));
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Попробуй еще раз");

                    goto SequenceChecker;
                }
            }

            goto Start;

            Ackerman:
            {
                Console.WriteLine("Добро пожаловать в программу вычисления функции аккермана");
                Console.WriteLine("Введите два числа через пробел");
                try
                {
                    var nums = Console.ReadLine().Split().Where(i => !string.IsNullOrWhiteSpace(i))
                        .Select(int.Parse).ToArray();

                    if (nums.Length != 2) throw new ArgumentException("Ровно 2 числа, пожалуйста!");
                    
                    Console.WriteLine($"Результат вычисления функции Аккермана: " +
                                      $"{MathHelper.AckermanFunc(nums[0], nums[1])}");
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Попробуй еще раз");

                    goto Ackerman;
                }
            }

            goto Start;

            End:
            {
                Console.WriteLine("Пока, друг!");
            }
        }
    }
}