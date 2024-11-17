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


  private PersonConfig config;

  public Person (int Birth_year, Gender Gender, PersonConfig config){
    gender = Gender;
    _birth_year = Birth_year;
    is_alive = true;
    this.config = config;
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

    if (this.gender == Gender.Male && age == config.majority_age && engine.config.men_selebration) // 18ти летний прикол
    {
      if (ProbabilityCalculator.IsEventHappened(config.majority_death_chance))
      {
        is_alive = false;
        _rip_year = currentYear;
        return;
      }
    }

    if (gender == Gender.Male && age >= config.army_age[0] && age <= config.army_age[1] && engine.config.army) // армия
    {
      if (is_in_army) // дембель
      {
        is_in_army = false;
        was_in_army = true;
      }

      if (ProbabilityCalculator.IsEventHappened(config.conscription_chance) && !was_in_army) // забрали в армию
      {
        is_in_army = true;
      }

      if (ProbabilityCalculator.IsEventHappened(config.army_death_chance) && is_in_army) //смерть в армии
      {
        is_alive = false;
        _rip_year = currentYear;
        return;
      }
    }

    if (gender == Gender.Female &&
     engine.population_men.Count > 0 && 
     age >= config.pregnancy_age_women[0] && 
     age <= config.pregnancy_age_women[1])
    {
      bool hasPartner  = engine.population_men.AsParallel().Any(person => 
      {
        int age = currentYear - person._birth_year;
        bool isAlive = person.is_alive;
        bool result = age >= config.pregnancy_age_men[0] && 
                      age <= config.pregnancy_age_men[1] && 
                      isAlive && !is_in_army && is_alone && 
                      ProbabilityCalculator.IsEventHappened(config.find_partner_chance);
        person.is_alone = false;
        return result;
    });
      if (hasPartner && ProbabilityCalculator.IsEventHappened(config.birth_chance))
    {
      Gender childGender = ProbabilityCalculator.IsEventHappened(config.girl_birth_chance) ? Gender.Female : Gender.Male;
      Person child = new Person (currentYear, childGender, config);
      ChildBirth?.Invoke(child);
      if (engine.config.death_while_birth && ProbabilityCalculator.IsEventHappened(config.death_while_birth_chance)) // смерть при родах 0.0342
      {
        is_alive = false;
        _rip_year = currentYear;
        return;
      }
    }
    }
  
  if (engine.config.dumb_ways_to_die && gender == Gender.Female && //смерть по тупости
     ((age >= config.dumb_ways_to_die_age[0] && age <= config.dumb_ways_to_die_age[1]) ||
     (age >= config.dumb_ways_to_die_age[2])) && 
     ProbabilityCalculator.IsEventHappened(config.dumb_ways_to_die_chance))
     {
        is_alive = false;
        _rip_year = currentYear;
        return;
     }

    bool isFemale = gender == Gender.Female;
    bool isDeathHappened = (age <= config.hundred_years && ProbabilityCalculator.IsEventHappened(isFemale ? engine._death_rules_female[age] : 
    engine._death_rules_male[age])) || 
    (age > config.hundred_years && ProbabilityCalculator.IsEventHappened(engine._death_rules_male[100]));

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

  public void Unsubscribe(Engine engine)
  {
    engine.YearTick -= this.OnYearTick;
  }
}
