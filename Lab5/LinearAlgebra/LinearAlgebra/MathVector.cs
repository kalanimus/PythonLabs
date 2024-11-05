using System;
using System.Collections;

namespace LinearAlgebra
{
    public interface IMathVector : IEnumerable
    {
        int Dimensions { get; }
        double this[int i] { get; set; }
        double Length { get; }
        IMathVector SumNumber(double number);
        IMathVector MultiplyNumber(double number);
        IMathVector Sum(IMathVector vector);
        IMathVector Multiply(IMathVector vector);
        double ScalarMultiply(IMathVector vector);
        double CalcDistance(IMathVector vector);
    }


    public class MathVector : IMathVector
    {
        private readonly double[] _coordinates;

        public MathVector(double[] coordinates)
        {
            if (coordinates == null || coordinates.Length == 0)
                throw new ArgumentException("Координаты не могут быть пустыми");

            _coordinates = new double[coordinates.Length];
            Array.Copy(coordinates, _coordinates, coordinates.Length);
        }

        public int Dimensions => _coordinates.Length;

        public double this[int i]
        {
            get
            {
                if (i < 0 || i >= Dimensions)
                    throw new IndexOutOfRangeException("Индекс вне диапазона");

                return _coordinates[i];
            }
            set
            {
                throw new InvalidOperationException("Вектор иммутабелен");
            }
        }

        public double Length
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < Dimensions; i++)
                {
                    sum += _coordinates[i] * _coordinates[i];
                }
                return Math.Sqrt(sum);
            }
        }

        public IMathVector SumNumber(double number)
        {
            double[] result = new double[Dimensions];
            for (int i = 0; i < Dimensions; i++)
            {
                result[i] = _coordinates[i] + number;
            }
            return new MathVector(result);
        }

        public IMathVector MultiplyNumber(double number)
        {
            double[] result = new double[Dimensions];
            for (int i = 0; i < Dimensions; i++)
            {
                result[i] = _coordinates[i] * number;
            }
            return new MathVector(result);
        }

        public IMathVector Sum(IMathVector vector)
        {
            if (vector == null || vector.Dimensions != Dimensions)
                throw new ArgumentException("Количество измерений у векторов должны совпадать");

            double[] result = new double[Dimensions];
            for (int i = 0; i < Dimensions; i++)
            {
                result[i] = _coordinates[i] + vector[i];
            }
            return new MathVector(result);
        }

        public IMathVector Multiply(IMathVector vector)
        {
            if (vector == null || vector.Dimensions != Dimensions)
                throw new ArgumentException("Количество измерений у векторов должны совпадать");

            double[] result = new double[Dimensions];
            for (int i = 0; i < Dimensions; i++)
            {
                result[i] = _coordinates[i] * vector[i];
            }
            return new MathVector(result);
        }

        public double ScalarMultiply(IMathVector vector)
        {
            if (vector == null || vector.Dimensions != Dimensions)
                throw new ArgumentException("Количество измерений у векторов должны совпадать");

            double result = 0;
            for (int i = 0; i < Dimensions; i++)
            {
                result += _coordinates[i] * vector[i];
            }
            return result;
        }

        public double CalcDistance(IMathVector vector)
        {
            if (vector == null || vector.Dimensions != Dimensions)
                throw new ArgumentException("Количество измерений у векторов должны совпадать");

            double sum = 0;
            for (int i = 0; i < Dimensions; i++)
            {
                double diff = _coordinates[i] - vector[i];
                sum += diff * diff;
            }
            return Math.Sqrt(sum);
        }

        public static IMathVector operator +(MathVector vector_1, MathVector vector_2)
        {
            return vector_1.Sum(vector_2);
        }

        public static IMathVector operator +(MathVector vector_1, double num)
        {
            return vector_1.SumNumber(num);
        }

        public static IMathVector operator -(MathVector vector_1, MathVector vector_2)
        {
            return vector_1.Sum(vector_2.MultiplyNumber(-1));
        }

        public static IMathVector operator -(MathVector vector_1, double num)
        {
            return vector_1.SumNumber(-num);
        }

        public static IMathVector operator *(MathVector vector_1, MathVector vector_2)
        {
            return vector_1.Multiply(vector_2);
        }

        public static IMathVector operator *(MathVector vector_1, double num)
        {
            return vector_1.MultiplyNumber(num);
        }

        public static IMathVector operator /(MathVector vector_1, MathVector vector_2)
        {
            if (vector_1.Dimensions != vector_2.Dimensions)
                throw new ArgumentException("Количество измерений у векторов должны совпадать");

            double[] result = new double[vector_1.Dimensions];
            for (int i = 0; i < vector_1.Dimensions; i++)
            {
                if (vector_2[i] == 0)
                    throw new DivideByZeroException("Деление на 0 невозможно!");

                result[i] = vector_1[i] / vector_2[i];
            }
            return new MathVector(result);
        }

        public static IMathVector operator /(MathVector vector_1, double num)
        {
            if (num == 0)
            {
                throw new DivideByZeroException("Деление на 0 невозможно!");
            }
            return vector_1.MultiplyNumber(1 / num);
        }

        public static double operator %(MathVector vector_1, MathVector vector_2)
        {
            return vector_1.ScalarMultiply(vector_2);
        }
        public IEnumerator GetEnumerator()
        {
            return _coordinates.GetEnumerator();
        }
    }
}
