using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework_04.Bookkeeping
{
    /// <summary>
    /// Тестовая реализация интерфейса годового отчета сотрудника
    /// </summary>
    public class EmployersYearReport : IEmployersYearReport
    {
        public IDictionary<Months, MonthlyAccounting> AnnualAccounting { get; } = new Dictionary<Months, MonthlyAccounting>();
       

        public void AddOrUpdateAnnualAccounting(MonthlyAccounting accounting)
        {
            if (AnnualAccounting.ContainsKey(accounting.Month)) AnnualAccounting[accounting.Month] = accounting;
            
            else AnnualAccounting.Add(accounting.Month, accounting);
        }

        public int GetNumOfMonthsWithPositiveProfit() =>
            AnnualAccounting.Count(kv => kv.Value.Profit > 0);

        public IEnumerable<Months> GetMonthsWithTopWorstProfitWithTies(int topLimit)
        {
            return AnnualAccounting.Where(i => 
                AnnualAccounting.Select(kv => kv.Value.Profit).Distinct().OrderByDescending(p => p)
                    .Take(topLimit).Contains(i.Value.Profit)).Select(i => i.Value.Month);
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