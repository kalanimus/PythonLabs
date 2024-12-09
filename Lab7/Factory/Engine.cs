namespace Factory;
using System.Collections.Concurrent;
using System.Data;

public class Engine
{
  private readonly QueueManager _queueManager = new();
  private readonly Config _config;
  private readonly ConcurrentBag<IceCream> _iceCreams;
  public Engine(Config config) {
    _config = config;
    _iceCreams = new ConcurrentBag<IceCream>();
  }

  public async Task Run()
  {
    var cows = new Cow[_config.cow_amount];
    var icecreamMakersArray = new IcecreamMaker[_config.icecream_maker_amount];
    var icecreamDecoratorsArray = new IcecreamDecorator[_config.icecream_decorator_amount];

    for (int i = 0; i < _config.cow_amount; i++)
    {
      cows[i] = new Cow(i + 1, _config.milk_amount_per_cow, _config.random_time_interval, _config.milk_generation_time);
      cows[i].MilkProduced += _queueManager.AddMilk;
    }

    for (int i = 0; i < _config.icecream_maker_amount; i++)
    {
      var handlerId = i;
      icecreamMakersArray[i] = new IcecreamMaker(i + 1, _config.icecream_making_time, _config.icecream_per_milk, _config.random_time_interval);
      icecreamMakersArray[i].IceCreamGenerated += _queueManager.AddIcecream;
    }

    for (int i = 0; i < _config.icecream_decorator_amount; i++)
    {
      icecreamDecoratorsArray[i] = new IcecreamDecorator(i + 1, _config.icecream_decoration_time, _config.random_time_interval);
    }

    var cowTasks = new Task[_config.cow_amount];
    for (int i = 0; i < _config.cow_amount; i++)
    {
      int cowId = i;
      cowTasks[i] = Task.Run(() => cows[cowId].ProduceMilk());
    }

    var icecreamMakerTasks = new Task[_config.icecream_maker_amount];
    for (int i = 0; i < _config.icecream_maker_amount; i++)
    {
      int icecreamMakerId = i;
      icecreamMakerTasks[i] = Task.Run(() =>
      {
        foreach (var eventData in _queueManager.GetMilkQueue().GetConsumingEnumerable())
        {
          icecreamMakersArray[icecreamMakerId].ProcessEvent(eventData);
        }
      });
    }

    var icecreamDecoratorsTasks = new Task[icecreamDecoratorsArray.Length];
    for (int i = 0; i < icecreamDecoratorsArray.Length; i++)
    {
      int icecreamDecoratorsId = i;
      icecreamDecoratorsTasks[i] = Task.Run(() =>
      {
        foreach (var resultData in _queueManager.GetIcecreamQueue().GetConsumingEnumerable())
        {
          _iceCreams.Add(icecreamDecoratorsArray[icecreamDecoratorsId].ProcessResult(resultData));
        }
      });
    }

    await Task.WhenAll(cowTasks);
    _queueManager.CompleteMilkQueue();

    await Task.WhenAll(icecreamMakerTasks);
    _queueManager.CompleteIcecreamQueue();

    await Task.WhenAll(icecreamDecoratorsTasks);
    Console.WriteLine("Обработка завершена.");

    var counted_ice_cream = IceCreamCounter.CountIceCreams(_iceCreams.ToList());
    FileOperations.FileOperations.MakeResultFile(counted_ice_cream);
  }
}
