using HQ.Domain.Entities;

namespace HQ.Application.Dtos.Posts.Responses;

public class ResponseGetAllPosts
{
    public IEnumerable<PostDTO> Posts { get; set; }    
}