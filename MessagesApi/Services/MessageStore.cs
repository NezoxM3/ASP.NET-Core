using MessagesApi.Models;

namespace MessagesApi.Services;
public class MessageStore
{
    private readonly object _lock = new();
    private readonly List<Message> _messages;

    public MessageStore()
    {
        var now = DateTime.Now;
        _messages = new List<Message>
        {
            new() { Id = 1, From = "anna@example.com", Text = "Привіт! Як справи?", SentAt = now.AddHours(-5) },
            new() { Id = 2, From = "petro@example.com", Text = "Нагадую про зустріч завтра о 10:00.", SentAt = now.AddHours(-4) },
            new() { Id = 3, From = "anna@example.com", Text = "Надішли, будь ласка, звіт.", SentAt = now.AddHours(-3) },
            new() { Id = 4, From = "olena@example.com", Text = "Дякую за допомогу!", SentAt = now.AddHours(-2) },
            new() { Id = 5, From = "petro@example.com", Text = "Зустріч перенесено на 11:00.", SentAt = now.AddHours(-1) },
            new() { Id = 6, From = "anna@example.com", Text = "Чекаю на відповідь.", SentAt = now.AddMinutes(-10) }
        };
    }

    public IReadOnlyList<Message> GetAll()
    {
        lock (_lock)
        {
            return _messages.ToList();
        }
    }

    public Message? GetById(int id)
    {
        lock (_lock)
        {
            return _messages.FirstOrDefault(m => m.Id == id);
        }
    }

    public IReadOnlyList<Message> GetBySender(string address)
    {
        lock (_lock)
        {
            return _messages
                .Where(m => string.Equals(
                    m.From, address.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var message = _messages.FirstOrDefault(m => m.Id == id);
            return message != null && _messages.Remove(message);
        }
    }
}
