namespace VectorTest;
using LinearAlgebra;
public class VectorMethodsTests
{
    [Theory]
    [InlineData(new double[] { 0 }, 1)]
    [InlineData(new double[] { 0, 0 }, 2)]
    [InlineData(new double[] { 0, 0, 0 }, 3)]
    public void DimensionsTest(double[] data, int result)
    {
        IMathVector vector = new MathVector(data);
        Assert.Equal(result, vector.Dimensions);
    }

    [Theory]
    [InlineData(new double[] { 1 }, 0 , 1)]
    [InlineData(new double[] { 1, 2, 2.5 }, 2 , 2.5)]
    [InlineData(new double[] { 3, 5, 0 }, 1 , 5)]
    public void GetElementTest(double[] data, int index, double result)
    {
        IMathVector vector = new MathVector(data);
        Assert.Equal(result, vector[index]);
    }

    [Theory]
    [InlineData(new double[] { 1 }, 1)]
    [InlineData(new double[] { 3, 4 }, 5)]
    [InlineData(new double[] { -5 }, 5)]
    public void VectorLengthTest(double[] data, double result)
    {
        IMathVector vector = new MathVector(data);
        Assert.Equal(result, vector.Length);
    }

    [Fact]
    public void VectorSumTest()
    {
        IMathVector vector1 = new MathVector(new double[] {1,2,3});
        IMathVector vector2 = new MathVector(new double[] {1,1,1});
        IMathVector result = new MathVector(new double[] {2,3,4});
        Assert.Equal(result, vector1.Sum(vector2));
    }

    [Fact]
    public void VectorSumNumberTest()
    {
        IMathVector vector1 = new MathVector(new double[] {1,2,3});
        IMathVector result = new MathVector(new double[] {2,3,4});
        Assert.Equal(result, vector1.SumNumber(1));
    }

    [Fact]
    public void VectorMyltiplyTest()
    {
        IMathVector vector1 = new MathVector(new double[] {1,2,3});
        IMathVector vector2 = new MathVector(new double[] {2,2,2});
        IMathVector result = new MathVector(new double[] {2,4,6});
        Assert.Equal(result, vector1.Multiply(vector2));
    }

    [Fact]
    public void VectorMyltiplyNumberTest()
    {
        IMathVector vector1 = new MathVector(new double[] {1,2,3});
        IMathVector result = new MathVector(new double[] {2,4,6});
        Assert.Equal(result, vector1.MultiplyNumber(2));
    }

    [Fact]
    public void VectorDivisionTest()
    {
        IMathVector vector1 = new MathVector(new double[] {2,4,8});
        IMathVector vector2 = new MathVector(new double[] {2,2,2});
        IMathVector result = new MathVector(new double[] {1,2,4});
        Assert.Equal(result, vector1.Divide(vector2));
    }

    [Fact]
    public void VectorDivisionNumberTest()
    {
        IMathVector vector1 = new MathVector(new double[] {2,4,8});
        double num = 2;
        IMathVector result = new MathVector(new double[] {1,2,4});
        Assert.Equal(result, vector1.DivideNumber(num));
    }

    [Fact]
    public void ScalarMyltiplyTest()
    {
        IMathVector vector1 = new MathVector(new double[] {3,4});
        IMathVector vector2 = new MathVector(new double[] {6,8});
        double result = 50;
        Assert.Equal(result, vector1.ScalarMultiply(vector2));
    }

    [Fact]
    public void CalcDistanceTest()
    {
        IMathVector vector1 = new MathVector(new double[] {3,4});
        IMathVector vector2 = new MathVector(new double[] {6,8});
        double result = 5;
        Assert.Equal(result, vector1.CalcDistance(vector2));
    }
}

public class VectorInterfaceOperarorsOverrideTests
{
    [Fact]
    public void VectorsSumOperatorOverrideTest()
    {
        IMathVector vector1 = new MathVector(new double[] {1,2,3});
        IMathVector vector2 = new MathVector(new double[] {1,1,1});
        IMathVector result = new MathVector(new double[] {2,3,4});
        Assert.Equal(result, vector1 + vector2);
    }

    [Fact]
    public void VectorSumNumberOperatorOverrideTest()
    {
        IMathVector vector1 = new MathVector(new double[] {1,2,3});
        double num = 1;
        IMathVector result = new MathVector(new double[] {2,3,4});
        Assert.Equal(result, vector1 + num);
    }

