using System;
using Demographic;
using Demographic.FileOperations;

class Program
{
  static void Main(string[] args)
  {
    var d1 = FileOperations.ReadAge(args[0]);
    var (d2, d3) = FileOperations.ReadRules(args[1]);
    IEngine model = new Demographic.Engine(int.Parse(args[2]), int.Parse(args[3]), int.Parse(args[4]), d1, d2, d3);
    // Demographic.Engine.PrintPeopleList(model.population);
    

    model.Start();
    Console.WriteLine("Finished");
  }
}
