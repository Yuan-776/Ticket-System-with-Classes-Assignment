using System;
using NLog;

class Program
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    static TicketManager ticketManager = new TicketManager("Tickets.csv");

    static void Main(string[] args)
    {
        logger.Info("Application started");
        string choice;

        do
        {
            Console.WriteLine("1) Read data from file.");
            Console.WriteLine("2) Create file from data.");
            Console.WriteLine("Enter any other key to exit.");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ticketManager.ReadDataFromFile();
                    break;
                case "2":
                    var ticket = CreateTicketFromUserInput();
                    if (ticket != null)
                    {
                        ticketManager.CreateFileFromData(ticket);
                    }
                    break;
                default:
                    logger.Info("Application ended");
                    return;
            }
        } while (true); // This will repeat until the user chooses to exit.
    }

    static Ticket CreateTicketFromUserInput()
    {
        Console.WriteLine("Enter new ticket details:");

        Console.Write("TicketID: ");
        string ticketID = Console.ReadLine();
        Console.Write("Summary: ");
        string summary = Console.ReadLine();
        Console.Write("Status: ");
        string status = Console.ReadLine();
        Console.Write("Priority: ");
        string priority = Console.ReadLine();
        Console.Write("Submitter: ");
        string submitter = Console.ReadLine();
        Console.Write("Assigned: ");
        string assigned = Console.ReadLine();
        Console.Write("Watching: ");
        string watching = Console.ReadLine();

        if (string.IsNullOrEmpty(ticketID) || string.IsNullOrEmpty(summary) || string.IsNullOrEmpty(status))
        {
            logger.Error("Ticket ID, Summary, and Status are required fields.");
            Console.WriteLine("Ticket ID, Summary, and Status are required fields.");
            return null;
        }

        return new Ticket(ticketID, summary, status, priority, submitter, assigned, watching);
    }
}
