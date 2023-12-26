using MyContacts.shared.Interfaces;
using MyContacts.shared.Models;
using MyContacts.shared.Services;
using System.Diagnostics;

namespace MyContacts.ConsoleApp.Services;

internal class MenuService
{
    private readonly IContactService _contactService = new ContactService();

    public void ShowMainMenu() 
    {
        Console.Clear();
        Console.WriteLine("\n\n\n\n\n");
        Console.WriteLine("     _________________");
        Console.WriteLine("     |                |");
        Console.WriteLine("     |   MyContacts   |");
        Console.WriteLine("     |________________|");
        Console.WriteLine("\n\n\n\n\n\n\n\n");
        Console.WriteLine("        By Caroline\n");
        Console.WriteLine("       Press any key");
        Console.WriteLine("        To continue");


        Console.ReadKey();
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("    Main Menu");
            Console.WriteLine("-------------------");
            Console.WriteLine($"{"1.",-4}Show contacts list");
            Console.WriteLine($"{"2.",-4}Add a new contact to the list");
            Console.WriteLine($"{"3.",-4}Search for a specific contact");
            Console.WriteLine();
            Console.WriteLine($"{"Q.",-4}Exit application");
            Console.WriteLine();
            Console.Write("Option: ");
            string option = Console.ReadLine()!;

            try
            {
                switch (option.ToLower())
                {
                    case "1":
                        ShowAllContactsMenu();
                        break;
                    case "2":
                        AddContactMenu();
                        break;
                    case "3":
                        SearchForContactMenu();
                        break;
                    case "q":
                        Console.Clear();
                        Console.WriteLine("\n\n\n");
                        Console.WriteLine("     Exiting application! Goodbye!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again!");
                        Console.ReadKey();
                        break;
                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        
    }

    public void ShowAllContactsMenu()
    {
        var contacts = _contactService.GetContactsFromList();
        if (contacts != null) 
        {
            Console.Clear();
            Console.WriteLine("    Contacts");
            Console.WriteLine("----------------");

            int i = 1;
            foreach (var contact in contacts)
            {

                Console.WriteLine($"{i}.  {contact.Name.FirstName} {contact.Name.LastName}");
                i++;
            }
            Console.ReadKey();
        }
        else 
        { 
            Console.Clear();
            Console.WriteLine("No contacts in list");
            Console.ReadKey();
        }
    }

    public void AddContactMenu()
    {
        IContact contact = new Contact();

        Console.Clear();
        Console.WriteLine("Enter your contacts info");
        Console.WriteLine("-----------------------");
        Console.Write("First name: ");
        contact.Name.FirstName = UppercaseFirst(Console.ReadLine()!);

        Console.Write("Last name: ");
        contact.Name.LastName = UppercaseFirst(Console.ReadLine()!);

        Console.Write("Email: ");
        contact.ContactInfo.Email = Console.ReadLine()!;

        Console.Write("Phonenumber: ");
        contact.ContactInfo.PhoneNumber = Console.ReadLine()!;

        Console.WriteLine("\nWould you like to add an adress too? (Y/N)");
        string answer = Console.ReadLine()!.ToLower();

        if (answer == "y")
        {
            Console.Write("Adress: ");
            contact.ContactAdress.Address = UppercaseFirst(Console.ReadLine()!);

            Console.Write("City: ");
            contact.ContactAdress.City = UppercaseFirst(Console.ReadLine()!);

            Console.Write("Zip-Code: ");
            contact.ContactAdress.ZipCode = Console.ReadLine()!;
        }

        _contactService.AddContactToList(contact);
    }

    public void ShowContactMenu(IContact contact)
    {
        Console.Clear();
        int fullNameLength = (contact.Name.FirstName.Length + contact.Name.LastName.Length);
        int middle = (fullNameLength + 30) / 2;
        int emailLength = contact.ContactInfo.Email.Length;
        int phoneLength = contact.ContactInfo.PhoneNumber.Length;

        Console.WriteLine("");
        Console.WriteLine($"               {contact.Name.FirstName} {contact.Name.LastName}");
        for (int i = -31; i< fullNameLength; i++)
        {
            Console.Write("_");
        }
        Console.WriteLine("\n");


        for (int i = 1; i <middle ; i++)
        {
            Console.Write(" ");
        }
        Console.Write("Email:\n");

        for (int i = 0; i < (middle - (emailLength / 2)); i++)
        {
            Console.Write(" ");
        }
        Console.Write($"{contact.ContactInfo.Email}\n\n");


        for (int i = 2; i < middle; i++)
        {
            Console.Write(" ");
        }
        Console.Write("PhoneNr:\n");

        for (int i = 0; i < (middle - (phoneLength / 2)); i++)
        {
            Console.Write(" ");
        }
        Console.Write($"{contact.ContactInfo.PhoneNumber}\n\n");

        if (contact.ContactAdress.Address != null && contact.ContactAdress.ZipCode != null && contact.ContactAdress.City != null) 
        {

            int adressLength = contact.ContactAdress.Address.Length;
            int postalLength = (contact.ContactAdress.ZipCode.Length + contact.ContactAdress.City.Length);
            for (int i = 2; i < middle; i++)
            {
                Console.Write(" ");
            }
            Console.Write("Address:\n");

            for (int i = 0; i < (middle - (adressLength / 2)); i++)
            {
                Console.Write(" ");
            }
            Console.Write($"{contact.ContactAdress.Address}\n");

            for (int i = 0; i < (middle - (postalLength / 2)); i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine($"{contact.ContactAdress.ZipCode}  {contact.ContactAdress.City}\n");


        }

        for (int i = -31; i < fullNameLength; i++)
        {
            Console.Write("_");
        }
        Console.WriteLine("");

        Console.ReadKey();
        bool running = true;

        while (running)
        {
            Console.WriteLine("");
            Console.WriteLine("      Would you like to:");
            Console.WriteLine("");
            Console.WriteLine("1.    Change contact info");
            Console.WriteLine("2.    Delete contact\n");
            Console.WriteLine("Q.    Change nothing and go back to main menu\n");
            Console.Write("      Option: ");
            string option = Console.ReadLine()!.ToLower();

            switch (option)
            {
                case "1":
                    running = false;
                    EditContactMenu(contact);
                    break;
                case "2":
                    RemoveContactMenu(contact);
                    running = false;
                    break;
                case "q":
                    running = false;
                    Console.Clear();
                    Console.WriteLine("Going back to main menu. Press any key to continue");
                    Console.ReadKey();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option, press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public void RemoveContactMenu(IContact contact) 
    { 
        bool running = true;
        while (running)
        {
            Console.Clear() ;
            Console.WriteLine($"Are you sure you want to delete {contact.Name.FirstName} {contact.Name.LastName} from the list?\n");
            Console.WriteLine("Type 'Yes' if you wish to delete the contact.");
            Console.WriteLine("Or type 'No' if you want to keep the contact in the list.\n");
            Console.Write("Answer: ");
            string answer = Console.ReadLine()!.ToLower();

            switch (answer)
            {
                case "yes":
                    Console.Clear();
                    _contactService.RemoveContactFromList(contact);
                    Console.WriteLine("Deleting the contact and going back to main menu. \npress any key to continue");
                    running = false;
                    Console.ReadKey();
                    break;
                case "no":
                    Console.Clear();
                    Console.WriteLine("Keeping the contact and going back to main menu.\nPress any key to continue.");
                    running = false;
                    Console.ReadKey();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Your answer must be yes or no! Press any key to try again");
                    Console.ReadKey();
                    break;
            }

        }

    }

    public void EditContactMenu(IContact contact)
    {

    }
    public void SearchByPositionMenu()
    {
        bool running = true;
        while (running)
        {
            Console.Clear() ;
            Console.WriteLine("Enter the number in the list for the contact that you are searching for.");
            Console.Write("Position: ");

            if (int.TryParse(Console.ReadLine(), out int position))
            {
                var contacts = _contactService.GetContactsFromList();
                if (contacts.Count >= position)
                {
                    IContact contact = _contactService.GetContactFromListByPosition(position);
                    if (contact == null)
                    {
                        Console.Clear();
                        Console.WriteLine("The contact could not be found.");

                        Console.WriteLine("Would you like to go back to main menu? (Q)");
                        Console.WriteLine("Or try again? (press any other key)");
                        string option = Console.ReadLine()!.ToLower();
                        if (option == "q")
                        {
                            running = false;
                        }
                    }
                    else if (contact != null)
                    {
                        running = false;
                        ShowContactMenu(contact);

                    }
                }
                else
                {
                    Console.WriteLine("The list does not contain that number.");
                    Console.WriteLine("Press any key to try again, or Q to go back to main menu.");
                    string input = Console.ReadLine()!.ToLower();
                    if (input == "q")
                    {
                        running = false;
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Must be a number!");
                Console.WriteLine("Press any key to try again, or Q to go back to main menu.");
                string input = Console.ReadLine()!.ToLower();
                if (input == "q")
                { 
                    running = false;
                }
            }
        }
    }

    public void SearchForContactMenu() 
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Would you like to search for a specific contact with email or position number in the contacts list?");
            Console.WriteLine("1.    Email");
            Console.WriteLine("2.    Number");
            Console.WriteLine();
            Console.WriteLine("Q.    Go back to main menu.");
            Console.WriteLine();
            Console.Write("Option: ");
            string option = Console.ReadLine()!.ToLower();

            switch (option)
            {
                case "1":
                    
                    SearchByEmailMenu();
                    running = false;
                    break;
                case "2":
                    running = false;
                    SearchByPositionMenu();
                    break;
                case "q": Console.WriteLine("Going back to main menu.");
                    Console.ReadKey();
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }
    public void SearchByEmailMenu() 
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.Write("Contacts Email: ");
            string email = Console.ReadLine()!.ToLower();
            IContact contact = _contactService.GetContactFromListByEmail(email);
            if (contact == null)
            {
                Console.Clear();
                Console.WriteLine($"A contact with the specified email ({email}) could not be found.\n");
                
                Console.WriteLine("Would you like to go back to main menu? (Q)") ;
                Console.WriteLine("Or try again? (press any other key)");
                string option = Console.ReadLine()!.ToLower();
                if (option == "q")
                {
                    running = false;
                }
            }
            else if (contact != null)
            {
                running = false;
                ShowContactMenu(contact);

            }
        }
        

        
    }

    static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
}

// SearhbyPosition
// Edit
// Remove

