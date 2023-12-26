using MyContacts.shared.Interfaces;
using MyContacts.shared.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MyContacts.shared.Services;

public class ContactService : IContactService
{
    private readonly IFileService _fileService = new FileService();
    private string _filePath = @"c:\Projects\MyContacts\contacts.json";
    

    internal List<IContact> _contacts = [];

    public ContactService()
    {

        try
        { 
           GetContactsFromList();

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public bool AddContactToList(IContact contact)
    {
        
        try
        {
            GetContactsFromList();
            if (!_contacts.Any(x => x.ContactInfo.Email.ToLower() == contact.ContactInfo.Email.ToLower()))
            {
                _contacts.Add(contact);

                string json = JsonConvert.SerializeObject(_contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                _fileService.SaveContentToFile(_filePath, json);

            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public bool RemoveContactFromList(IContact contact)
    {
        try
        {
            GetContactsFromList();

            
            if (_contacts.Any(x => x.Id == contact.Id))
            {
                
                _contacts.Remove(_contacts.Find(x => x.Id == contact.Id)!);

                string json = JsonConvert.SerializeObject(_contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                _fileService.SaveContentToFile(_filePath, json);

            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public IContact GetContactFromListByEmail(string email)
    {
        try
        {
            GetContactsFromList();

            var contact = _contacts.FirstOrDefault(x => x.ContactInfo.Email.ToLower() == email);
            return contact ??= null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public IContact GetContactFromListByPosition(int position) 
    {
        try
        {
            GetContactsFromList();

            var contact = _contacts[position - 1];
            return contact ??= null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public List<IContact> GetContactsFromList()
    {
        try
        {
          
            string content = _fileService.GetContentFromFile(_filePath);
            if (!string.IsNullOrEmpty(content))
            {
                _contacts = JsonConvert.DeserializeObject<List<IContact>>(content, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })!;
                return _contacts;
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
