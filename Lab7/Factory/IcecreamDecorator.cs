using System;

namespace Factory;

public class IcecreamDecorator
{
  private int _id { get; }
  private int _icecream_decoration_time;
  private int[] _random_time_interval;
  public IcecreamDecorator (int id, int icecream_decoration_time, int[] random_time_interval) {
    _id = id;
    _icecream_decoration_time = icecream_decoration_time;
    _random_time_interval = random_time_interval;
  } 
  public IceCream ProcessResult(IceCream iceCream)
  {
    string message = $"Машина для придания вкуса {_id}: Обработка мороженного со вкусом: {iceCream.taste.ToString()}";
    FileOperations.FileOperations.WriteToLogFile(message);
    Console.WriteLine(message);
    Thread.Sleep(RandomNumber.GetRandomNumberFromRange(_random_time_interval[0], _random_time_interval[1]) + _icecream_decoration_time);
    iceCream.RandomizeTaste();

    string result_message = $"Машина для придания вкуса {_id}: Завершила обработку. Теперь у мороженного вкус: {iceCream.taste.ToString()}";
    FileOperations.FileOperations.WriteToLogFile(result_message);
    Console.WriteLine(result_message);
    return iceCream;
}
}
