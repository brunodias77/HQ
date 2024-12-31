using Microsoft.AspNetCore.Http;

namespace HQ.Application.Dtos.Posts;

public class RequestCreatePostFormData : RequestCreatePost
{
    public IFormFile? Image { get; set; }
}