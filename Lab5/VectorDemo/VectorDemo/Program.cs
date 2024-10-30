using LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {           
            MathVector vector1 = new MathVector(new double[] { 3, 4 });
            MathVector vector2 = new MathVector(new double[] { 6, 8 });

            Console.WriteLine($"Колличество элементов вектора 1:\n{vector1.Dimensions}");
            Console.WriteLine($"Колличество элементов вектора 2:\n{vector2.Dimensions}");

            Console.WriteLine($"Первый элемент вектора 1:\n{vector1[0]}");
            Console.WriteLine($"Второй элемент вектора 2:\n{vector2[1]}");

            Console.WriteLine($"Длина вектора 1:\n{vector1.Length}");
            Console.WriteLine($"Длина вектора 2:\n{vector2.Length}");

            double num = 2;
            IMathVector sum_with_double = vector1.SumNumber(num);
            Console.WriteLine("Вектор 1 + 2:");
            PrintVector(sum_with_double);

            IMathVector mult_with_double = vector1.MultiplyNumber(num);
            Console.WriteLine("Вектор 1 * 2:");
            PrintVector(mult_with_double);

            IMathVector sum_with_vector = vector1.Sum(vector2);
            Console.WriteLine("Вектор 1 + вектор 2:");
            PrintVector(sum_with_vector);

            IMathVector mult_with_vector = vector1.Multiply(vector2);
            Console.WriteLine("Вектор 1 * вектор 2:");
            PrintVector(mult_with_vector);

            double scalat_mult = vector1.ScalarMultiply(vector2);
            Console.WriteLine($"Скалярное произведение вектора 1 и вектора 2:\n{scalat_mult}");

            double distance = vector1.CalcDistance(vector2);
            Console.WriteLine($"Евклидово расстояние между вектором 1 и вектором 2:\n{scalat_mult}");

            Console.WriteLine("\nДалее тестирование перегрузки операторов\n");

            IMathVector sum = vector1 + vector2;
            Console.WriteLine("Вектор 1 + вектор 2:");
            PrintVector(sum);

            IMathVector difference = vector1 - vector2;
            Console.WriteLine("Вектор 1 - вектор 2:");
            PrintVector(difference);

            IMathVector product = vector1 * vector2;
            Console.WriteLine("Вектор 1 * вектор 2:");
            PrintVector(product);

            double scalar_product = vector1 % vector2;
            Console.WriteLine($"Скалярное произведение векторов (вектор 1 % вектор 2):\n{scalar_product}");

            IMathVector division = vector1 / vector2;
            Console.WriteLine("Вектор 1 / вектор 2:");
            PrintVector(division);

            IMathVector multipliedByNumber = vector1 * num;
            Console.WriteLine($"Вектор 1 * {num}:");
            PrintVector(multipliedByNumber);

            IMathVector dividedByNumber = vector1 / num;
            Console.WriteLine($"Вектор 1 / {num}:");
            PrintVector(dividedByNumber);

            IMathVector sumWithNumber = vector1 + num;
            Console.WriteLine($"Вектор 1 + {num}:");
            PrintVector(sumWithNumber);

            IMathVector differenceWithNumber = vector1 - num;
            Console.WriteLine($"Вектор 1 - {num}:");
            PrintVector(differenceWithNumber);

            Console.WriteLine("\nДалее тестирование ошибок\n");

            try
            {
                IMathVector vector3 = new MathVector(new double[] { });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            try
            {
                IMathVector vector3 = new MathVector(new double[] { 1, 2 });
                double dimention = vector3[5];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            try
            {
                IMathVector vector3 = new MathVector(new double[] { 1, 2 });
                vector3[5] = 10;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            try
            {
                IMathVector vector3 = new MathVector(new double[] { 1, 2 });
                IMathVector vector4 = new MathVector(new double[] { 1, 2, 3 });
                vector3.Sum(vector4);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            try
            {
                IMathVector vector3 = new MathVector(new double[] { 1, 2 });
                IMathVector vector4 = new MathVector(new double[] { 1, 2, 3 });
                IMathVector result_vector = (MathVector)vector3 + (MathVector)vector4;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            try
            {
                IMathVector vector3 = new MathVector(new double[] { 1, 2 });
                IMathVector vector4 = new MathVector(new double[] { 1, 2, 3 });
                vector3.Multiply(vector4);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            try
            {
                IMathVector vector3 = new MathVector(new double[] { 1, 2 });
                IMathVector vector4 = new MathVector(new double[] { 1, 2, 3 });
                IMathVector result_vector = (MathVector)vector3 * (MathVector)vector4;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            try
            {
                IMathVector vector3 = new MathVector(new double[] { 1, 2 });
                IMathVector vector4 = new MathVector(new double[] { 1, 2, 3 });
                vector3.ScalarMultiply(vector4);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            try
            {
                IMathVector vector3 = new MathVector(new double[] { 1, 2 });
                IMathVector vector4 = new MathVector(new double[] { 1, 2, 3 });
                double result = (MathVector)vector3 % (MathVector)vector4;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            try
            {
                IMathVector vector3 = new MathVector(new double[] { 1, 2 });
                IMathVector vector4 = new MathVector(new double[] { 1, 2, 3 });
                vector3.CalcDistance(vector4);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            try
            {
                IMathVector vector3 = new MathVector(new double[] { 1, 2 });
                IMathVector vector4 = new MathVector(new double[] { 1, 2, 3 });
                IMathVector result_vector = (MathVector)vector3 / (MathVector)vector4;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            try
            {
                IMathVector vector3 = new MathVector(new double[] { 1, 2 });
                IMathVector vector4 = new MathVector(new double[] { 1, 0 });
                IMathVector result_vector = (MathVector)vector3 / (MathVector)vector4;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            try
            {
                IMathVector vector3 = new MathVector(new double[] { 1, 2 });
                IMathVector result_vector = (MathVector)vector3 / 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА! {ex.Message}");
            }

            Console.ReadKey();

            void PrintVector(IMathVector vector)
            {
                for (int i = 0; i < vector.Dimensions; i++)
                {
                    Console.Write($"{vector[i]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
