using AkademiEvent.API.Models.ORM;

namespace AkademiEvent.API.Models.DTO.activity;

public class CreateActivityRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public Guid CategoryID { get; set; }
    public List<IFormFile> Images { get; set; }

}
