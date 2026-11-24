namespace MessagesApi.Models;

public class Message
{
    public int Id { get; set; }

    /// <summary>Адреса відправника.</summary>
    public string From { get; set; } = "";

    public string Text { get; set; } = "";

    public DateTime SentAt { get; set; }
}
