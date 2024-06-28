using System.ComponentModel.DataAnnotations.Schema;

namespace AkademiEvent.API.Models.ORM;

public class ActivityImage:BaseEntity
{
    public string ImagePath { get; set; }
    public Guid ActivityID { get; set; }
    [ForeignKey("ActivityID")]
    public Activity Activity { get; set; }
}
