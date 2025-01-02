using HQ.Application.Abstractions;
using HQ.Application.Dtos.Posts;
using HQ.Application.Dtos.Posts.Responses;
using HQ.Domain.Repositories;
using HQ.Domain.Services;

namespace HQ.Application.UseCases.Posts.Get_All_Post;

public class GetAllPostsUseCase : IUseCase<RequestGetAllPosts, ResponseGetAllPosts>
{
    public GetAllPostsUseCase(IPostRespository postRepository)
    {
        _postRepository = postRepository;
    }

    private readonly IPostRespository _postRepository;
    
    public async Task<ResponseGetAllPosts> Execute(RequestGetAllPosts request)
    {
        var posts = await _postRepository.GetAllPostsWithUsersAndCategories();
        var listPosts = posts?.Select(post => new PostDTO
        {
            Title = post.Title,
            Content = post.Content,
            UserName = post.User.Name,
            Category = post.Category.Name,
            PublicationDate = post.PublishedAt
        }).ToList();
        
        var response = new ResponseGetAllPosts()
        {
            Posts = listPosts ?? Enumerable.Empty<PostDTO>(),
        };
        return response;
    }
}