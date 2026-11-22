using System.ComponentModel.DataAnnotations;
using AboutMe.Models;
using AboutMe.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AboutMe.Pages;

public class MessageModel : PageModel
{
    private readonly MessageStore _store;

    public MessageModel(MessageStore store)
    {
        _store = store;
    }

    [BindProperty]
    public MessageInput Input { get; set; } = new();

    public IReadOnlyList<GuestMessage> Messages { get; private set; } =
        new List<GuestMessage>();

    public void OnGet()
    {
        Messages = _store.GetAll();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            Messages = _store.GetAll();
            return Page();
        }

        _store.Add(new GuestMessage
        {
            Name = Input.Name.Trim(),
            Email = Input.Email.Trim(),
            Text = Input.Text.Trim(),
            SentAt = DateTime.Now
        });

        return RedirectToPage();
    }
}

public class MessageInput
{
    [Display(Name = "Ім'я")]
    [Required(ErrorMessage = "Вкажіть ім'я")]
    [StringLength(50, MinimumLength = 2,
        ErrorMessage = "Ім'я має містити від 2 до 50 символів")]
    public string Name { get; set; } = "";

    [Display(Name = "Пошта")]
    [Required(ErrorMessage = "Вкажіть пошту")]
    [EmailAddress(ErrorMessage = "Некоректна адреса пошти")]
    public string Email { get; set; } = "";

    [Display(Name = "Повідомлення")]
    [Required(ErrorMessage = "Напишіть повідомлення")]
    [StringLength(1000, ErrorMessage = "Не більше 1000 символів")]
    public string Text { get; set; } = "";
}
