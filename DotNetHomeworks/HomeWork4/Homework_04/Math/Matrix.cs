using System;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Homework_04.Math
{
    //TODO: оптимизировать true/false sharing, использование памяти(ArrayPool/Memory<T> etc.)
    //TODO: обработка OutOfMemoryException
    public class Matrix
    {
        private double[,] _backArray;

        /// <summary>
        /// Создает матрицу с заданными размерами
        /// </summary>
        /// <param name="shape">Размеры матрицы - число строк и столбцов в ней</param>
        /// <returns>Матрица с заданными размерами</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Matrix CreateMatrix(params int[] shape)
        {
            if (shape?.Length != 2)
                throw new ArgumentException("Matrices must be two - dimensional");
            if ((shape[0] < 1) || (shape[1] < 1))
                throw new ArgumentException("Number of dimension must be greater than zero");

            return new Matrix(shape[0], shape[1]);
        }

        /// <summary>
        /// Заполняет матрицу значениями в соответствии со значениями передаваемого массива,
        /// осуществляя копирование элементов
        /// </summary>
        /// <param name="foodArr">Массив значений</param>
        public void FeedMatrix(double[,] foodArr)
        {
            if (foodArr.GetLength(0) != _backArray.GetLength(0)
                || foodArr.GetLength(1) != _backArray.GetLength(1))
                throw new ArgumentException("The placeholder array must match the matrix by shape");

            _backArray = foodArr.Clone() as double[,];
        }

        public void FillTheMatrix()
        {
            var randomizer = new Random();
            for (var i = 0; i < _backArray.GetLength(0); ++i)
            {
                for (var j = 0; j < _backArray.GetLength(1); ++j)
                {
                    _backArray[i, j] = randomizer.Next(10, 100);
                }
            }
        }

        private Matrix(int rows, int cols)
        {
            _backArray = new double[rows, cols];
        }


        public double this[int row, int col]
        {
            get => _backArray[row, col];
            set => _backArray[row, col] = value;
        }

        public double[] this[int rowNum, bool isRow = true]
        {
            get
            {
                if (isRow) return _backArray.GetRow(rowNum);

                var column = new double[_backArray.GetLength(0)];
                for (var i = 0; i < column.Length; ++i)
                    column[i] = _backArray[i, rowNum];

                return column;
            }

            set
            {
                if (isRow)
                {
                    if (value == null || value.Length != _backArray.GetLength(1))
                        throw new ArgumentException("The vector-row passed to the indexer must be non-empty " +
                                                    "and have a length equal to the length of the matrix row");

                    //TODO: Use BlockCopy instead of iterations
                    _backArray.SetRow(rowNum, value);

                    return;
                }

                if (value == null || value.Length != _backArray.GetLength(0))
                    throw new ArgumentException("The vector-column passed to the indexer must be non-empty " +
                                                "and have a length equal to the length of the matrix column");
                //TODO: Use BlockCopy instead of iterations
                for (var i = 0; i < value.Length; ++i)
                    _backArray[i, rowNum] = value[i];
            }
        }

        public int[] Shape => new[] {_backArray.GetLength(0), _backArray.GetLength(1)};

        /// <summary>
        /// Данный метод осуществляет произведение матриц
        /// </summary>
        /// <param name="leftMatrix">Левая матрица</param>
        /// <param name="rightMatrix">Правая матрица</param>
        /// <returns>Результирующая матрица - произведение матриц</returns>
        /// <exception cref="ArgumentException">Выбрасывается в случае если матрицы не
        /// удовлетворяют правилу произведения матриц</exception>
        public static Matrix Dot(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.Shape[1] != rightMatrix.Shape[0])
                throw new ArgumentException("Shapes of matrices are not aligned");

            var resultMatrix = new Matrix(leftMatrix.Shape[0], rightMatrix.Shape[1]);

            rightMatrix = Transpose(rightMatrix);


            Parallel.For(0, leftMatrix.Shape[0], row =>
            {
                for (var col = 0; col < rightMatrix.Shape[0]; ++col)
                {
                    resultMatrix[row, col] = VecMul(leftMatrix[row], rightMatrix[col]);
                }
            });

            return resultMatrix;
        }

        public static Matrix Multiply(Matrix matrix, double multiplier)
        {
            var resultMatrix = new Matrix(matrix.Shape[0], matrix.Shape[1]);

            Parallel.For(0, matrix.Shape[0], row => { resultMatrix[row] = VecMul(matrix[row], multiplier); });

            return resultMatrix;
        }

        public static Matrix Subtract(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.Shape[0] != rightMatrix.Shape[0] || leftMatrix.Shape[1] != rightMatrix.Shape[1])
                throw new ArgumentException("the difference of matrices " +
                                            "must include matrices with the same size");

            var resultMatrix = new Matrix(leftMatrix.Shape[0], leftMatrix.Shape[1]);

            Parallel.For(0, leftMatrix.Shape[0],
                row => { resultMatrix[row] = VectorSubtract(leftMatrix[row], rightMatrix[row]); });

            return resultMatrix;
        }
        
        public static Matrix Add(Matrix leftMatrix, Matrix rightMatrix)
        {
            if (leftMatrix.Shape[0] != rightMatrix.Shape[0] || leftMatrix.Shape[1] != rightMatrix.Shape[1])
                throw new ArgumentException("the difference of matrices " +
                                            "must include matrices with the same size");

            var resultMatrix = new Matrix(leftMatrix.Shape[0], leftMatrix.Shape[1]);

            Parallel.For(0, leftMatrix.Shape[0],
                row => { resultMatrix[row] = VectorAdd(leftMatrix[row], rightMatrix[row]); });

            return resultMatrix;
        }

        //Данный метод осуществляет произведение двух векторов, применяя низкоуровневые оптимизации
        private static double VecMul(double[] vector, double[] vector1)
        {
            if (vector.Length != vector1.Length)
                throw new ArgumentException("Vectors must be the same size for multiplication");

            if (vector.Length < 4) return vector.Select((t, i) => t * vector1[i]).Sum();


            var vectorSize = Vector<double>.Count;
            int ptr;

            var tmpVector0 = new Vector<double>(vector);
            var tmpVector1 = new Vector<double>(vector1);
            var result = Vector.Dot(tmpVector0, tmpVector1);

            for (ptr = vectorSize; ptr < (vector.Length - vectorSize + 1); ptr += vectorSize)
            {
                tmpVector0 = new Vector<double>(vector, ptr);
                tmpVector1 = new Vector<double>(vector1, ptr);
                result += Vector.Dot(tmpVector0, tmpVector1);
            }

            for (; ptr < vector.Length; ptr++)
            {
                result += vector[ptr] * vector1[ptr];
            }

            return result;
        }

        private static double[] VecMul(double[] vector, double multiplier)
        {
            if (vector.Length < 4) return vector.Select((t) => t * multiplier).ToArray();

            var vectorSize = Vector<double>.Count;
            int ptr;


            for (ptr = 0; ptr < (vector.Length - vectorSize + 1); ptr += vectorSize)
            {
                var tmpVector = new Vector<double>(vector, ptr) * multiplier;

                tmpVector.CopyTo(vector, ptr);
            }

            return vector;
        }

        private static double[] VectorAdd(double[] leftVector, double[] rightVector)
        {
            if (leftVector.Length < 4) return leftVector.Select((t, i) => t + rightVector[i]).ToArray();
                
            var vectorSize = Vector<double>.Count;
            int ptr;
            for (ptr = 0; ptr < (leftVector.Length - vectorSize + 1); ptr += vectorSize)
            {
                var tmpVector0 = new Vector<double>(leftVector, ptr);
                var tmpVector1 = new Vector<double>(rightVector, ptr);

                (tmpVector0 + tmpVector1).CopyTo(leftVector, ptr);
            }

            for (; ptr < leftVector.Length; ptr++)
            {
                leftVector[ptr] = leftVector[ptr] + rightVector[ptr];
            }

            return leftVector;
        }
        
        private static double[] VectorSubtract(double[] leftVector, double[] rightVector)
        {
            if (leftVector.Length < 4) return leftVector.Select((t, i) => t - rightVector[i]).ToArray();

            var vectorSize = Vector<double>.Count;
            int ptr;

            for (ptr = 0; ptr < (leftVector.Length - vectorSize + 1); ptr += vectorSize)
            {
                var tmpVector0 = new Vector<double>(leftVector, ptr);
                var tmpVector1 = new Vector<double>(rightVector, ptr);

                (tmpVector0 - tmpVector1).CopyTo(leftVector, ptr);
            }

            for (; ptr < leftVector.Length; ptr++)
            {
                leftVector[ptr] = leftVector[ptr] - rightVector[ptr];
            }

            return leftVector;
        }

        /// <summary>
        /// Данный метод осуществляет транспонирование матрицы
        /// </summary>
        /// <param name="sourceMatrix">Транспонируемая матрица</param>
        /// <returns>Транспонированная матрица, находится в новой области памяти</returns>
        public static Matrix Transpose(Matrix sourceMatrix)
        {
            var shape = sourceMatrix.Shape;
            Array.Reverse(shape);
            var resultMatrix = CreateMatrix(shape);
            for (var row = 0; row < shape[0]; ++row)
            {
                for (var col = 0; col < shape[1]; ++col)
                {
                    resultMatrix[row, col] = sourceMatrix[col, row];
                }
            }

            return resultMatrix;
        }

        public override string ToString()
        {
            var ret = new StringBuilder();
            for (var i = 0; i < _backArray.GetLength(0); ++i)
            {
                for (var j = 0; j < _backArray.GetLength(1); ++j)
                {
                    ret.Append(_backArray[i, j]);
                    ret.Append("\t");
                }

                ret.Append("\n");
            }

            return ret.ToString();
        }
    }
}