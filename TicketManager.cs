using System;
using System.IO;
using NLog;

public class TicketManager
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    private string bugDefectsFilePath = "BugDefect.csv";
    private string enhancementsFilePath = "Enhancements.csv";
    private string tasksFilePath = "Tasks.csv";

    public void ReadDataFromFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
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
                logger.Error("File does not exist: " + filePath);
                Console.WriteLine("File does not exist.");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error reading the file: " + filePath);
        }
    }

    public void CreateFileFromData(Ticket ticket)
    {
        string filePath = DetermineFilePath(ticket);
        try
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(ticket.ToString());
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error writing to the file: " + filePath);
        }
    }

    private string DetermineFilePath(Ticket ticket)
    {
        if (ticket is BugDefectTicket) return bugDefectsFilePath;
        if (ticket is EnhancementTicket) return enhancementsFilePath;
        if (ticket is TaskTicket) return tasksFilePath;
        throw new ArgumentException("Unknown ticket type.");
    }
}
