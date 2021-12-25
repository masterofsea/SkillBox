using System;
using System.Threading.Tasks;

namespace Diary.Models.DiaryBook.PersistentManager
{

    public interface IDiaryBookPersistentManager
    {
        Task SaveDiaryBook(Models.DiaryBook.DiaryBook diaryBook);

        DiaryBook LoadDiaryBookInMemory();

        DiaryBook LoadDiaryBookInMemoryWithRecordsWithinDateTimeInterval(DateTime begin, DateTime end);
    }
}