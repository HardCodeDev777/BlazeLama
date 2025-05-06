namespace BlazeLamaWeb;
public static class PageData
{
    public static List<string> routes = new();

    public static Dictionary<string, List<string>> routesNames = new();

    public static Dictionary<string, List<ChatData>> history = new();

    public static Dictionary<string, string> chatModels = new();
}