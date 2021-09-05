using System;

namespace Homework1
{
    internal static class Program
    {
        private static void Main()
        {
            #region Домашнее задание

            // Уровень сложности: просто
            // Задание 1. Переделать программу так, чтобы до первой волны увольнени в отделе было не более 20 сотрудников
            #region Задание 1.
            Console.WriteLine("Задание номер 1");
            TestEmployeeRepository(20, new Action<Repository>[] {repo =>
            {
                repo.DeleteWorkerByName("Агата");
            },
            repo => repo.DeleteWorkerByName("Аделина")});
            #endregion


            // Уровень сложности: средняя сложность
            // * Задание 2. Создать отдел из 40 сотрудников и реализовать несколько увольнений, по результатам
            //              которых в отделе должно остаться не более 30 работников
            #region Задание 2.
            Console.Out.WriteLine("Задание номер 2");
            TestEmployeeRepository(40, repo =>
            {
                var rnd = new Random();

                while (repo.Workers.Count > 30)
                {
                    var name = repo.Workers[rnd.Next(repo.Workers.Count)].FirstName;

                    repo.DeleteWorkerByName(name);
                }
            });
            #endregion


            // Уровень сложности: сложно
            // ** Задание 3. Создать отдел из 50 сотрудников и реализовать увольнение работников
            //               чья зарплата превышает 30000руб
            #region Задание 2.
            Console.Out.WriteLine("Задание номер 3");
            TestEmployeeRepository(50, repo => repo.DeleteWorkerBySalary(30000));
            #endregion

            #endregion

        }

        private static void TestEmployeeRepository(int initialNumOfWorkers, params Action<Repository>[] handlers)
        {
            //Бросаем исключение в случае некорректных данных на входе тестирующей функции
            if (initialNumOfWorkers < 0 || handlers == null) throw new ArgumentException("Not valid arguments for repository test func");

            //Инициализация репозитория заданным числом сотрудников
            var repository = new Repository(initialNumOfWorkers);

            // Вывод в std out stream полученного репозитория
            repository.Print("База данных до преобразования");

            for (var i = 0; i < handlers.Length; ++i)
            {
                handlers[i]?.Invoke(repository);

                // Печать в std out stream сотрудников после преобразования
                repository.Print($"База данных после {i + 1} преобразования");
            }
        }
    }
}
