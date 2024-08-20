using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    public class IPAddressEntry
    {
        public string Address { get; }

        public IPAddressEntry(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("IP address cannot be null or whitespace.");

            Address = address;

            if (!IsValidAddress(address))
                throw new ArgumentException("Invalid IP address or CIDR notation.");
        }

        private bool IsValidAddress(string address)
        {
            return IPAddress.TryParse(address, out _) || IPNetwork.TryParse(address, out _);
        }

        public override string ToString()
        {
            return Address;
        }
    }
}
