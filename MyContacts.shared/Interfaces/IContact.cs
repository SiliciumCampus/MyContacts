using MyContacts.shared.Models;

namespace MyContacts.shared.Interfaces;

public interface IContact 
{
    Guid Id { get; set;  }

    IName Name { get; set; }
    IContactInfo ContactInfo { get; set; }
    IContactAdress ContactAdress { get; set; }
}
