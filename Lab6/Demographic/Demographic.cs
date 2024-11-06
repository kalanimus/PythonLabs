namespace Demographic;

public interface IEngine
{

}

public class Engine : IEngine
{
  // YearTick
}

public class Person
{
  // ChildBirth
}

public static class ProbabilityCalculator
{
  private static readonly Random _random = new Random();
  public static bool IsEventHappened(double eventProbability)
  {
    return _random.NextDouble() <= eventProbability;
  }
}