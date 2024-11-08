using System;
using Demographic;
using Demographic.FileOperations;

class Program
{
  static void Main(string[] args)
  {
    var d1 = FileOperations.ReadAge("../../../../Files/InitialAge.csv");
    var (d2, d3) = FileOperations.ReadRules("../../../../Files/DeathRules.csv");
    var model = new Demographic.Engine(1990, 2140, 130_000_000, d1, d2, d3);
    // Demographic.Engine.PrintPeopleList(model.population);
    

    model.Start();
  }
}
