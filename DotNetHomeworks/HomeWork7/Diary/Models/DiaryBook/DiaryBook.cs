using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Diary.Models.DiaryBook.Exceptions;
using Diary.Models.DiaryBook.Records;
using JetBrains.Annotations;

namespace Diary.Models.DiaryBook
{
    [UsedImplicitly]
    public class DiaryBook : IDiaryBook
    {
        private int _recordId;
        public IDictionary<int, DiaryRecord> DiaryRecords { get; } = new Dictionary<int, DiaryRecord>();

        public void AddRecord(DiaryRecord record)
        {
            if (DiaryRecords.ContainsKey(record.Id)) 
                throw new RecordWithDefineIdAlreadyExistsException(
                    "Попытка создания записи, нарушающей ограничение уникальности идентификатора PK_Constraint");

            if (record.Id == 0) record.Id = Interlocked.Increment(ref _recordId);
            if (record.CreationDate == default) record.CreationDate = DateTime.Now;

            UpdateId();
            DiaryRecords.TryAdd(record.Id, record);
        }

        public void DeleteRecordById(int id)
        {
            if (!DiaryRecords.ContainsKey(id)) throw new RecordDoesNotExistsException("Попытка удаления несуществующей записи"); 
            
            DiaryRecords.Remove(id);
        }

        public void DeleteRecordByPattern(DiaryRecord patterRecord)
        {
            var keys = DiaryRecords.Where((k => k.Value.Actions == patterRecord.Actions ||
                                                k.Value.Hashtags == patterRecord.Hashtags ||
                                                k.Value.Id == patterRecord.Id ||
                                                k.Value.Mood == patterRecord.Mood ||
                                                k.Value.CreationDate == patterRecord.CreationDate))
                .Select(kv => kv.Key);

            foreach (var key in keys) DeleteRecordById(key);
        }

        public void UpdateDiaryRecord(DiaryRecord updatedRecord)
        {
            if (!DiaryRecords.TryGetValue(updatedRecord.Id, out _)) 
                throw new RecordDoesNotExistsException("Попытка обновления несуществующей записи");

            DiaryRecords[updatedRecord.Id] = updatedRecord;
        }


        public void GenerateNewRecord()
        {
            var rnd = new Random();
            var id = Interlocked.Increment(ref _recordId);
            
            DiaryRecords.Add(id, new DiaryRecord()
            {
                Actions = Actions.Learning,
                Hashtags = $"#{rnd.Next()}#{rnd.Next()}#{rnd.Next()}",
                Id = id,
                Mood = Mood.Happy,
                Text = rnd.Next().ToString(),
                CreationDate = DateTime.Now - TimeSpan.FromDays(rnd.Next(0, 1000))
            });
        }
        
        public IEnumerable<DiaryRecord> GetOrderedEnumerable(OrderField orderField)
        {
            switch (orderField)
            {
                case OrderField.Actions:
                    return DiaryRecords.Values.OrderBy(v => v.Actions);
                case OrderField.Id:
                    return DiaryRecords.Values.OrderBy(v => v.Id);
                case OrderField.CreationDate:
                    return DiaryRecords.Values.OrderBy(v => v.CreationDate);
                case OrderField.Text:
                    return DiaryRecords.Values.OrderBy(v => v.Text);
                case OrderField.Mood:
                    return DiaryRecords.Values.OrderBy(v => v.Mood);
                case OrderField.Hashtags:
                    return DiaryRecords.Values.OrderBy(v => v.Hashtags);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderField), orderField, null);
            }
        }

        public IEnumerator<DiaryRecord> GetEnumerator() =>
            DiaryRecords.Select(record => record.Value).GetEnumerator();
        
        

        public void UpdateId()
        {
            _recordId = DiaryRecords.Keys.Max() + 1;
        }
    }
}