using AddressBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

public class IPAddressManager
{
    private readonly HashSet<IPAddressEntry> ipAddresses = new HashSet<IPAddressEntry>();
    private readonly HashSet<IPAddressEntry> ipv4Addresses = new HashSet<IPAddressEntry>();
    private readonly HashSet<IPAddressEntry> ipv6Addresses = new HashSet<IPAddressEntry>();

    public void AddIPAddress(IPAddressEntry ipAddress, bool showMessage)
    {
        if (ipAddresses.Add(ipAddress))
        {
            if (IPAddress.TryParse(ipAddress.Address, out IPAddress ip))
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    ipv4Addresses.Add(ipAddress);
                else
                    ipv6Addresses.Add(ipAddress);
            }
            if(showMessage)
                Console.WriteLine("IP Address added successfully.");
        }
        else
        {
            Console.WriteLine("IP Address already exists.");
        }
    }

    public void RemoveIPAddress(IPAddressEntry ipAddress)
    {
        if (ipAddresses.Remove(ipAddress))
        {
            ipv4Addresses.Remove(ipAddress);
            ipv6Addresses.Remove(ipAddress);
            Console.WriteLine("IP Address removed successfully.");
        }
        else
        {
            Console.WriteLine("IP Address not found.");
        }
    }

    public bool SearchIPAddress(string ipAddress)
    {
        return ipAddresses.Any(ip => ip.Address == ipAddress || MatchesCIDR(ip.Address, ipAddress));
    }

    public IEnumerable<IPAddressEntry> ListIPAddresses(bool sorted = false, bool onlyIPv4 = false, bool onlyIPv6 = false)
    {
        IEnumerable<IPAddressEntry> result = ipAddresses;

        if (onlyIPv4)
        {
            result = ipv4Addresses;
        }
        else if (onlyIPv6)
        {
            result = ipv6Addresses;
        }

        return sorted ? result.OrderBy(ip => ip.Address) : result;
    }

    private bool MatchesCIDR(string cidr, string ipAddress)
    {
        return IPNetwork.TryParse(cidr, out IPNetwork network) && network.Contains(IPAddress.Parse(ipAddress));
    }

    public IEnumerable<IPAddressEntry> GetAllIPAddresses() => ipAddresses;
}