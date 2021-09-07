using System.Threading.Tasks;

namespace Homework_04.Bookkeeping.Presenters
{
    public interface IEmployersReportPresenter
    {
        Task<string> PresentReport(EmployersYearReport report);
    }
}