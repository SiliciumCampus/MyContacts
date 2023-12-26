using MyContacts.shared.Interfaces;

namespace MyContacts.shared.Models;

public class ContactInfo : IContactInfo
{
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}
