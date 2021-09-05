using System;
using System.IO;
using static MathHelper.MathHelper;

namespace HomeWork6.GroupsWriters
{
    public class FileGroupWriter : IGroupWriter
    {
        private string OutputFilePath { get; }
        private int FileBufferSizeInBinaryLogValue { get; }
        private int GroupsCount { get; }
        private int Num { get; }

        public FileGroupWriter(string outputFilePath, int fileBufferSizeInBinaryLogValue, int groupsCount, int num)
        {
            if (string.IsNullOrEmpty(outputFilePath))
                throw new ArgumentException("Incorrect file name");

            if (fileBufferSizeInBinaryLogValue < 0 || fileBufferSizeInBinaryLogValue > 16)
                throw new ArgumentException("Incorrect buffer size");
            
            
            OutputFilePath = outputFilePath;
            FileBufferSizeInBinaryLogValue = fileBufferSizeInBinaryLogValue;
            GroupsCount = groupsCount;
            Num = num;
        }

        public void WriteGroups()
        {
            using var writer = new BinaryWriter(new FileStream(OutputFilePath, FileMode.Create, FileAccess.Write, FileShare.Write,
                Pow2(FileBufferSizeInBinaryLogValue)));

            for (var i = 0; i < GroupsCount; ++i)
            {
                var upperLim = Pow2(i + 1);

                for (var k = Pow2(i); k < upperLim; ++k) writer.WriteNumAsSeparatedString(k);

                writer.Write((byte) '\n');
            }

            for (var k = Pow2(GroupsCount); k <= Num; ++k) writer.WriteNumAsSeparatedString(k);
        }
    }
}