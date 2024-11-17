using System;

namespace Demographic;

public class EngineConfig
{
  public int start_year { get; set; }
  public int end_year { get; set; }
  public int population_count { get; set; }
  public bool men_selebration { get; set; }
  public bool army { get; set; }
  public bool death_while_birth { get; set; }
  public bool dumb_ways_to_die { get; set; }
  public int people_per_Person { get; set; }
  public List<int>? result_age_interval { get; set; }
}
