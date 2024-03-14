using System;
using System.IO;
using NLog;

public class TicketManager
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    private string _filePath;

    public TicketManager(string filePath)
    {
        _filePath = filePath;
    }

    public void ReadDataFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                using (StreamReader sr = new StreamReader(_filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        Console.WriteLine(line);
                    }
                }
            }
            else
            {
                logger.Error("File does not exist.");
                Console.WriteLine("File does not exist.");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error reading the file.");
        }
    }

    public void CreateFileFromData(Ticket ticket)
    {
        try
        {
            using (StreamWriter sw = File.AppendText(_filePath))
            {
                sw.WriteLine(ticket.ToString());
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error writing to the file.");
        }
    }
}
