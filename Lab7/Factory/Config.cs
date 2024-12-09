using System;
using System.Runtime.CompilerServices;

namespace Factory;

public class Config
{
  public int cow_amount { get; set; }
  public int milk_amount_per_cow { get; set; }
  public int icecream_maker_amount { get; set; }
  public int icecream_per_milk { get; set; }
  public int icecream_decorator_amount { get; set; }
  public int milk_generation_time { get; set; }
  public int icecream_making_time { get; set; }
  public int icecream_decoration_time { get; set; }
  public int[]? random_time_interval { get; set; }
}
