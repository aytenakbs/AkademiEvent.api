using System.ComponentModel.DataAnnotations.Schema;

namespace AkademiEvent.API.Models.ORM;

public class Ticket : BaseEntity
{
    public Guid CategoryID { get; set; }
    [ForeignKey("CategoryID")]
    public Category Category { get; set; }
    public Guid CustomerID { get; set; }
    [ForeignKey("CustomerID")]
    public Customer Customer { get; set; }
    public Guid ActivityID { get; set; }
    [ForeignKey("ActivityID")]
    public Activity Activity { get; set; }
}
