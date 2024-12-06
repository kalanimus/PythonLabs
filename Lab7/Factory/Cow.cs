using System;

namespace Factory;

public class Cow
{
  public event Action<string>? MilkProduced;
  
  private int _id { get;}
  public Cow (int id) {
    _id = id;
  }
  public void ProduceMilk()
  {
      for (int i = 0; i < 5; i++)
      {
          var eventData = $"Молоко {i + 1}";
          Console.WriteLine($"Корова {_id}: Генерация {eventData}");
          MilkProduced?.Invoke(eventData);
          Thread.Sleep(50);
      }
  }
}
