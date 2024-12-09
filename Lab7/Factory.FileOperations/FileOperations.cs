namespace Factory.FileOperations;
using System.Threading;

public static class FileOperations
{
  private static readonly object _lock = new object();
  public static string ReadConfigFile (string path)
  {
    string result;
    result = File.ReadAllText(path);
    return result;
  }

  public static void CreateLogFile()
  {
    // string path = "../../../../Files/log.txt";
    string path = "Files/log.txt";

    if (File.Exists(path))
    {
      File.Delete(path);
    }
  }
    public static void WriteToLogFile(string message)
  {
    // string path = "../../../../Files/log.txt";
    string path = "Files/log.txt";
    lock (_lock){
      using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}: {message}");
        }
    }
  }

  public static void MakeResultFile(List<(string key, int value)> results)
  {
    // string path = "../../../../Files/iceCreamCount.csv";
    string path = "Files/iceCreamCount.csv";

    if (File.Exists(path))
    {
      File.Delete(path);
    }
    using (StreamWriter writer = new StreamWriter(path, true))
      {
        writer.WriteLine("Вкус,Количество");
        for (int i = 0; i < results.Count; i++){
          writer.WriteLine($"{results[i].key},{results[i].value}");
        }
      }
    
  }
}
