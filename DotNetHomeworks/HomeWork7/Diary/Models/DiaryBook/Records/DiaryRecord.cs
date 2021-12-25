using System;
using JetBrains.Annotations;

namespace Diary.Models.DiaryBook.Records
{
    [UsedImplicitly]
    public class DiaryRecord
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public string Text { get; set; }

        public Actions Actions { get; set; }

        public Mood Mood { get; set; }

        public string Hashtags { get; set; }
    }
}