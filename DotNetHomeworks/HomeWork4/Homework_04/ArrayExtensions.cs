using System;
using System.Runtime.InteropServices;

namespace Homework_04
{
    /// <summary>
    /// Методы расширения для массива
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Возвращает строку из двумерного массива по её номеру
        /// </summary>
        /// <param name="array">исходный массив</param>
        /// <param name="rowNum">номер строки массива, начиная с нуля</param>
        /// <typeparam name="T">тип данных массива</typeparam>
        /// <returns>строка из двумерного массива</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
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

        /// <summary>
        /// Обновляет значение строки в двумерном массиве
        /// </summary>
        /// <param name="array">исходный массив</param>
        /// <param name="rowNum">номер строки массива, начиная с нуля</param>
        /// <param name="updatedRow">новая строка данных</param>
        /// <typeparam name="T">тип данных массива</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
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

        /// <summary>
        /// Метод вычисляющий размер типа данных в неуправляемой памяти
        /// </summary>
        /// <typeparam name="T">тип данных</typeparam>
        /// <returns>размер типа данных в байтах в неуправляемой памяти</returns>
        private static int CalculateUnmanagedSize<T>()
        {
            if (typeof(T) == typeof(bool)) return 1;
            
            return typeof(T) == typeof(char) ? 2 : Marshal.SizeOf<T>();
        }
    }
}