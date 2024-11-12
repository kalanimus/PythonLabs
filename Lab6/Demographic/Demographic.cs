using System.Collections;
using System.Data;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Demographic.FileOperations;

namespace Demographic;

public enum Gender
{
  Male,
  Female
}

public interface IEngine
{
  public List<Person> population_men{get;}
  public List<Person> population_women{get;}
  public List<Person> decased{get;}
  public void Start ();
  public event Action<Engine>? YearTick;
}

public class Engine : IEngine
{
  public int _current_year;
  public int _last_year;

  public List<Person> population_men {get;}
  public List<Person> population_women {get;}
  public List<Person> decased {get;}

  public Dictionary<int,double> _population_rules;
  public Dictionary<int,double> _death_rules_male;
  public Dictionary<int,double> _death_rules_female;

  public bool _men_selebration = false;
  public bool _army = false;
  public bool _death_while_birth = false;
  public bool _dumb_ways_to_die = false;

  public Engine(int Start_year, int End_year, int Population_size, bool men_selebration, bool army, bool death_while_birth, bool dumb_ways_to_die, Dictionary<int,double> Population_rules, Dictionary<int,double> Death_rules_male, Dictionary<int,double> Death_rules_female)
  {
    _current_year = Start_year;
    _last_year = End_year;
    _population_rules = Population_rules;
    _death_rules_male = Death_rules_male;
    _death_rules_female = Death_rules_female;
    decased = new List<Person>();
    (population_men, population_women) = GeneratePopulation(_population_rules, Population_size / 1000, _current_year);
    _men_selebration = men_selebration;
    _army = army;
    _death_while_birth = death_while_birth;
    _dumb_ways_to_die = dumb_ways_to_die;
  }

  private (List<Person> men, List<Person> women) GeneratePopulation(Dictionary<int,double> rules, double population_size, int start_year){
    List<Person> result_men = new List<Person>();
    List<Person> result_women = new List<Person>();
    foreach (var ageRule in rules)
    {
      int count = (int)Math.Round(ageRule.Value / 1000 * population_size);
      int halfCount = count / 2;
      for (int i = 0; i < count; i++)
      {
        if (i < halfCount){
          Person man = new Person(start_year - ageRule.Key, Gender.Male);
          result_men.Add(man);
        }
        else
        {
          Person woman = new Person(start_year - ageRule.Key, Gender.Female);
          result_women.Add(woman);
        }
      }
    }
    return (result_men, result_women);
  }

  public event Action<Engine>? YearTick;
  public void Start ()
  {
    FileOperations.FileOperations.CreateYearByYearFile();
    foreach (var person in population_men)
    {
      person.Subscribe(this);
      Subscribe(person);
    }
    foreach (var person in population_women)
    {
      person.Subscribe(this);
      Subscribe(person);
    }

    int years = 0;
    
    for (; _current_year <= _last_year; _current_year++)
    {
      FileOperations.FileOperations.WriteToFile(_current_year, population_men.Count, population_women.Count);
      YearTick?.Invoke(this);
      UpdatePopulation();
      years++;     
    }
    FileOperations.FileOperations.CreateModelResultFile(CountResults(population_men), CountResults(population_women));
  }
  
  private List<int> CountResults(List<Person> people)
  {
    List<int> results = new List<int>{0, 0, 0, 0};
    for (int i = 0; i < people.Count; i++)
    {
      int age = _current_year - people[i]._birth_year;
      if (age < 19) results[0] += 1;
      else if (age < 45) results[1] += 1;
      else if (age < 66) results[2] += 1;
      else  results[3] += 1;
    }
    return results;
  }

  private void UpdatePopulation()
  {
    UpdateList(population_men);
    UpdateList(population_women);
  }

  private void UpdateList(List<Person> population)
  {
    List<Person> dead = new List<Person>();
    foreach (var person in population)
    {
      if (!person.is_alive)
      {
        dead.Add(person);
      }
      if (person.is_alone)
      {
        person.break_relationship();
      }
    }

    foreach (var person in dead)
    {
      population.Remove(person);
      decased.Add(person);
    }
  }
  public void OnChildBirth(Person child)
  {
    child.Subscribe(this);
    Subscribe(child);
    if (child.gender == Gender.Male)
    {
      population_men.Add(child);
    }
    else
    {
      population_women.Add(child);
    }
      
  }

  public void Subscribe(Person person){
    person.ChildBirth += OnChildBirth;
  }
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

public static class ProbabilityCalculator
{
  private static readonly Random _random = new Random();
  public static bool IsEventHappened(double eventProbability)
  {
    return _random.NextDouble() <= eventProbability;
  }
}