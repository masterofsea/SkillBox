using System;
using System.Runtime.InteropServices;

namespace Homework_04
{
    public static class ArrayExtensions
    {
        public static T[] GetRow<T>(this T[,] array, int rowNum)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            
            if (!typeof(T).IsPrimitive)
                throw new InvalidOperationException("Not supported for managed types.");

            var cols = array.GetLength(1);
            var result = new T[cols];

            var size = CalculateUnmanagedSize<T>();

            Buffer.BlockCopy(array, rowNum*cols*size, result, 0, cols*size);

            return result;
        }

        public static void SetRow<T>(this T[,] array, int rowNum, T[] updatedRow)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            
            if (!typeof(T).IsPrimitive)
                throw new InvalidOperationException("Not supported for managed types.");
            
            var cols = array.GetLength(1);
            var size = CalculateUnmanagedSize<T>();

                       Buffer.BlockCopy(updatedRow, 0, array, rowNum*cols*size, cols*size);
        }

        private static int CalculateUnmanagedSize<T>()
        {
            if (typeof(T) == typeof(bool)) return 1;
            
            return typeof(T) == typeof(char) ? 2 : Marshal.SizeOf<T>();
        }
    }
}