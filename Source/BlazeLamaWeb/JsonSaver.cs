using System.Text.Json;

namespace BlazeLamaWeb;

public static class JsonSaver {

    public async static Task SaveToJSON(string ollamaPath)
    {
        using FileStream fs = new(@"Data\DataOllamaPath.json", FileMode.Create);
        await JsonSerializer.SerializeAsync(fs, ollamaPath);
    }

    public async static Task SaveToJSON(Dictionary<string, List<ChatData>> datas)
    {
        using FileStream fs = new(@"Data\Data.json", FileMode.Create);
        await JsonSerializer.SerializeAsync(fs, datas);
    }

    public async static Task SaveToJSON(Dictionary<string, List<string>> datas)
    {
        using FileStream fs = new(@"Data\DataNames.json", FileMode.Create);
        await JsonSerializer.SerializeAsync(fs, datas);
    }
    public async static Task SaveToJSON(Dictionary<string, string> datas)
    {
        using FileStream fs = new(@"Data\DataModels.json", FileMode.Create);
        await JsonSerializer.SerializeAsync(fs, datas);
    }

    public async static Task<Dictionary<string, List<ChatData>>> ReadFromJSON()
    {
        if (!File.Exists(@"Data\Data.json")) return new Dictionary<string, List<ChatData>>();

        using FileStream fs = new(@"Data\Data.json", FileMode.OpenOrCreate);
        var datas = await JsonSerializer.DeserializeAsync<Dictionary<string, List<ChatData>>>(fs);
        return datas!;
    }
    public async static Task<Dictionary<string, List<string>>> ReadFromJSONNames()
    {
        if (!File.Exists(@"Data\DataNames.json")) return new Dictionary<string, List<string>>();
        using FileStream fs = new(@"Data\DataNames.json", FileMode.OpenOrCreate);
        var datas = await JsonSerializer.DeserializeAsync<Dictionary<string, List<string>>>(fs);
        return datas!;
    }
    public async static Task<Dictionary<string, string>> ReadFromJSONModels()
    {
        if (!File.Exists(@"Data\DataModels.json")) return new Dictionary<string, string>();
        using FileStream fs = new(@"Data\DataModels.json", FileMode.OpenOrCreate);
        var datas = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(fs);
        return datas!;
    }
}