    [Fact]
    public void VectorsSubstartOperatorOverrideTest()
    {
        IMathVector vector1 = new MathVector(new double[] {1,2,3});
        IMathVector vector2 = new MathVector(new double[] {1,1,1});
        IMathVector result = new MathVector(new double[] {0,1,2});
        Assert.Equal(result, vector1 - vector2);
    }

    [Fact]
    public void VectorSubstartNumberOperatorOverrideTest()
    {
        IMathVector vector1 = new MathVector(new double[] {1,2,3});
        double num = 1;
        IMathVector result = new MathVector(new double[] {0,1,2});
        Assert.Equal(result, vector1 - num);
    }

    [Fact]
    public void VectorsMultiplyOperatorOverrideTest()
    {
        IMathVector vector1 = new MathVector(new double[] {1,2,3});
        IMathVector vector2 = new MathVector(new double[] {2,2,2});
        IMathVector result = new MathVector(new double[] {2,4,6});
        Assert.Equal(result, vector1 * vector2);
    }

    [Fact]
    public void VectorMultiplyNumberOperatorOverrideTest()
    {
        IMathVector vector1 = new MathVector(new double[] {1,2,3});
        double num = 2;
        IMathVector result = new MathVector(new double[] {2,4,6});
        Assert.Equal(result, vector1 * num);
    }

    [Fact]
    public void VectorsDivisionOperatorOverrideTest()
    {
        IMathVector vector1 = new MathVector(new double[] {1, 2, 3});
        IMathVector vector2 = new MathVector(new double[] {2, 2, 2});
        IMathVector result = new MathVector(new double[] {0.5, 1, 1.5});
        Assert.Equal(result, vector1 / vector2);
    }

    [Fact]
    public void VectorDivisionNumberOperatorOverrideTest()
    {
        IMathVector vector1 = new MathVector(new double[] { 1, 2, 3 });
        double num = 2;
        IMathVector result = new MathVector(new double[] { 0.5, 1, 1.5 });
        Assert.Equal(result, vector1 / num);
    }

    [Fact]
    public void VectorsScalarMultiplyOperatorOverrideTest()
    {
        IMathVector vector1 = new MathVector(new double[] { 3, 4 });
        IMathVector vector2 = new MathVector(new double[] { 6, 8 });
        double result = 50;
        Assert.Equal(result, vector1 % vector2);
    }
}

public class VectorClassOperarorsOverrideTests
{
    [Fact]
    public void VectorsSumOperatorOverrideTest()
    {
        MathVector vector1 = new MathVector(new double[] {1,2,3});
        MathVector vector2 = new MathVector(new double[] {1,1,1});
        MathVector result = new MathVector(new double[] {2,3,4});
        Assert.Equal(result, vector1 + vector2);
    }

    [Fact]
    public void VectorSumNumberOperatorOverrideTest()
    {
        MathVector vector1 = new MathVector(new double[] {1,2,3});
        double num = 1;
        MathVector result = new MathVector(new double[] {2,3,4});
        Assert.Equal(result, vector1 + num);
    }

    [Fact]
    public void VectorsSubstartOperatorOverrideTest()
    {
        MathVector vector1 = new MathVector(new double[] {1,2,3});
        MathVector vector2 = new MathVector(new double[] {1,1,1});
        MathVector result = new MathVector(new double[] {0,1,2});
        Assert.Equal(result, vector1 - vector2);
    }

    [Fact]
    public void VectorSubstartNumberOperatorOverrideTest()
    {
        MathVector vector1 = new MathVector(new double[] {1,2,3});
        double num = 1;
        MathVector result = new MathVector(new double[] {0,1,2});
        Assert.Equal(result, vector1 - num);
    }

    [Fact]
    public void VectorsMultiplyOperatorOverrideTest()
    {
        MathVector vector1 = new MathVector(new double[] {1,2,3});
        MathVector vector2 = new MathVector(new double[] {2,2,2});
        MathVector result = new MathVector(new double[] {2,4,6});
        Assert.Equal(result, vector1 * vector2);
    }

    [Fact]
    public void VectorMultiplyNumberOperatorOverrideTest()
    {
        MathVector vector1 = new MathVector(new double[] {1,2,3});
        double num = 2;
        MathVector result = new MathVector(new double[] {2,4,6});
        Assert.Equal(result, vector1 * num);
    }

