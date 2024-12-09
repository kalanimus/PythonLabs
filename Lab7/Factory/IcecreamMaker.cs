using System;

namespace Factory;

public class IcecreamMaker
{
  public event Action<IceCream>? IceCreamGenerated;
  private int[] _random_time_interval;
  private int _icecream_making_time;
  private int _icecream_per_milk;
  private int _id { get; }
  public IcecreamMaker (int id, int icecream_making_time, int icecream_per_milk, int[] random_time_interval) {
    _id = id;
    _icecream_making_time = icecream_making_time;
    _random_time_interval = random_time_interval;
    _icecream_per_milk = icecream_per_milk;
  } 
  public IceCream ProcessEvent(string eventData)
  {
    string message = $"Холодильник {_id}: Обработка {eventData}";
    FileOperations.FileOperations.WriteToLogFile(message);
    Console.WriteLine(message);
    Thread.Sleep(RandomNumber.GetRandomNumberFromRange(_random_time_interval[0], _random_time_interval[1]) + _icecream_making_time);

    string resultMessage = $"Холодильник {_id}: Результат для {eventData}";
    FileOperations.FileOperations.WriteToLogFile(resultMessage);
    Console.WriteLine(resultMessage);

    for(int i = 0; i < _icecream_per_milk; i++){
      IceCreamGenerated?.Invoke(new IceCream());
    }
      
    return new IceCream();
  }
}
