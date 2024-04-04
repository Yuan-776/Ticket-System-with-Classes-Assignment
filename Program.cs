using System;
using System.Collections.Generic;
using NLog;

class Program
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    static TicketManager ticketManager = new TicketManager();

    static void Main(string[] args)
    {
        logger.Info("Application started");
        string choice;

        do
        {
            Console.WriteLine("1) Read data from file.");
            Console.WriteLine("2) Create data.");
            Console.WriteLine("3) Search tickets.");

            Console.WriteLine("Enter any other key to exit.");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter the type of ticket to read (BugDefects, Enhancements, Tasks): ");
                    string readType = Console.ReadLine();
                    string filePath = readType switch
                    {
                        "BugDefects" => "BugDefect.csv",
                        "Enhancements" => "Enhancements.csv",
                        "Tasks" => "Tasks.csv",
                        _ => null
                    };
                    if (filePath != null) ticketManager.ReadDataFromFile(filePath);
                    else Console.WriteLine("Invalid ticket type.");
                    break;
                case "2":
                    Ticket ticket = CreateTicketFromUserInput();
                    if (ticket != null) ticketManager.CreateFileFromData(ticket);
                    break;
                case "3":
                    Console.WriteLine("Enter the type of information to search (status, priority, submitter): ");
                    string searchType = Console.ReadLine();
                    Console.WriteLine("Enter search term: ");
                    string searchTerm = Console.ReadLine();
                    ticketManager.SearchTickets(searchTerm, searchType);
                    break;

                default:
                    logger.Info("Application ended");
                    return;
            }
        } while (true);
    }

    static Ticket CreateTicketFromUserInput()
    {
        Console.WriteLine("Enter the type of ticket to create (BugDefect, Enhancement, Task): ");
        string type = Console.ReadLine().Trim();
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
        Console.Write("Watching (comma separated): ");
        List<string> watching = new List<string>(Console.ReadLine().Split(','));

        // Example for a BugDefectTicket. Extend this with actual data collection for other types.
        if (type.Equals("BugDefect", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write("Severity: ");
            string severity = Console.ReadLine();
            return new BugDefectTicket(ticketID, summary, status, priority, submitter, assigned, watching, severity);
        }
        else if (type.Equals("Enhancement", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write("Software: ");
            string software = Console.ReadLine();
            Console.Write("Cost: ");
            double cost = double.Parse(Console.ReadLine());
            Console.Write("Reason: ");
            string reason = Console.ReadLine();
            Console.Write("Estimate: ");
            string estimate = Console.ReadLine();
            return new EnhancementTicket(ticketID, summary, status, priority, submitter, assigned, watching, software, cost, reason, estimate);
        }
        else if (type.Equals("Task", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write("ProjectName: ");
            string projectName = Console.ReadLine();
            Console.Write("DueDate (yyyy-MM-dd): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());
            return new TaskTicket(ticketID, summary, status, priority, submitter, assigned, watching, projectName, dueDate);
        }
        else
        {
            Console.WriteLine("Invalid ticket type selected.");
            return null;
        }
    }
}
