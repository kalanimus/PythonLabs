using System.Collections.Concurrent;
public class QueueManager
{
  private readonly BlockingCollection<string> _milkQueue = new();
  private readonly BlockingCollection<string> _IcecreamQueue = new();

  public void AddMilk(string eventData) => _milkQueue.Add(eventData);
  public void AddIcecream(string resultData) => _IcecreamQueue.Add(resultData);
  public BlockingCollection<string> GetMilkQueue() => _milkQueue;
  public BlockingCollection<string> GetIcecreamQueue() => _IcecreamQueue;
  public void CompleteMilkQueue() => _milkQueue.CompleteAdding();
  public void CompleteIcecreamQueue() => _IcecreamQueue.CompleteAdding();
}
