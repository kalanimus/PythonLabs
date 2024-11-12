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

  public static void CreateYearByYearFile()
  {
    string path = "D:/PythonLabs/Lab6/Files/Year_by_year_results.csv";
    if (File.Exists(path))
    {
      File.Delete(path);
    }
    using (StreamWriter writer = new StreamWriter(path, true)) // true для добавления
        {
            writer.WriteLine("year,men_count,women_count");
        }
  }

  public static void CreateModelResultFile(List<int> men_count_by_age, List<int> women_count_by_age)
  {
    string path = "D:/PythonLabs/Lab6/Files/Model_results.csv";
    if (File.Exists(path))
    {
      File.Delete(path);
    }
    List<string> age_list = new List<string> {"0-18", "19-45", "45-65", "65-100" };
    using (StreamWriter writer = new StreamWriter(path, true)) // true для добавления
        {
            writer.WriteLine("age,men_count,women_count");
            for (int i = 0; i < age_list.Count; i++)
            {
              writer.WriteLine($"{age_list[i]},{men_count_by_age[i]},{women_count_by_age[i]}");
            }
        }
  }

  public static void WriteToFile(int year, int men_count, int women_count)
  {
    string path = "D:/PythonLabs/Lab6/Files/Year_by_year_results.csv";
    using (StreamWriter writer = new StreamWriter(path, true)) // true для добавления
        {
            writer.WriteLine($"{year},{men_count},{women_count}");
        }
  }
}
