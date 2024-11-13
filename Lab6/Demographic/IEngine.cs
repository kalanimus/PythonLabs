using System;

namespace Demographic;

public interface IEngine
{
  public List<Person> population_men{get;}
  public List<Person> population_women{get;}
  public List<Person> decased{get;}
  public void Start ();
  public event Action<Engine>? YearTick;
}