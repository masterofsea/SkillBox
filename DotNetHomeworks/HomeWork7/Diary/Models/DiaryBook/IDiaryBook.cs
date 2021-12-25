using System.Collections.Generic;
using Diary.Models.DiaryBook.Records;

namespace Diary.Models.DiaryBook
{
    public interface IDiaryBook
    {
        IDictionary<int, DiaryRecord> DiaryRecords { get; }

        void AddRecord(DiaryRecord record);

        void DeleteRecordById(int id);

        void DeleteRecordByPattern(DiaryRecord patterRecord);

        void UpdateDiaryRecord(DiaryRecord updatedRecord);

        IEnumerable<DiaryRecord> GetOrderedEnumerable(OrderField orderField);
    }
}