    [Fact]
    public void VectorsDivisionOperatorOverrideTest()
    {
        MathVector vector1 = new MathVector(new double[] {1, 2, 3});
        MathVector vector2 = new MathVector(new double[] {2, 2, 2});
        MathVector result = new MathVector(new double[] {0.5, 1, 1.5});
        Assert.Equal(result, vector1 / vector2);
    }

    [Fact]
    public void VectorDivisionNumberOperatorOverrideTest()
    {
        MathVector vector1 = new MathVector(new double[] { 1, 2, 3 });
        double num = 2;
        MathVector result = new MathVector(new double[] { 0.5, 1, 1.5 });
        Assert.Equal(result, vector1 / num);
    }

    [Fact]
    public void VectorsScalarMultiplyOperatorOverrideTest()
    {
        MathVector vector1 = new MathVector(new double[] { 3, 4 });
        MathVector vector2 = new MathVector(new double[] { 6, 8 });
        double result = 50;
        Assert.Equal(result, vector1 % vector2);
    }
}

public class VectorErrorTests
{
    [Theory]
    [InlineData(new double[] {}, "Координаты не могут быть пустыми")]
    [InlineData(null, "Координаты не могут быть пустыми")]
    public void CreationErrorTest (double[] data, string message)
    {
        var error = Assert.Throws<ArgumentException>(() => new MathVector(data));
        Assert.Equal(message, error.Message);
    }

    [Theory]
    [InlineData(new double[] {1,2,3}, 3, "Индекс вне диапазона")]
    [InlineData(new double[] {1,2,3}, -1, "Индекс вне диапазона")]
    public void IndexOutOfRangeErrorTest (double[] data, int index, string message)
    {
        IMathVector vector = new MathVector(data);

        var error = Assert.Throws<IndexOutOfRangeException>(() => vector[index]);
        Assert.Equal(message, error.Message);
    }

    [Fact]
    public void ImmutabilityErrorTest ()
    {
        IMathVector vector = new MathVector(new double[] {1,2,3});
        string message = "Вектор иммутабелен";
        var error = Assert.Throws<InvalidOperationException>(() => vector[1] = 1);
        Assert.Equal(message, error.Message);
    }

    [Theory]
    [InlineData(new double[] {1,2,3}, new double[] {1,2}, "+")]
    [InlineData(new double[] {1,2,3}, new double[] {1,2},"-")]
    [InlineData(new double[] {1,2,3}, new double[] {1,2}, "*")]
    [InlineData(new double[] {1,2,3}, new double[] {1,2},"/")]
    [InlineData(new double[] {1,2,3}, new double[] {1,2},"%")]
    public void DifferentDimensionsOnOperationsErrorTest (double[] data1, double[] data2, string operation)
    {
        IMathVector vector1 = new MathVector(data1);
        IMathVector vector2 = new MathVector(data2);
        string message = "Количество измерений у векторов должны совпадать";
        var error = new ArgumentException();
        switch (operation) 
        {
            case "+":
                error = Assert.Throws<ArgumentException>(() => vector1 + vector2);
                break;
            case "-":
                error = Assert.Throws<ArgumentException>(() => vector1 - vector2);
                break;
            case "*":
                error = Assert.Throws<ArgumentException>(() => vector1 * vector2);
                break;
            case "/":
                error = Assert.Throws<ArgumentException>(() => vector1 + vector2);
                break;
            case "%":
                error = Assert.Throws<ArgumentException>(() => vector1 + vector2);
                break;
        }
        
        Assert.Equal(message, error.Message);
    }

    [Fact]
    public void DivisionByZeroVectorErrorTest ()
    {
        IMathVector vector1 = new MathVector(new double[] {1,2,3});
        IMathVector vector2 = new MathVector(new double[] {0,2,3});
        string message = "Деление на 0 невозможно!";
        var error = Assert.Throws<ArgumentException>(() => vector1 / vector2);
        Assert.Equal(message, error.Message);
    }

    [Fact]
    public void DivisionByZeroErrorTest ()
    {
        IMathVector vector = new MathVector(new double[] {1,2,3});
        int num = 0;
        string message = "Деление на 0 невозможно!";
        var error = Assert.Throws<ArgumentException>(() => vector / num);
        Assert.Equal(message, error.Message);
    }
}