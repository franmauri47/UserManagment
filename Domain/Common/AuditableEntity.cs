namespace Domain.Common;

public class AuditableEntity : BaseEntity
{
    public DateTime CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
