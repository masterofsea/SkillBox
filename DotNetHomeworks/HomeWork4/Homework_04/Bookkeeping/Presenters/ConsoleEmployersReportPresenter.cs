using System.Text;
using System.Threading.Tasks;

namespace Homework_04.Bookkeeping.Presenters
{
    public class ConsoleEmployersReportPresenter : IEmployersReportPresenter
    {
        public Task<string> PresentReport(EmployersYearReport report)
        {
            var tcs = new TaskCompletionSource<string>();
            var sb = new StringBuilder();

            sb.AppendLine("Месяц\tДоход, тыс. руб.\tРасход, тыс. руб.\tПрибыль, тыс. руб.");
            foreach (var monthlyAccounting in report.AnnualAccounting)
            {
                sb.AppendLine(monthlyAccounting.Value.ToString());
            }

            tcs.SetResult(sb.ToString());
            return tcs.Task;
        }
    }
}