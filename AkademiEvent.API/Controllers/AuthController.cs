using AkademiEvent.API.Models.Auth;
using AkademiEvent.API.Models.DTO.auth;
using AkademiEvent.API.Models.ORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkademiEvent.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AkademiEventContext _db;
    public AuthController(AkademiEventContext db)
    {
        _db = db;
    }
    [HttpPost]
    public IActionResult Post(LoginRequestDto model)
    {
        var user=_db.AdminUsers.FirstOrDefault(x=>x.Email==model.Email&&x.Password==model.Password&&x.IsDeleted==false);
        if (user==null)
        {
            return BadRequest("Invalid email or password");
        }
        else
        {
            var token = EtkinlikTokenHandler.CreateToken(user.Email);
            return Ok(token);
        }
        
    }
}
