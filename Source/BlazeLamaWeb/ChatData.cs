using System.Text.Json.Serialization;

namespace BlazeLamaWeb;

public class ChatData
{
    public string Role { get; set; } = "";
    public string Text { get; set; } = "";

    [JsonConstructor]
    public ChatData(string Role, string Text)
    {
        this.Text = Text;
        this.Role = Role;
    }
}