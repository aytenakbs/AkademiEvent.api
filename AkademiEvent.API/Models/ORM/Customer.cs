namespace AkademiEvent.API.Models.ORM;

public class Customer:BaseEntity
{
    public string NameSurname { get; set; }
    public string Email {  get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Birthday { get; set; }

}
