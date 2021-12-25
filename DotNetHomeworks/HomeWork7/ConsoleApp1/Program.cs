using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal static class Program
    {
        private static int _acc = 0;

        private static void Main(string[] args)
        {
            var taskArr = new Task[10000];
            for (var i = 0; i < 10000; ++i)
            {
                taskArr[i] = Task.Run(() =>
                {
                    _acc += 1;
                });
            }

            (Task.WhenAll(taskArr)).Wait();

            Console.WriteLine(_acc);
        }
    }
}