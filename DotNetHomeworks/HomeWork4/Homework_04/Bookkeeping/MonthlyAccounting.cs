namespace Homework_04.Bookkeeping
{
    public class MonthlyAccounting
    {
        public MonthlyAccounting(Months month, decimal income, decimal expenses)
        {
            Month = month;
            Income = income;
            Expenses = expenses;
        }

        public Months Month { get; }

        public decimal Income { get; }

        public decimal Expenses { get; }

        public decimal Profit => Income - Expenses;
        

        public override string ToString()
        {
            return $"{Month}\t{Income}\t\t\t{Expenses}\t\t\t{Profit}";
        }
    }
}