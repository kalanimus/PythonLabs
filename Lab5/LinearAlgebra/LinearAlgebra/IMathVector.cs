using System;
using System.Collections;

namespace LinearAlgebra;

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

        IMathVector Divide(IMathVector vector);
        IMathVector DivideNumber(double num);

        public static IMathVector operator +(IMathVector vector_1, IMathVector vector_2) => vector_1.Sum(vector_2);
        public static IMathVector operator +(IMathVector vector_1, double num) => vector_1.SumNumber(num);
        public static IMathVector operator -(IMathVector vector_1, IMathVector vector_2) => vector_1.Sum(vector_2.MultiplyNumber(-1));
        public static IMathVector operator -(IMathVector vector_1, double num) => vector_1.SumNumber(-num);
        public static IMathVector operator *(IMathVector vector_1, IMathVector vector_2) => vector_1.Multiply(vector_2);
        public static IMathVector operator *(IMathVector vector_1, double num) => vector_1.MultiplyNumber(num);
        public static IMathVector operator /(IMathVector vector_1, IMathVector vector_2) => vector_1.Divide(vector_2);
        public static IMathVector operator /(IMathVector vector_1, double num) => vector_1.DivideNumber(num);
        public static double operator %(IMathVector vector_1, IMathVector vector_2) => vector_1.ScalarMultiply(vector_2);
    }
