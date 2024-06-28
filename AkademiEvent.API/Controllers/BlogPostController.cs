using AkademiEvent.API.Models.DTO.blogpost;
using AkademiEvent.API.Models.ORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkademiEvent.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BlogPostController : ControllerBase
{
    private readonly AkademiEventContext _db;
    public BlogPostController(AkademiEventContext db)
    {
        _db = db;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        List<GetAllBlogPostsResponseDto> list = _db.BlogPosts.Where(x=>x.IsDeleted==false).Select(x=>new GetAllBlogPostsResponseDto
        {
            Id= x.Id,
            Title= x.Title,
            Content= x.Content,
        }).ToList();
        return Ok(list);
    }
    [HttpGet("{id}")]
    public IActionResult GetBlogPost(Guid id)
    {
        var blogPost = _db.BlogPosts.FirstOrDefault(x=>x.Id==id&&x.IsDeleted==false);
        if (blogPost==null)
        {
            return NotFound();
        }
        else
        {
            GetBlogPostResponseDto model = new GetBlogPostResponseDto();
            model.Id = blogPost.Id;
            model.Title = blogPost.Title;
            model.Content = blogPost.Content;
            return Ok(model);
        }
    }
    [HttpPost]
    public IActionResult Post(CreateBlogPostRequestDto model)
    {
        var entity = new BlogPost
        {
            Title = model.Title,
            Content = model.Content,
        };
        
        _db.BlogPosts.Add(entity);
        _db.SaveChanges();
        return Ok();
    }
    [HttpPut("{id}")]
    public IActionResult UpdateBlogPost(Guid id, UpdateBlogPostRequestDto model)
    {
        var entity = _db.BlogPosts.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
        if (entity == null)
        {
            return NotFound();
        }
        else
        {
            entity.Title = model.Title;
            entity.Content = model.Content;

            _db.BlogPosts.Update(entity);
            _db.SaveChanges();
        }

        return Ok(entity);
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteBlogPost(Guid id)
    {
        var entity=_db.BlogPosts.FirstOrDefault(x=>x.Id==id &&x.IsDeleted==false);
        if (entity==null)
        {
            return NotFound();
        }
        entity.IsDeleted = true;
        _db.SaveChanges();
        return Ok();
    }
}
