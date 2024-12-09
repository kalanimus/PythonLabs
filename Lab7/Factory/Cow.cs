using System;

namespace Factory;

public class Cow
{
  public event Action<string>? MilkProduced;
  
  private int _id { get;}
  private int _milk_amount { get; }
  private int[] _random_interval { get; }
  private int _milk_generation_time { get; }

  public Cow (int id, int milk_amount, int[] random_interval, int milk_generation_time) {
    _id = id;
    _milk_amount = milk_amount;
    _random_interval = random_interval;
    _milk_generation_time = milk_generation_time;
  }
  public void ProduceMilk()
  {
      for (int i = 0; i < _milk_amount; i++)
      {
          var eventData = $"Молоко {i + 1}";
          string message = $"Корова {_id}: Генерация {eventData}";
          FileOperations.FileOperations.WriteToLogFile(message);
          Console.WriteLine(message);
          MilkProduced?.Invoke(eventData);
          Thread.Sleep(RandomNumber.GetRandomNumberFromRange(_random_interval[0], _random_interval[1]) + _milk_generation_time);
      }
  }
}
