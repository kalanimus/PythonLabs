using System.Collections;
namespace LinearAlgebra;

public interface IMathVector : IEnumerable
{
  /// <summary>
  /// Получить размерность вектора (количество координат).
  /// </summary>
  int Dimensions { get; }
  /// <summary>
  /// Индексатор для доступа к элементам вектора. Нумерация снуля.
  /// </summary>
  double this[int i] { get; set; }
  /// <summary>Рассчитать длину (модуль) вектора.</summary>
  double Length { get; }
  /// <summary>Покомпонентное сложение с числом.</summary>
  IMathVector SumNumber(double number);
  /// <summary>Покомпонентное умножение на число.</summary>
  IMathVector MultiplyNumber(double number);
  /// <summary>Сложение с другим вектором.</summary>
  IMathVector Sum(IMathVector vector);
  /// <summary>Покомпонентное умножение с другимвектором.</summary>
  IMathVector Multiply(IMathVector vector);
  /// <summary>Скалярное умножение на другой вектор.</summary>
  double ScalarMultiply(IMathVector vector);
  /// <summary>
  /// Вычислить Евклидово расстояние до другого вектора.
  /// </summary>
  double CalcDistance(IMathVector vector);
}


public class MathVector : IMathVector
{
  private readonly double[] _coordinates;

  public MathVector(double[] coordinates)
  {
    if (coordinates == null || coordinates.Length == 0)
        throw new ArgumentException("Координаты не муогут быть путсыми");

    _coordinates = new double[coordinates.Length];
    Array.Copy(coordinates, _coordinates, coordinates.Length);
  }

  public int Dimensions => _coordinates.Length;

  public double this[int i]
  {
    get
    {
        if (i < 0 || i >= Dimensions)
            throw new IndexOutOfRangeException("Index is out of range.");
        return _coordinates[i];
    }
    set
    {
        throw new InvalidOperationException("This vector is immutable.");
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
    var result = new double[Dimensions];
    for (int i = 0; i < Dimensions; i++)
    {
        result[i] = _coordinates[i] + number;
    }
    return new MathVector(result);
  }

  public IMathVector MultiplyNumber(double number)
  {
    var result = new double[Dimensions];
    for (int i = 0; i < Dimensions; i++)
    {
        result[i] = _coordinates[i] * number;
    }
    return new MathVector(result);
  }

  public double CalcDistance(IMathVector vector)
  {
      throw new NotImplementedException();
  }

  public IEnumerator GetEnumerator()
  {
      throw new NotImplementedException();
  }

  public IMathVector Multiply(IMathVector vector)
  {
      throw new NotImplementedException();
  }

  public double ScalarMultiply(IMathVector vector)
  {
      throw new NotImplementedException();
  }

  public IMathVector Sum(IMathVector vector)
  {
      throw new NotImplementedException();
  }
}
