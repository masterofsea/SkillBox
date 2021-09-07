using System.Collections.Generic;

namespace Homework_04.Bookkeeping
{
    /// <summary>
    /// Данный интерфейс предоставляет возможности по анализу годового отчета доходов сотрудника
    /// </summary>
    public interface IEmployersYearReport
    {
        /// <summary>
        /// Словарь представляет данные удобным для чтения образа в формате отношения месяца и месячного отчета
        /// </summary>
        IDictionary<Months, MonthlyAccounting> AnnualAccounting { get; }
        
        /// <summary>
        /// Метод добавления в случае отсутствия за месяц или обновления присутствующего месячного отчета
        /// </summary>
        /// <param name="accounting">месячный отчет</param>
        void AddOrUpdateAnnualAccounting(MonthlyAccounting accounting);

        /// <summary>
        /// Метод возвращает количество месяцев с положительной прибылью
        /// </summary>
        /// <returns></returns>
        int GetNumOfMonthsWithPositiveProfit();

        /// <summary>
        /// Данный метод возвращает перечисление топ N месяцев в которых прибыль была в пределах 3 самых неприбыльных различных прибылей
        /// </summary>
        /// <param name="topLimit">количество в топе</param>
        /// <returns>перечисление топ N месяцев в которых прибыль была в пределах 3 самых неприбыльных различных прибылей</returns>
        IEnumerable<Months> GetMonthsWithTopWorstProfitWithTies(int topLimit);
    }
}