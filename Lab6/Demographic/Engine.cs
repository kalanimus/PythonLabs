using System.Collections;
using System.Data;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Xml.Serialization;
using Demographic.FileOperations;

namespace Demographic;

public class Engine : IEngine
{
  public int _current_year;

  public List<Person> population_men { get; }
  public List<Person> population_women { get; }
  public List<Person> decased { get; }

  public Dictionary<int, double> _population_rules;
  public Dictionary<int, double> _death_rules_male;
  public Dictionary<int, double> _death_rules_female;

  public EngineConfig config;


  public Engine(Config config,
    Dictionary<int, double> Population_rules,
    Dictionary<int, double> Death_rules_male,
    Dictionary<int, double> Death_rules_female)
  {
    this.config = config.engine_config;
    _current_year = this.config.start_year;
    _population_rules = Population_rules;
    _death_rules_male = Death_rules_male;
    _death_rules_female = Death_rules_female;
    decased = new List<Person>();
    (population_men, population_women) = GeneratePopulation(_population_rules,
                                          config.engine_config.population_count / config.engine_config.people_per_Person,
                                          _current_year,
                                          config.person_config);
  }

  private (List<Person> men, List<Person> women) GeneratePopulation(Dictionary<int, double> rules,
    double population_size,
    int start_year,
    PersonConfig person_config)
  {
    List<Person> result_men = new List<Person>();
    List<Person> result_women = new List<Person>();
    foreach (var ageRule in rules)
    {
      int count = (int)Math.Round(ageRule.Value / config.people_per_Person * population_size);
      int halfCount = count / 2;
      for (int i = 0; i < count; i++)
      {
        if (i < halfCount)
        {
          Person man = new Person(start_year - ageRule.Key, Gender.Male, person_config);
          result_men.Add(man);
        }
        else
        {
          Person woman = new Person(start_year - ageRule.Key, Gender.Female, person_config);
          result_women.Add(woman);
        }
      }
    }
    return (result_men, result_women);
  }

  public event Action<Engine>? YearTick;
  public void Start()
  {
    FileOperations.FileOperations.CreateYearByYearFile();
    foreach (var person in population_men)
    {
      person.Subscribe(this);
      // Subscribe(person);
    }
    foreach (var person in population_women)
    {
      person.Subscribe(this);
      Subscribe(person);
    }

    int years = 0;

    for (; _current_year <= config.end_year; _current_year++)
    {
      FileOperations.FileOperations.WriteToFile(_current_year,
                                                population_men.Count,
                                                population_women.Count);
      YearTick?.Invoke(this);
      UpdatePopulation();
      years++;
    }
    FileOperations.FileOperations.CreateModelResultFile(CountResults(population_men), CountResults(population_women));
  }

  private List<int> CountResults(List<Person> people)
  {
    List<int> results = new List<int> { 0, 0, 0, 0 };
    for (int i = 0; i < people.Count; i++)
    {
      int age = _current_year - people[i]._birth_year;
      if (age < config.result_age_interval[0]) results[0] += 1;
      else if (age < config.result_age_interval[1]) results[1] += 1;
      else if (age < config.result_age_interval[2]) results[2] += 1;
      else results[3] += 1;
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

  public void Subscribe(Person person)
  {
    person.ChildBirth += OnChildBirth;
  }

  public void Unsubscribe(Person person)
  {
    person.ChildBirth -= OnChildBirth;
  }
}