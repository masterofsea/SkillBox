using System;
using System.Collections;

namespace HomeWork5
{
    public static class SequenceChecker//<T> where T IEquatable<int>, IEquatable<long>, IEquatable<ulong>:
    {
        public static bool IsArithmeticProgression(int[] sequence)
        {
            unsafe
            {
                if (sequence == null) 
                    throw new ArgumentException("Passed argument is not a sequence");

                if (sequence.Length < 3) return true;
            
                fixed(int* p = sequence)
                {
                    for (var i = 1; i < sequence.Length - 1; ++i)
                        if (*(p + i) * 2 != *(p + i - 1) + *(p + i + 1)) return false;
                        

                    return true;
                }
            }
        }
        
        
        public static bool IsGeometricProgression(params int[] sequence)
        {
            unsafe
            {
                if (sequence == null) 
                    throw new ArgumentException("Passed argument is not a sequence");
                
                if (sequence.Length < 3) return true;

                fixed(int* p = sequence)
                {
                    for (var i = 1; i < sequence.Length - 1; ++i)
                        if (*(p + i) * *(p + i) != *(p + i - 1) * *(p + i + 1)) return false;

                    return true;
                }
            }
        }
        
    }
}