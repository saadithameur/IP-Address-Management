# IP-Address-Management

## Overview
A .NET 8.0 console application to manage IP addresses. Main functions are checking formats,adding, removing, searching, and listing IP addresses with storage in a text file.

## prerequisites
- .NET 8.0 SDK
- NuGet package: `System.Net.IPNetwork` (for checking CIDR support)

## Setup and Run
1. **Clone the Repository**
   ```bash
   git clone https://github.com/saadithameur/IP-Address-Management.git
2. **Build the Application**
   ```bash
   dotnet build
4. **Run the Application**
   ```bash
   dotnet run
   
## Usage
1. **Add IP Address**: Add IPv4, IPv6, or CIDR block.
2. **Remove IP Address**: Remove a specific address.
3. **Search IP Address**: Search for a specific address
4. **List IP Addresses**: View, sort, and filter IP addresses.
5. **Exit**: Close the application.

## Design

### Classes:
- `IPAddressEntry`
- `IPAddressManager`
- `FileHandler`

### Data Storage:
IP addresses are stored in `addresses.txt`.

### Optimizations:
Uses `HashSet` for O(1) operations, asynchronous file I/O.

## Assumptions

- Unique IP addresses.
- Standard format for IPv4/IPv6.

