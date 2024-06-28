using AkademiEvent.API.Models.DTO.activity;
using AkademiEvent.API.Models.ORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkademiEvent.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ActivityController : ControllerBase
{
    private readonly AkademiEventContext _db;
    public ActivityController(AkademiEventContext db)
    {
        _db = db;
    }
    [HttpPost]
    public IActionResult Post(CreateActivityRequestDto model)
    {
        // save image to server
        List<string> imagePaths = new List<string>();
        foreach (var image in model.Images)
        {

            //extension control
            if (image.ContentType != "image/jpeg" && image.ContentType != "image/jpg" && image.ContentType != "image/png")
            {
                return BadRequest("Lütfen sadece jpeg,jpg ve png formatında resim yükleyiniz.");
            }

            var guidName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", guidName);


            using (var stream = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            imagePaths.Add(guidName);
        }


        // save activity
        Activity activity = new Activity
        {
            Name = model.Name,
            Description = model.Description,
            StartDate = model.StartDate,
            CategoryID = model.CategoryID,
        };

        _db.Activities.Add(activity);
        _db.SaveChanges();


        // save images
        foreach (var item in imagePaths)
        {
            ActivityImage activityImage = new ActivityImage();
            activityImage.ActivityID = activity.Id;
            activityImage.ImagePath = item;

            _db.ActivityImages.Add(activityImage);
        }

        _db.SaveChanges();

        return Ok();
    }
    [HttpGet]
    public IActionResult GetAllActivity()
    {
        List<GetAllActivitiesResponseDto> model = _db.Activities.Where(x => x.IsDeleted == false).Select(x => new GetAllActivitiesResponseDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            StartDate = x.StartDate,
            CategoryName = x.Category.Name,
            Latitude = x.Latitude,
            Longitude = x.Longitude
        }).ToList();
        foreach (var item in model)
        {
            item.Images = _db.ActivityImages.Where(x => x.ActivityID == item.Id && x.IsDeleted == false).Select(x => x.ImagePath).ToList();
        }
        return Ok(model);
    }
    [HttpGet("{id}")]
    public IActionResult GetActivity(Guid id)
    {
        var activity = _db.Activities.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
        if (activity == null)
        {
            return NotFound();
        }
        else
        {
            GetActivityResponseDto value = new GetActivityResponseDto();

            value.Id = activity.Id;
            value.Name = activity.Name;
            value.Description = activity.Description;
            value.StartDate = activity.StartDate;
            value.CategoryName = activity.Category.Name;
            value.Images = _db.ActivityImages.Where(x => x.ActivityID == id && x.IsDeleted == false).Select(x => x.ImagePath).ToList();
            return Ok(value);
        }

    }
    [HttpDelete("{id}")]
    public IActionResult DeleteActivity(Guid id)
    {
        var value = _db.Activities.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
        if (value == null) 
        { 
            return NotFound(); 
        }
        else
        {
            value.DeleteDate = DateTime.Now;
            value.IsDeleted = true;
            _db.SaveChanges();
            return Ok();
        }


    }


}
