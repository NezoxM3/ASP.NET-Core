using MessagesApi.Models;
using MessagesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MessagesApi.Controllers;

[ApiController]
[Route("api/messages")]
public class MessagesController : ControllerBase
{
    private readonly MessageStore _store;

    public MessagesController(MessageStore store)
    {
        _store = store;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Message>> GetAll()
    {
        return Ok(_store.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Message> GetById(int id)
    {
        var message = _store.GetById(id);
        if (message == null)
        {
            return NotFound(new { error = $"Повідомлення з id={id} не знайдено" });
        }
        return Ok(message);
    }

    [HttpGet("by-sender")]
    public ActionResult<IEnumerable<Message>> GetBySender(
        [FromQuery] string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            return BadRequest(new { error = "Параметр address є обов'язковим" });
        }
        return Ok(_store.GetBySender(address));
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (!_store.Delete(id))
        {
            return NotFound(new { error = $"Повідомлення з id={id} не знайдено" });
        }
        return NoContent();
    }
}
