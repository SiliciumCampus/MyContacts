namespace MyContacts.shared.Interfaces;

public interface IContactService
{

    /// <summary>
    /// Add a contact to a contacts list
    /// </summary>
    /// <param name="contact">a contact of type IContact</param>
    /// <returns>Return true if successful, or false if it fails or contact already exists</returns>


    bool AddContactToList(IContact contact);
    bool EditContactInList(IContact contact);
    bool RemoveContactFromList(IContact contact);
    List<IContact> GetContactsFromList();
    IContact GetContactFromListByEmail(string email);
    IContact GetContactFromListByPosition(int position);

}
