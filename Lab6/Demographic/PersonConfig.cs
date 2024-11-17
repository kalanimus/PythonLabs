using System;

namespace Demographic;

public class PersonConfig
{
  public double birth_chance { get; set; }
  public double girl_birth_chance { get; set; }
  public int majority_age { get; set; }
  public double majority_death_chance { get; set; }
  public List<int>? army_age { get; set; }
  public double conscription_chance { get; set; }
  public double army_death_chance { get; set; }
  public List<int>? pregnancy_age_women { get; set; }
  public List<int>? pregnancy_age_men { get; set; }
  public double find_partner_chance { get; set; }
  public double death_while_birth_chance { get; set; }
  public List<int>? dumb_ways_to_die_age { get; set; }
  public double dumb_ways_to_die_chance { get; set; }
  public int hundred_years { get; set; }
}
