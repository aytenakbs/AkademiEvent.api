using AkademiEvent.API.Models.DTO.category;
using AkademiEvent.API.Models.ORM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkademiEvent.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly AkademiEventContext _db;
    public CategoryController(AkademiEventContext db)
    {
        _db = db;
    }
    
    [HttpGet]
    public IActionResult GetAllCategories()
    {
        List<GetAllCategoriesResponseDto> categories = _db.Categories.Where(x=>x.IsDeleted==false).Select(x => new GetAllCategoriesResponseDto
        {
            Id = x.Id,
            Name = x.Name,
            AddDate = x.AddDate,
        }).OrderBy(q => q.AddDate).ToList();
        return Ok(categories);
    }
    [HttpGet("{id}")]
    public IActionResult GetCategory(Guid id)
    {
        var entity=_db.Categories.FirstOrDefault(x => x.Id == id);
        if (entity==null)
        {
            return NotFound();
        }
        else
        {
            GetCategoryResponseDto model=new GetCategoryResponseDto
            {
                Id=entity.Id,
                Name=entity.Name,
                AddDate=entity.AddDate,
            };
            return Ok(model);
        }
    }
    [Authorize]
    [HttpPost]
    public IActionResult CreateCategory(CreateCategoryRequestDto model)
    {
        var entity =new Category 
        { 
        Name=model.Name 
        };
        _db.Categories.Add(entity);
        _db.SaveChanges();
        return Ok();
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(Guid id)
    {
        var entity=_db.Categories.FirstOrDefault(x => x.Id == id);
        if (entity == null)
        {
            return NotFound();

        }
        else
        {
            entity.IsDeleted = true;
            entity.DeleteDate = DateTime.Now;
            _db.SaveChanges();
            return Ok();
        }
    }
    [HttpPut("{id}")]
    public IActionResult UpdateCategory(Guid id,UpdateRequestCategoryDto model)
    {
        var category = _db.Categories.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
        if (category == null)
        {
            return NotFound();
        }
        else
        {
            category.Name = model.Name;
            category.UpdateDate = DateTime.Now;
            _db.SaveChanges();
            return Ok();
        }
    }

}
