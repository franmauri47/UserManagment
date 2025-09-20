using Domain.Common;

namespace Domain.Entities;

public class Domicile : AuditableEntity
{
    public int UserId { get; set; }
    public required string Street { get; set; }
    public string? DirectionNumber { get; set; }
    public required string Province { get; set; }
    public required string City { get; set; }

    // Navegability properties
    public required User User { get; set; }
}