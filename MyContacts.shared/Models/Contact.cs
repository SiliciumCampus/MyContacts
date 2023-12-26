using MyContacts.shared.Interfaces;

namespace MyContacts.shared.Models;

public class Contact : IContact
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public IName Name { get; set; } = new Name();
    public IContactInfo ContactInfo { get; set; } = new ContactInfo();
    public IContactAdress ContactAdress { get; set; } = new ContactAdress();
}
