using System.ComponentModel.DataAnnotations.Schema;

namespace AkademiEvent.API.Models.ORM;

public class Activity:BaseEntity
{
    public string Name { get; set; }=String.Empty;
    public string Description { get; set; }= String.Empty;
    public string Latitude { get; set; } = String.Empty;
    public string Longitude { get; set; } = String.Empty;
    public DateTime StartDate { get; set; }
    public Guid CategoryID { get; set; }
    [ForeignKey("CategoryID")]
    public Category Category{ get; set; }
    public Guid CityID { get; set; }
    [ForeignKey("CityID")]
    public City City { get; set; }  
}
