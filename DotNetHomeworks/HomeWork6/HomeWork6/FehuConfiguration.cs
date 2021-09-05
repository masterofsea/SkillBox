namespace HomeWork6
{
    public class FehuConfiguration
    {
        public string PathToOutputFile { get; set; }
        
        public int FileBufferSizeInBinaryLogValue { get; set; }
        
        public bool CalculateOnlyGroupsNum { get; set; }
        
        public bool UseArchiver { get; set; }

        public string ArchiveOutputFile { get; set; }
    }
}