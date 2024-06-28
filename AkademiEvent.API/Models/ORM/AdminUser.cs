namespace AkademiEvent.API.Models.ORM;

public class AdminUser:BaseEntity
{
    public string Email { get; set; }     
    public string Password { get; set; }     
}
