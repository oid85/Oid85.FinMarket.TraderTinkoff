using System.ComponentModel.DataAnnotations;

namespace Oid85.FinMarket.TraderTinkoff.Infrastructure.Database.Entities.Base;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}