namespace Factory.FileOperations;

public static class FileOperations
{
  public static string ReadConfigFile (string path)
  {
    string result;
    result = File.ReadAllText(path);
    return result;
  }
}
