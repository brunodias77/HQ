using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace HQ.Application.Dtos.Posts;

public class RequestCreatePost
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime? PublishedAt { get; set; } 
}