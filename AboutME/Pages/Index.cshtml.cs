using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AboutMe.Pages;

public class IndexModel : PageModel
{
    // TODO: впишіть свої дані
    public string FirstName { get; } = "Максим";
    public string LastName { get; } = "Пислар";
    public string City { get; } = "Чернівці";
    public string Country { get; } = "Україна";
    public DateOnly BirthDate { get; } = new DateOnly(2008, 10, 30);

    public int Age { get; private set; }
    public string Initials => $"{FirstName[0]}{LastName[0]}";

    public void OnGet()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        Age = today.Year - BirthDate.Year;
        if (today < BirthDate.AddYears(Age))
        {
            Age--;
        }
    }
}
