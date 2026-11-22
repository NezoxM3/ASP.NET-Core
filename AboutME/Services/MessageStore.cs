using System.Text.Encodings.Web;
using System.Text.Json;
using AboutMe.Models;

namespace AboutMe.Services;

public class MessageStore
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private readonly string _filePath;
    private readonly object _lock = new();
    private readonly List<GuestMessage> _messages;

    public MessageStore(IWebHostEnvironment env)
    {
        var dir = Path.Combine(env.ContentRootPath, "App_Data");
        Directory.CreateDirectory(dir);
        _filePath = Path.Combine(dir, "messages.json");
        _messages = Load();
    }

    public IReadOnlyList<GuestMessage> GetAll()
    {
        lock (_lock)
        {
            return _messages.OrderByDescending(m => m.SentAt).ToList();
        }
    }

    public void Add(GuestMessage message)
    {
        lock (_lock)
        {
            _messages.Add(message);
            var json = JsonSerializer.Serialize(_messages, JsonOptions);
            File.WriteAllText(_filePath, json);
        }
    }

    private List<GuestMessage> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<GuestMessage>();
        }

        try
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<GuestMessage>>(json)
                   ?? new List<GuestMessage>();
        }
        catch (JsonException)
        {
            return new List<GuestMessage>();
        }
    }
}
