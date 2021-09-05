using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWork_02.Models.ConcreteSubjects
{
    /// <summary>
    /// Абстрактный класс реализующий общие для всех предметов методы и предоставляющий
    /// для обязательной реализации набор членов. Представляет из себя совокупность данных о самом предмете, а так же
    /// хранит в себе успеваемость конкретного студента. Несмотря на то, что данное решение нарушает принципы ООП
    /// и грамотное разделение интерфейсов это сойдет для второго домашнего задания.
    /// </summary>
    public abstract class Subject
    {
        //Название предмета
        public abstract string SubjectName { get; }

        protected List<int> AcademicPerformance { get; } = new List<int>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grade">оценка добавляемая в журнал предмета</param>
        public void Rate(int grade)
        {
            if (grade < 0) throw new ArgumentException("Rate must have a positive value");

            AcademicPerformance.Add(grade);
        }

        /// <summary>
        /// Данный метод возвращает среднюю успеваемость студента по предмету за все время
        /// </summary>
        /// <returns>средний итоговый результат</returns>
        public double CalculateAvgRating()
        {
            if (AcademicPerformance.Count == 0) return 0;

            return (double) AcademicPerformance.Sum() / AcademicPerformance.Count;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(SubjectName);
            sb.Append(": ");
            if (AcademicPerformance.Count == 0) sb.Append("No marks have been given yet");
            
            foreach (var rate in AcademicPerformance)
            {
                sb.Append($"{rate}, ");
            }

            return sb.ToString();
        }
    }
}