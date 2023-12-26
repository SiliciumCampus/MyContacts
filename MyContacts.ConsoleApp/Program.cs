using MyContacts.ConsoleApp.Services;

namespace MyContacts.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var menuService = new MenuService();
            Console.Clear();
            menuService.ShowMainMenu();
            Console.ReadKey();
            Console.Clear();
            //menuService.AddContactMenu();

            Console.Clear();
            //menuService.ShowAllContactsMenu();

            Console.ReadKey();
        }
    }
}
