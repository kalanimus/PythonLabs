﻿using System;
using System.Collections;
using System.Xml.XPath;

namespace LinearAlgebra
{
    


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

        public IMathVector Divide(IMathVector vector)
        {
            if (vector == null || vector.Dimensions != Dimensions)
                throw new ArgumentException("Количество измерений у векторов должны совпадать");
            for (int i = 0; i < Dimensions; i++){
                if (vector[i] == 0)
                    throw new ArgumentException("Деление на 0 невозможно!");
            }
            double[] result = new double[Dimensions];
            for (int i = 0; i < Dimensions; i++) {
                result[i] = _coordinates[i] / vector[i];
            }
            return new MathVector(result);
        }
        public IMathVector DivideNumber(double num)
        {
            if (num == 0)
                throw new ArgumentException("Деление на 0 невозможно!");
            double[] result = new double[Dimensions];
            for (int i = 0; i < Dimensions; i++) {
                result[i] = _coordinates[i] / num;
            }
            return new MathVector(result);
        }

        public static IMathVector operator +(MathVector vector_1, IMathVector vector_2) => vector_1.Sum(vector_2);
        
        public static IMathVector operator +(MathVector vector_1, double num) => vector_1.SumNumber(num);

        public static IMathVector operator -(MathVector vector_1, IMathVector vector_2) => vector_1.Sum(vector_2.MultiplyNumber(-1));

        public static IMathVector operator -(MathVector vector_1, double num) => vector_1.SumNumber(-num);

        public static IMathVector operator *(MathVector vector_1, IMathVector vector_2) => vector_1.Multiply(vector_2);
        
        public static IMathVector operator *(MathVector vector_1, double num) => vector_1.MultiplyNumber(num);

        public static IMathVector operator /(MathVector vector_1, IMathVector vector_2) => vector_1.Divide(vector_2);
    
        public static IMathVector operator /(MathVector vector_1, double num) => vector_1.DivideNumber(num);
        
        public static double operator %(MathVector vector_1, MathVector vector_2) => vector_1.ScalarMultiply(vector_2);
        public IEnumerator GetEnumerator() => _coordinates.GetEnumerator();
        
    }
}
