namespace HomeWork5
{
    public static class MathHelper
    {
        public static int AckermanFunc(int n, int m)
        {
            if (n == 0) return m + 1;

            return AckermanFunc(n - 1, m == 0 ? 1 : AckermanFunc(n, m - 1));
        }
    }
}