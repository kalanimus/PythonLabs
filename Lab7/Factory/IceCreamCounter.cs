using System;

namespace Factory;

public static class IceCreamCounter
{
  public static List<(string key, int value)> CountIceCreams(List<IceCream> iceCreams){
    List<(string key, int value)> result = new List<(string key, int value)>();
    for (int i = 0; i < iceCreams.Count; i++){
      string iceCream_taste = iceCreams[i].taste.ToString();
      UpdateOrAddPair(result, iceCream_taste);
    }
    return result;
  }

  private static void UpdateOrAddPair(List<(string key, int value)> pairs, string iceCream_taste)
  {
    for (int i = 0; i < pairs.Count; i++)
    {
      if (pairs[i].key == iceCream_taste)
      {
        pairs[i] = (pairs[i].key, pairs[i].value + 1);
        return;
      }
    }

    pairs.Add((iceCream_taste, 1));
  }
}
