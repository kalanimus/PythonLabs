using Factory;
using Factory.FileOperations;
using System.Text.Json;
class Program
{
  static async Task Main(string[] args)
  {
    var json_file = FileOperations.ReadConfigFile(args[0]);
    
    Config config = JsonSerializer.Deserialize<Config>(json_file);

    var model = new Engine();
    await model.Run();
  }
}
