namespace SmartCraft.Core.Entities;

public class Interaction
{
    public int Id { get; set; }

    public int ContactId { get; set; }
    public virtual Contact Contact { get; set; } = null!;

    public string Type { get; set; }
    public string Notes { get; set; }
    public string Subject { get; set; }
    public string Outcome { get; set; }
    public string Location { get; set; }

    private DateTime? _creationDate = DateTime.UtcNow;

    /// <summary>
    /// timestamp of created date
    /// </summary>
    public DateTime? Created => _creationDate;
}