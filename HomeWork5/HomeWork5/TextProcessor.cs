using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HomeWork5
{
    public class TextProcessor
    {
        private string[] IndexByLettersNum { get; }
        
        private string[] MainTable { get; }

        public TextProcessor(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new NullReferenceException("Text must not be null or empty");

            var words = Regex.Split(text, @"\s+").Where(s => s != string.Empty);

            var enumerable = words as string[] ?? words.ToArray();
            IndexByLettersNum = enumerable.OrderBy(i => i.Length).ToArray();

            MainTable = enumerable.OrderBy(i => i).ToArray();
        }
        
        /// <summary>
        /// Метод, возвращающий слово(слова), содержащее минимальное количество букв
        /// </summary>
        /// <returns>слово(слова) c минимальным количеством букв</returns>
        /// <exception cref="NullReferenceException"></exception>
        public IEnumerable<string> FindWordWithMinLettersNum() =>
            IndexByLettersNum.Where(i => i.Length == IndexByLettersNum[0].Length)
                .Select(i => new string(i.ToCharArray()));


        /// <summary>
        /// Метод, возвращающий слово(слова) с максимальным количеством букв.
        /// </summary>
        /// <returns>слово(слова) с максимальным количеством букв</returns>
        /// <exception cref="NullReferenceException"></exception>
        public IEnumerable<string> FindWordWithMaxLettersNum()
        {
            var maxLengthWord = IndexByLettersNum.Last().Length;

            for (var i = IndexByLettersNum.Length; ; )
            {
                if (IndexByLettersNum[--i].Length == maxLengthWord) 
                    yield return new string(IndexByLettersNum[i].ToCharArray());
                
                else yield break;
            }
        }
        
        
        /// <summary>
        /// Метод, возвращающий текст в котором удалены все кратные рядом стоящие символы, оставив по одному
        /// </summary>
        /// <returns>Текст в котором удалены все кратные рядом стоящие символы</returns>
        /// <exception cref="NullReferenceException"></exception>
        public string[] GetTextWithoutRepeatingLetters()
        {
            var wordsWithoutRepeats = new string[MainTable.Length];
            for (var i = 0; i < MainTable.Length; ++i)
            {
                var bufferedLetter = MainTable[i][0];
                var wordBuilder = new List<char> {bufferedLetter};
                
                for (var j = 1; j < MainTable[i].Length; ++j)
                {
                    if (MainTable[i][j] == bufferedLetter) continue;
                    
                    bufferedLetter = MainTable[i][j];
                    wordBuilder.Add(bufferedLetter);
                }
                
                wordsWithoutRepeats[i] = new string(wordBuilder.ToArray());
            }

            return wordsWithoutRepeats;
        }
    }
}