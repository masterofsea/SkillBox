namespace HomeWork6.Archivers
{
    public interface IGroupsArchiver
    {
        public void ArchiveGroups(string pathToFile, string outputFile, string entryName);
    }
}