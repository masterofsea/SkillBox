using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Diary.Models.DiaryBook.PersistentManager
{
    public class DiaryBookPersistentManager : IDiaryBookPersistentManager
    {
        private string DiskSpacePath { get; }
        private Lazy<JsonSerializer> Serializer { get; } = new(new JsonSerializer());


        public DiaryBookPersistentManager(string diskSpacePath)
        {
            DiskSpacePath = diskSpacePath;
        }


        public async Task SaveDiaryBook(DiaryBook diaryBook)
        {
            await using var writer = new StreamWriter(
                new FileStream($"{DiskSpacePath}/Diary.json", FileMode.Create));
            using var jsonWriter = new JsonTextWriter(writer);

            Serializer.Value.Serialize(writer, diaryBook);
        }

        public DiaryBook LoadDiaryBookInMemory()
        {
            using var file = File.OpenText(@$"{DiskSpacePath}/Diary.json");
            var jsonReader = new JsonTextReader(file);
            
            return Serializer.Value.Deserialize<DiaryBook>(jsonReader);
        }

        public DiaryBook LoadDiaryBookInMemoryWithRecordsWithinDateTimeInterval(DateTime begin, DateTime end)
        {
            using var file = File.OpenText(@$"{DiskSpacePath}/Diary.json");
            var jsonReader = new JsonTextReader(file);
            
            var eagerLoadBook = Serializer.Value.Deserialize<DiaryBook>(jsonReader);

            var sortedDiaryBook = new DiaryBook();
            
            foreach (var record in eagerLoadBook)
                if (record.CreationDate > begin && record.CreationDate < end)
                    sortedDiaryBook.AddRecord(record);
            

            return sortedDiaryBook;
        }
    }
}