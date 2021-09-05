using System.IO;
using System.IO.Compression;

namespace HomeWork6.Archivers
{
    public class ZipDefaultArchiver : IGroupsArchiver
    {
        private const string Format = ".zip";
        public void ArchiveGroups(string srcName, string archiveName, string entryName)
        {
            var archName = archiveName.EndsWith(Format) ? archiveName : archiveName + Format;
            
            if (File.Exists(archName)) File.Delete(archName);
            using var zip = ZipFile.Open(archName, ZipArchiveMode.Create);
            zip.CreateEntryFromFile(srcName, entryName);
        }
    }
}