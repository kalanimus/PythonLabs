namespace Factory;

public class Engine
{
  private readonly QueueManager _queueManager = new();

  public async Task Run()
  {
    var cows = new Cow[3];
    var icecreamMakersArray = new IcecreamMaker[3];
    var icecreamDecoratorsArray = new IcecreamDecorator[2];

    for (int i = 0; i < cows.Length; i++)
    {
      cows[i] = new Cow(i + 1);
      cows[i].MilkProduced += _queueManager.AddMilk;
    }

    for (int i = 0; i < icecreamMakersArray.Length; i++)
    {
      var handlerId = i;
      icecreamMakersArray[i] = new IcecreamMaker(i + 1);
      icecreamMakersArray[i].OnMilkGenerated += _queueManager.AddIcecream;
    }

    for (int i = 0; i < icecreamDecoratorsArray.Length; i++)
    {
      icecreamDecoratorsArray[i] = new IcecreamDecorator(i + 1);
    }

    var cowTasks = new Task[cows.Length];
    for (int i = 0; i < cows.Length; i++)
    {
      int cowId = i;
      cowTasks[i] = Task.Run(() => cows[cowId].ProduceMilk());
    }

    var icecreamMakerTasks = new Task[icecreamMakersArray.Length];
    for (int i = 0; i < icecreamMakersArray.Length; i++)
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
          icecreamDecoratorsArray[icecreamDecoratorsId].ProcessResult(resultData);
        }
      });
    }

    await Task.WhenAll(cowTasks);
    _queueManager.CompleteMilkQueue();

    await Task.WhenAll(icecreamMakerTasks);
    _queueManager.CompleteIcecreamQueue();

    await Task.WhenAll(icecreamDecoratorsTasks);
    Console.WriteLine("Обработка завершена.");
  }
}
