using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework_04.Bookkeeping
{
    public class Employee
    {
        public MonthlyAccounting[] AnnualAccounting { get; } = new MonthlyAccounting[12];

        public Employee()
        {
            var randomizer = new Random();
            for (var i = 1; i <= AnnualAccounting.Length; ++i)
            {
                AnnualAccounting[i - 1] = new MonthlyAccounting((Months) i,
                    randomizer.Next(50000, 120000), randomizer.Next(40000, 120000));
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Месяц\tДоход, тыс. руб.\tРасход, тыс. руб.\tПрибыль, тыс. руб.");
            foreach (var monthlyAccounting in AnnualAccounting)
            {
                sb.AppendLine(monthlyAccounting.ToString());
            }

            return sb.ToString();
        }
    }

    public static class LinqExtensions
    {
        public static IEnumerable<T> TopWithTies<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector,
            int n, bool orderByAsc = true)
        {
            IEnumerable<T> orderedEnumerable =
                !orderByAsc ? enumerable.OrderByDescending(selector) : enumerable.OrderBy(selector);

            return
            (
                from p in orderedEnumerable
                let topNValues = orderedEnumerable.Take(n).Select(selector).Distinct()
                where topNValues.Contains(selector(p))
                select p
            );
        }
    }
}