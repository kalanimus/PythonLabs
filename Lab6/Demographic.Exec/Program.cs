using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Demographic;
using Demographic.FileOperations;

class Program
{
  static void Main(string[] args)
{
    var age_rules = FileOperations.ReadAge(args[0]);
    var (men_rules, women_rules) = FileOperations.ReadRules(args[1]);
    var json_file = FileOperations.ReadConfigFile(args[2]);
    // var json_file = FileOperations.ReadConfigFile("../../../../Files/config.json");
    Config config = JsonSerializer.Deserialize<Config>(json_file);
    // Console.WriteLine(config.engine_config.people_per_Person);
    if (config != null)
    {
      IEngine model = new Demographic.Engine(config, age_rules, men_rules, women_rules);

      model.Start();
      Console.WriteLine("Finished");
    }
    else
    {
      Console.WriteLine("Config read error");
    }

  }
}
