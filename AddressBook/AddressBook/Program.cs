using System;
using System.Threading.Tasks;

namespace AddressBook
{
    class Program
    {
        static async Task Main()
        {
            var ipAddressManager = new IPAddressManager();
            var fileHandler = new FileHandler("addresses.txt");
            var loadedAddresses = await fileHandler.LoadFromFileAsync();
            foreach (var address in loadedAddresses)
            {
                ipAddressManager.AddIPAddress(address, false);
            }

            while (true)
            {
                Console.WriteLine("1. Add IP Address");
                Console.WriteLine("2. Remove IP Address");
                Console.WriteLine("3. Search IP Address");
                Console.WriteLine("4. List IP Addresses");
                Console.WriteLine("5. Exit");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.Write("Enter IP Address (or CIDR): ");
                        try
                        {
                            ipAddressManager.AddIPAddress(new IPAddressEntry(Console.ReadLine()),true);
                            await fileHandler.SaveToFileAsync(ipAddressManager.GetAllIPAddresses());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case "2":
                        Console.Write("Enter IP Address to remove: ");
                        ipAddressManager.RemoveIPAddress(new IPAddressEntry(Console.ReadLine()));
                        await fileHandler.SaveToFileAsync(ipAddressManager.GetAllIPAddresses());
                        break;

                    case "3":
                        Console.Write("Enter IP Address to search: ");
                        bool found = ipAddressManager.SearchIPAddress(Console.ReadLine());
                        Console.WriteLine(found ? "IP Address found." : "IP Address not found.");
                        break;

                    case "4":
                        Console.WriteLine("Sort (yes/no): ");
                        bool sort = Console.ReadLine().ToLower() == "yes";
                        Console.WriteLine("Only IPv4 (yes/no): ");
                        bool onlyIPv4 = Console.ReadLine().ToLower() == "yes";
                        Console.WriteLine("Only IPv6 (yes/no): ");
                        bool onlyIPv6 = Console.ReadLine().ToLower() == "yes";

                        var listedAddresses = ipAddressManager.ListIPAddresses(sort, onlyIPv4, onlyIPv6);
                        Console.WriteLine("Stored IP Addresses:");
                        foreach (var ip in listedAddresses)
                        {
                            Console.WriteLine(ip);
                        }
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}