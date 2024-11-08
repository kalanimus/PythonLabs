namespace Demographic.FileOperations;

using System;
using System.Globalization;
using System.IO;

public static class FileOperations
{
  public static Dictionary<int, double> ReadAge(string path)
  {
    Dictionary<int, double> result = new Dictionary<int, double> ();

    string[] lines = File.ReadAllLines(path);
    for (int i = 1; i < lines.Length; i++) 
    {
      string[] values = lines[i].Split(",");
      foreach (string value in values){value.Trim();}
      if (values.Length == 2 &&
      int.TryParse(values[0], NumberStyles.Any, CultureInfo.InvariantCulture, out int key) &&
      double.TryParse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double probability)){
        result[key] = probability;
      }
    }
    return result;
  }

  public static (Dictionary<int, double> maleRules, Dictionary<int, double> femaleRules) ReadRules(string path)
  {
    Dictionary<int,double> maleResult = new Dictionary<int, double> ();
    Dictionary<int,double> femaleResult = new Dictionary<int, double> ();

    string[] lines = File.ReadAllLines(path);
    for (int i = 1; i < lines.Length; i++) 
    {
      string[] values = lines[i].Split(',');
      foreach (string value in values){value.Trim();}
      if (values.Length == 4 &&
          int.TryParse(values[0], NumberStyles.Any, CultureInfo.InvariantCulture, out int StartAge) &&
          int.TryParse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture, out int EndAge) &&
          double.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double MaleProbability) &&
          double.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out  double FemaleProbability))
          {
            for (int age = StartAge; age <= EndAge; age++)
            {
              maleResult[age] = MaleProbability;
              femaleResult[age] = FemaleProbability;
            }
          }
    }
    return(maleResult, femaleResult);
  }

  public static void PrintDictionary(Dictionary<int, double> dictionary)
{
    foreach (var entry in dictionary)
    {
        Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
    }
}
}
