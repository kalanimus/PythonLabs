using System;

namespace Factory;

public static class RandomNumber
{
  public static int GetRandomNumberFromRange(int minValue, int maxValue){
    Random random = new Random();
    return random.Next(minValue, maxValue);
  }
}
