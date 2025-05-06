namespace BlazeLamaWeb;

public static class DeleteData
{
    public static void DeleteFiles()
    {
        string exePath = AppDomain.CurrentDomain.BaseDirectory;
        string targetFolder = Path.Combine(exePath, "Data");

        if (Directory.Exists(targetFolder)) foreach (string file in Directory.GetFiles(targetFolder)) File.Delete(file);           
    }
}

