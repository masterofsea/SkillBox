using System;
using System.Text;
using HomeWork_02.Models.ConcreteSubjects;

namespace HomeWork_02.Models
{
    /// <summary>
    /// Класс представляющий студента
    /// </summary>
    public class Student
    {
        public string Name { get; }

        public int Age { get; }

        public int HeightInSm { get; }

        public Subject[] Subjects { get; }

        public Student(string name, int age, int heightInSm, params Subject[] subjects)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name must be have at least 1 not whitespace symbol");

            if (age < 0 || heightInSm < 0)
                throw new ArgumentException("age and height must have positive values");

            Subjects = subjects ?? throw new Exception("Subjects must have at least single value");

            Name = name;

            Age = age;

            HeightInSm = heightInSm;
        }

        public void ShowAcademicPerformance(bool formatAtCenter = false)
        {
            for (var subjectIndex = 0; subjectIndex < Subjects.Length; ++subjectIndex)
            {
                var outputString = $"{Subjects[subjectIndex]}\tSubject average performance: " +
                                   $"{Subjects[subjectIndex].CalculateAvgRating():f1}\n";
                if (formatAtCenter)
                {
                    //Выводим строку в центре экрана
                    var centerX = (Console.WindowWidth / 2) - (outputString.Length / 2);

                    var centerY = (Console.WindowHeight / 2) - 1 + subjectIndex;
                    
                    Console.SetCursorPosition(centerX, centerY);
                    
                    Console.WriteLine(outputString);
                }
                else
                {
                    Console.WriteLine(outputString);
                }
            }
            
            //Сбрасываем курсор в 0
            Console.SetCursorPosition(0, 0);
        }
    }
}