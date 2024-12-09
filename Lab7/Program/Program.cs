using Factory;
using Factory.FileOperations;
using System.Text.Json;
using System.Diagnostics;
class Program
{
  static async Task Main(string[] args)
  {
    // string path = "../../../../Files/config.json";
    // var json_file = FileOperations.ReadConfigFile(path);
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    var json_file = FileOperations.ReadConfigFile(args[0]);
    if (json_file != null) {
      Config config = JsonSerializer.Deserialize<Config>(json_file);
      if (config != null) {
        var model = new Engine(config);
        FileOperations.CreateLogFile();
        await model.Run();
        FileOperations.WriteToLogFile("Program finished");
      }
    } else {
      Console.WriteLine("Error with config file!");
    }
    stopwatch.Stop();
    FileOperations.WriteToLogFile($"{Math.Round(stopwatch.Elapsed.TotalMilliseconds)} секунд выполнялась программа");
    FileOperations.WriteToLogFile($"{Math.Round(stopwatch.Elapsed.TotalSeconds, 2)} секунд выполнялась программа");
  }
}
