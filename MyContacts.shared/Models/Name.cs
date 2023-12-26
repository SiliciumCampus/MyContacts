using MyContacts.shared.Interfaces;

namespace MyContacts.shared.Models;

public class Name : IName
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
