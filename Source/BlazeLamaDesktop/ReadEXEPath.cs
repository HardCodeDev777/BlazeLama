using System.IO;
using System.Text.Json;

namespace BlazeLamaDesktop;

public static class ReadEXEPath
{
    public async static Task<string> ReadFromJSONPath()
    {
        if (!File.Exists(@"Data\DataOllamaPath.json")) return string.Empty;
        using FileStream fs = new(@"Data\DataOllamaPath.json", FileMode.OpenOrCreate);
        var path = await JsonSerializer.DeserializeAsync<string>(fs);
        return path!;
    }
}