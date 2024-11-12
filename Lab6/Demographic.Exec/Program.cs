using System;
using Demographic;
using Demographic.FileOperations;

class Program
{
  static void Main(string[] args)
  {
    var age_rules = FileOperations.ReadAge(args[0]);
    var (men_rules, women_rules) = FileOperations.ReadRules(args[1]);
    IEngine model = new Demographic.Engine(int.Parse(args[2]), int.Parse(args[3]), int.Parse(args[4]), bool.Parse(args[5]), bool.Parse(args[6]), bool.Parse(args[7]), bool.Parse(args[8]), age_rules, men_rules, women_rules);
    // Demographic.Engine.PrintPeopleList(model.population);
    

    model.Start();
    Console.WriteLine("Finished");
  }
}
