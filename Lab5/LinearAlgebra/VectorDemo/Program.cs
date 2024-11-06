using LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


try
{
    IMathVector vector1 = new MathVector(new double[] { 1, 2, 3 });
    IMathVector vector2 = new MathVector(new double[] { 3, 2, 1});
    double num = 1;
    vector1 = vector1 + num;
    vector1[1] = 2;
    PrintVector(vector1);
}
catch (Exception ex)
{
    Console.WriteLine($"ОШИБКА! {ex.Message}");
}

void PrintVector(IMathVector vector)
{
    for (int i = 0; i < vector.Dimensions; i++)
    {
        Console.Write($"{vector[i]} ");
    }
    Console.WriteLine();
}