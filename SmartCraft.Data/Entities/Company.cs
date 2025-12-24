using SmartCraft.Core.Entities.Enums;

namespace SmartCraft.Core.Entities;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Industry { get; set; }
    public string Website { get; set; }
    public CompanySize Size { get; set; }
    public virtual IEnumerable<Contact> Contacts { get; set; } = new List<Contact>();

    private DateTime? _creationDate = DateTime.UtcNow;

    /// <summary>
    /// timestamp of created date
    /// </summary>
    public DateTime? Created => _creationDate;
}