using System;

namespace Factory;
public enum IceCreamTaste {
  Vanilla,
  Chocolate,
  Banana,
  Strawberry,
  Bublegum,
  Pistachio,
  Coffee,
  Toffee,
  SaltedCaramel,
  Licorice
}
public class IceCream
{
  public IceCreamTaste taste { get; set;}
  
  public IceCream(){
    taste = IceCreamTaste.Vanilla;
  }

  public void RandomizeTaste(){
    Random random = new Random();
    int randomValue = random.Next(Enum.GetValues(typeof(IceCreamTaste)).Length);
    taste = (IceCreamTaste)randomValue;
  }
}
