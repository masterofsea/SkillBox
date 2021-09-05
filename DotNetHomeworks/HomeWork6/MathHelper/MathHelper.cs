using System.Runtime.CompilerServices;

namespace MathHelper
{
    public static class MathHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateNearestBinaryNum(int x) {
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            
            return x - (x >> 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CalculateNumOfElderByte(int x)
        {
            x = (x & 0x55555555) + ((x >> 1) & 0x55555555);
            x = (x & 0x33333333) + ((x >> 2) & 0x33333333);
            x = (x & 0x0F0F0F0F) + ((x >> 4) & 0x0F0F0F0F);
            x = (x & 0x00FF00FF) + ((x >> 8) & 0x00FF00FF);
            
            return (x & 0x0000FFFF) + (x >> 16); 
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Pow2(int pow) => 1 << pow;
    }
}