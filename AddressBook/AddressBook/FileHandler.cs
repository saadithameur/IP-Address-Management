using AddressBook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class FileHandler
{
    private readonly string filePath;

    public FileHandler(string filePath)
    {
        this.filePath = filePath;
    }

    public async Task SaveToFileAsync(IEnumerable<IPAddressEntry> ipAddresses)
    {
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var writer = new StreamWriter(stream))
            {
                foreach (var ip in ipAddresses)
                {
                    await writer.WriteLineAsync(ip.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving IP addresses to file: {ex.Message}");
        }
    }

    public async Task<IEnumerable<IPAddressEntry>> LoadFromFileAsync()
    {
        var ipAddresses = new List<IPAddressEntry>();

        try
        {
            if (File.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        ipAddresses.Add(new IPAddressEntry(line));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading IP addresses from file: {ex.Message}");
        }

        return ipAddresses;
    }
}