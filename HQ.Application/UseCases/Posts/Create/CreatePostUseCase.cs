using HQ.Application.Abstractions;
using HQ.Application.Dtos.Posts;
using HQ.Application.Dtos.Posts.Responses;
using HQ.Domain.Entities;
using HQ.Domain.Repositories;
using HQ.Domain.Services;

namespace HQ.Application.UseCases.Posts.Create;

public class CreatePostUseCase : IUseCase<RequestCreatePost, ResponseCreatePost>
{
    public CreatePostUseCase(IGetLoggedUser getLoggedUser, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IPostRespository postRespository)
    {
        _getLoggedUser = getLoggedUser;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _postRespository = postRespository;
    }

    private readonly IGetLoggedUser _getLoggedUser;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPostRespository _postRespository;
    
    public async Task<ResponseCreatePost> Execute(RequestCreatePost request)
    {
        request.PublishedAt ??= DateTime.UtcNow;
        var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        if (!Directory.Exists(uploadsDirectory))
        {
            Directory.CreateDirectory(uploadsDirectory);
        }
        var filePath = Path.Combine(uploadsDirectory, request.Image.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            request.Image.CopyTo(stream);
        }
        var user = await _getLoggedUser.User();
        var category = await _categoryRepository.GetByNameAsync(request.Category);
        var post = new Post
        {
            Title = request.Title,
            Content = request.Content,
            PublishedAt = DateTime.UtcNow,
            ImageUrl = $"https://localhost:7191/Uploads/{request.Image.FileName}",
            UserId = user.Id, 
            CategoryId = category.Id 
        };
        await _postRespository.AddAsync(post);
        await _unitOfWork.Commit();
        var response = new ResponseCreatePost()
        {
            Message = "Post criado com sucesso!",
        };
        return response;
    }
}