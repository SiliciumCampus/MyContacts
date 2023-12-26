using MyContacts.shared.Interfaces;

namespace MyContacts.shared.Models;

public class ContactAdress : IContactAdress
{
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
}
