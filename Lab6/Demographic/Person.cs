using System;

namespace Demographic;

public enum Gender
{
  Male,
  Female
}

public class Person
{
  public readonly int _birth_year;
  public int _rip_year;
  public Gender gender;
  public bool is_alive;
  public bool is_in_army = false;
  public bool was_in_army = false;
  public bool is_alone = true;
  private double _birth_chance = 0.151;
  private double _birth_chance_girl = 0.55;

  public Person (int Birth_year, Gender Gender){
    gender = Gender;
    _birth_year = Birth_year;
    is_alive = true;
  }

  public event Action<Person>? ChildBirth;

  public void break_relationship ()
  {
    is_alone = true;
  }
  public void OnYearTick(Engine engine)
  {
    if (is_alive == false) return;

    int currentYear = engine._current_year;
    int age = engine._current_year - this._birth_year;

    if (this.gender == Gender.Male && age == 18 && engine._men_selebration) // 18ти летний прикол
    {
      if (ProbabilityCalculator.IsEventHappened(0.99))
      {
        is_alive = false;
        _rip_year = currentYear;
        return;
      }
    }

    if (gender == Gender.Male && age >= 18 && age <= 30 && engine._army) // армия
    {
      if (is_in_army) // дембель
      {
        is_in_army = false;
        was_in_army = true;
      }

      if (ProbabilityCalculator.IsEventHappened(0.8) && !was_in_army) // забрали в армию
      {
        is_in_army = true;
      }

      if (ProbabilityCalculator.IsEventHappened(0.6) && is_in_army) //смерть в армии
      {
        is_alive = false;
        _rip_year = currentYear;
        return;
      }
    }

    if (gender == Gender.Female &&
     engine.population_men.Count > 0 && 
     age >= 18 && 
     age <= 45)
    {
      bool hasPartner  = engine.population_men.AsParallel().Any(person => 
      {
        int age = currentYear - person._birth_year;
        bool isAlive = person.is_alive;
        bool result = age >= 18 && age <= 45 && isAlive && !is_in_army && is_alone && ProbabilityCalculator.IsEventHappened(0.05);
        person.is_alone = false;
        return result;
    });
      if (hasPartner && ProbabilityCalculator.IsEventHappened(_birth_chance))
    {
      Gender childGender = ProbabilityCalculator.IsEventHappened(_birth_chance_girl) ? Gender.Female : Gender.Male;
      Person child = new Person (currentYear, childGender);
      ChildBirth?.Invoke(child);
      if (engine._death_while_birth && ProbabilityCalculator.IsEventHappened(0.0342)) // смерть при родах 0.0342
      {
        is_alive = false;
        _rip_year = currentYear;
        return;
      }
    }
    }
  
  if (engine._dumb_ways_to_die && gender == Gender.Female && //смерть по тупости
     ((age >= 15 && age <= 30) ||
     (age >= 70 && age <= 100)) && 
     ProbabilityCalculator.IsEventHappened(0.015))
     {
        is_alive = false;
        _rip_year = currentYear;
        return;
     }

    bool isFemale = gender == Gender.Female;
    bool isDeathHappened = (age <= 100 && ProbabilityCalculator.IsEventHappened(isFemale ? engine._death_rules_female[age] : 
    engine._death_rules_male[age])) || 
    (age > 100 && ProbabilityCalculator.IsEventHappened(engine._death_rules_male[100]));

    if (isDeathHappened)
    {
      is_alive = false;
      _rip_year = currentYear;
    }
  }
  public void Subscribe(Engine engine)
  {
    engine.YearTick += this.OnYearTick;
  }
}
