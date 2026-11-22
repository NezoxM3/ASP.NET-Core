namespace AboutMe.Models;

public class GuestMessage
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Text { get; set; } = "";
    public DateTime SentAt { get; set; }
}
