#pragma warning disable

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Hosting;

namespace BlazeLamaWeb;


public class OllamaRunning {
    private HostApplicationBuilder? builder;
    private IHost? app;
    private IChatClient? chatClient;

    public List<ChatMessage> chatHistory = new();
    public bool canType = true;

    public void Initialize(string modelname, List<ChatData> data)
    {
        for(int i = 0; i < data.Count; i++)
        {
            if(i == 0 || i % 2 == 0) chatHistory.Add(new(ChatRole.User, data[i].Text));
            else chatHistory.Add(new(ChatRole.Assistant, data[i].Text));

        }
        builder = Host.CreateApplicationBuilder();

        builder.Services.AddChatClient(new OllamaChatClient(new Uri("http://localhost:11434"), modelname));

        app = builder.Build();

        chatClient = app.Services.GetRequiredService<IChatClient>();

    }
    public async Task<string> Response(string userPrompt)
    {
        if (chatClient == null)
            throw new InvalidOperationException("OllamaRunning.Initialize(...) не был вызван!");
        canType = false;
        chatHistory.Add(new(ChatRole.User, userPrompt));

        var chatRespone = "";
        await foreach (var item in chatClient.GetStreamingResponseAsync(chatHistory))
        {
            chatRespone += item.Text;
        }
        canType = true;
        chatHistory.Add(new(ChatRole.Assistant, chatRespone));
        return chatRespone;
    }
}