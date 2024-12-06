using System;

namespace Factory;

public class IcecreamMaker
{
  public event Action<string>? OnMilkGenerated;

  private int _id { get; }
  public IcecreamMaker (int id) {
    _id = id;
  } 
  public void ProcessEvent(string eventData)
  {
      Console.WriteLine($"Холодильник {_id}: Обработка {eventData}");
      Thread.Sleep(150);

      string resultData = $"Холодильник {_id}: Результат для {eventData}";
      OnMilkGenerated?.Invoke(resultData);
  }
}
