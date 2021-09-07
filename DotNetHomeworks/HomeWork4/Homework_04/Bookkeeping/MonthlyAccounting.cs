namespace Homework_04.Bookkeeping
{
    /// <summary>
    /// Месячный отчет о прибыли сотрудника
    /// </summary>
    public class MonthlyAccounting
    {
        public MonthlyAccounting(Months month, decimal income, decimal expenses)
        {
            Month = month;
            Income = income;
            Expenses = expenses;
        }

        /// <summary>
        /// Месяц отчета
        /// </summary>
        public Months Month { get; }

        /// <summary>
        /// Доход
        /// </summary>
        public decimal Income { get; }

        /// <summary>
        /// Расходы
        /// </summary>
        public decimal Expenses { get; }

        
        /// <summary>
        /// Прибыль
        /// </summary>
        public decimal Profit => Income - Expenses;
        

        public override string ToString()
        {
            return $"{Month}\t{Income}\t\t\t{Expenses}\t\t\t{Profit}";
        }
    }
}