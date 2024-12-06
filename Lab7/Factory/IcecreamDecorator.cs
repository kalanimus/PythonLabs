using System;

namespace Factory;

public class IcecreamDecorator
{
  private int _id { get; }
  public IcecreamDecorator (int id) {
    _id = id;
  } 
  public void ProcessResult(string resultData)
  {
      Console.WriteLine($"Машина для придания вкуса {_id}: Обработка {resultData}");
      Thread.Sleep(100);
  }
}
