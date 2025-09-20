namespace Domain.Common;

public class AuditableEntity
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
