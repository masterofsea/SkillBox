using System.Threading.Tasks;


namespace HomeWork6
{
    internal static class Program
    {
        private static void Main()
        {
            InjectionRoot.Build<Fehu>().LoadRune();
        }
    }
}