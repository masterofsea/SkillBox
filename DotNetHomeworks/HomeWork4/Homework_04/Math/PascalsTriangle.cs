using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Homework_04.Math
{
    /// <summary>
    /// Класс представляющий Треугольник Паскаля
    /// </summary>
    internal class PascalsTriangle
    {
        /// <summary>
        /// Синглтон Треугольника Паскаля
        /// </summary>
        private static readonly List<Lazy<BigInteger>[]> BackArray = new List<Lazy<BigInteger>[]>(0)
            {new[] {new Lazy<BigInteger>(() => 1)}};

        /// <summary>
        /// Объект синхронизации, согласующий доступ к синглтон треугольнику Паскаля
        /// </summary>
        private static readonly object SyncObject = new object();

        /// <summary>
        /// Число строк в экземляре треугольника Паскаля
        /// </summary>
        public int Rows { get; }

        /// <summary>
        /// Индексатор позволяющий получить значение из определенной строки под определененым номером из треугольника Паскаля
        /// </summary>
        /// <param name="row">номер строки в труегольнике Паскаля</param>
        /// <param name="elementIndex">номер элемента в строке</param>
        /// <exception cref="ArgumentException"></exception>
        public BigInteger this[int row, int elementIndex]
        {
            get
            {
                if(row < 0 || elementIndex < 0 || elementIndex > row || row > Rows) 
                    throw new ArgumentException("Invalid row or element index");

                return BackArray[row][elementIndex].Value;
            }
        }

        /// <summary>
        /// Инициализирует треугольник Паскаля количеством элементов в нем
        /// </summary>
        /// <param name="rows"></param>
        private PascalsTriangle(int rows)
        {
            Rows = rows;
        }

        /// <summary>
        /// Словарь для мемоизации функции Факториала
        /// </summary>
        private static ConcurrentDictionary<long, BigInteger> FactorialMemory { get; } =
            new ConcurrentDictionary<long, BigInteger>();


        /// <summary>
        /// Реализация факториала через хвостовую рекурсию
        /// </summary>
        /// <param name="n"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        private static BigInteger FactorialTimes(long n, BigInteger acc) =>
            (n <= 0) ? acc : FactorialTimes(n - 1, acc * n);

        /// <summary>
        /// Мемоизированный Метод возвращающий значение факториала числа n
        /// </summary>
        /// <param name="n">Число для которого вычисляется факториал</param>
        /// <returns></returns>
        public static BigInteger Factorial(int n) =>
            FactorialMemory.GetOrAdd(n, FactorialTimes(n, 1));


        /// <summary>
        /// Метод построения ленивого треугольника Паскаля
        /// </summary>
        /// <param name="rows">чисто строк в треугольнике Паскаля</param>
        /// <returns>Возвращает ленивый треугольник Паскаля</returns>
        /// <exception cref="ArgumentException"></exception>
        public static PascalsTriangle BuildTriangle(int rows)
        {
            if (rows < 0) throw new ArgumentException();

            if (BackArray.Count >= rows) return new PascalsTriangle(rows);
            
            lock (SyncObject)
            {
                if (BackArray.Count >= rows) return new PascalsTriangle(rows);

                for (var n = BackArray.Count; n < rows; ++n)
                {
                    var length = n + 1;
                    var nextRow = new Lazy<BigInteger>[length];

                    for (var m = 0; m < length; ++m)
                    {
                        //avoid closures of indexes
                        var row = n;
                        var elementIndex = m;
                        nextRow[m] = new Lazy<BigInteger>(() =>
                            Factorial(row) / (Factorial(elementIndex) * Factorial(row - elementIndex)));

                        //Console.WriteLine($"n = {n} m = {m} val = {nextRow[m].Value}");
                    }

                    BackArray.Add(nextRow);
                }
            }

            return new PascalsTriangle(rows);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Rows > 12) return "Треугольник слишком велик для вывода на экран";
            
            for (var row = 0; row < Rows; ++row)
            {
                sb.Append(new string(' ', BackArray[Rows - 1].Length - row));
                foreach (var element in BackArray[row])
                    sb.AppendFormat("{0} ", element.Value);

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is PascalsTriangle triangleLink && triangleLink.Rows == Rows;
        }

        public override int GetHashCode()
        {
            if (Rows == 0) return 0;

            return (int) System.Math.Pow(2, BackArray.Count);
        }
    }
}