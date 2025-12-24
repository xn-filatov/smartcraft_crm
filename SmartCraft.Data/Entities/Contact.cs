namespace SmartCraft.Core.Entities;

public class Contact
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }
    public Company? Company { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string JobTitle { get; set; }
    public DateTime? LastContacted { get; set; }

    public IEnumerable<Interaction> Interactions { get; set; } = new List<Interaction>();

    public string FullName => $"{FirstName} {LastName}";

    private DateTime? _creationDate = DateTime.UtcNow;

    /// <summary>
    /// timestamp of created date
    /// </summary>
    public DateTime? Created => _creationDate;
}