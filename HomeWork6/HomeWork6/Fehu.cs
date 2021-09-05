using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using HomeWork6.Archivers;
using HomeWork6.GroupsCalculator;
using HomeWork6.GroupsWriters;
using HomeWork6.NLoaders;
using Microsoft.Extensions.Options;

namespace HomeWork6
{
    public class Fehu
    {
        private INloader NLoader { get; }
        private FehuConfiguration Configuration { get; }
        private IGroupsCounter GroupsCounter { get; }
        private IGroupsArchiver Archiver { get; }


        public Fehu(INloader nLoader, IOptions<FehuConfiguration> configuration, IGroupsCounter groupsCounter,
            IGroupsArchiver archiver)
        {
            NLoader = nLoader;

            GroupsCounter = groupsCounter;

            Archiver = archiver;

            Configuration = configuration.Value;
        }

        public void LoadRune()
        {
            var n = NLoader.LoadN();
            var groupsCount = GroupsCounter.CountGroups(n);

            Console.WriteLine($"Число групп равно {groupsCount}");

            if (Configuration.CalculateOnlyGroupsNum) return;


            var writer = new FileGroupWriter(Configuration.PathToOutputFile, Configuration.FileBufferSizeInBinaryLogValue, groupsCount, n);
            var clock = Stopwatch.StartNew();

            writer.WriteGroups();

            clock.Stop();

            Console.WriteLine($"Elapsed {clock.ElapsedMilliseconds} ms");


            if (!Configuration.UseArchiver) return;
            
            Archiver.ArchiveGroups(Configuration.PathToOutputFile,
                Configuration.ArchiveOutputFile, Configuration.PathToOutputFile);

            Console.WriteLine(
                $"Size until archived: {new FileInfo(Configuration.PathToOutputFile).Length} bytes" +
                $"\nSize of archived file:{new FileInfo(Configuration.ArchiveOutputFile).Length} bytes");
        }
    }

    public static class StreamWriterExtensions
    {
        public static void WriteAndCleanBuffer(this StreamWriter writer, StringBuilder buffer)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (buffer.Length == 0) writer.Write(Environment.NewLine);

            for (var i = 0; i < buffer.Length; ++i) writer.Write(buffer[i]);

            buffer.Clear();
        }

        public static void WriteLineAndCleanBuffer(this StreamWriter writer, StringBuilder buffer)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (buffer.Length == 0) writer.Write(Environment.NewLine);

            for (var i = 0; i < buffer.Length; ++i) writer.Write(buffer[i]);
            writer.WriteLine();
            buffer.Clear();
        }

        public static void WriteNumAsSeparatedString(this BinaryWriter writer, int n, char separator = ' ')
        {
            var significance = CalculateSignificance(n);
            Span<byte> stackBytes = stackalloc byte[significance + 1];

            for (var i = significance - 1; i >= 0; --i)
            {
                stackBytes[i] = (byte) (n % 10 + 48);

                n /= 10;
            }

            stackBytes[significance] = (byte) separator;

            writer.Write(stackBytes);
        }

        private static int CalculateSignificance(int n)
        {
            var acc = 0;
            while (n > 0)
            {
                ++acc;
                n /= 10;
            }

            return acc;
        }
    }
}