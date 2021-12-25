using System;
using System.Threading.Tasks;
using Diary.Models.DiaryBook.PersistentManager;

namespace DiaryBookTester
{
    internal static class Program
    {
        private static async Task Main()
        {
            var diaryBook = new Diary.Models.DiaryBook.DiaryBook();

            for (var i = 0; i < 1000; ++i)
                diaryBook.GenerateNewRecord();

            var diaryPersistentManager = new DiaryBookPersistentManager("C://1");
            await diaryPersistentManager.SaveDiaryBook(diaryBook);


            var diaryBookLoaded = diaryPersistentManager.LoadDiaryBookInMemory();
            
            var sortedBookLoaded = diaryPersistentManager.LoadDiaryBookInMemoryWithRecordsWithinDateTimeInterval(DateTime.Now- TimeSpan.FromDays(10), DateTime.Now);
        }
    }
}