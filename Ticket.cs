using System;
using System.Collections.Generic;

public abstract class Ticket
{
    public string TicketID { get; set; }
    public string Summary { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
    public string Submitter { get; set; }
    public string Assigned { get; set; }
    public List<string> Watching { get; set; } = new List<string>();

    protected Ticket(string ticketID, string summary, string status, string priority, string submitter, string assigned, List<string> watching)
    {
        TicketID = ticketID;
        Summary = summary;
        Status = status;
        Priority = priority;
        Submitter = submitter;
        Assigned = assigned;
        Watching = watching;
    }

    public abstract override string ToString();
}

public class BugDefectTicket : Ticket
{
    public string Severity { get; set; }

    public BugDefectTicket(string ticketID, string summary, string status, string priority, string submitter, string assigned, List<string> watching, string severity)
        : base(ticketID, summary, status, priority, submitter, assigned, watching)
    {
        Severity = severity;
    }

    public override string ToString() => $"{TicketID},{Summary},{Status},{Priority},{Submitter},{Assigned},{string.Join("|", Watching)},{Severity}";
}

public class EnhancementTicket : Ticket
{
    public string Software { get; set; }
    public double Cost { get; set; }
    public string Reason { get; set; }
    public string Estimate { get; set; }

    public EnhancementTicket(string ticketID, string summary, string status, string priority, string submitter, string assigned, List<string> watching, string software, double cost, string reason, string estimate)
        : base(ticketID, summary, status, priority, submitter, assigned, watching)
    {
        Software = software;
        Cost = cost;
        Reason = reason;
        Estimate = estimate;
    }

    public override string ToString() => $"{TicketID},{Summary},{Status},{Priority},{Submitter},{Assigned},{string.Join("|", Watching)},{Software},{Cost},{Reason},{Estimate}";
}

public class TaskTicket : Ticket
{
    public string ProjectName { get; set; }
    public DateTime DueDate { get; set; }

    public TaskTicket(string ticketID, string summary, string status, string priority, string submitter, string assigned, List<string> watching, string projectName, DateTime dueDate)
        : base(ticketID, summary, status, priority, submitter, assigned, watching)
    {
        ProjectName = projectName;
        DueDate = dueDate;
    }

    public override string ToString() => $"{TicketID},{Summary},{Status},{Priority},{Submitter},{Assigned},{string.Join("|", Watching)},{ProjectName},{DueDate:yyyy-MM-dd}";
}
