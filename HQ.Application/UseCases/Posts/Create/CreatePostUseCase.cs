using HQ.Application.Abstractions;
using HQ.Application.Dtos.Posts;
using HQ.Application.Dtos.Posts.Responses;
using HQ.Domain.Entities;

namespace HQ.Application.UseCases.Posts.Create;

public class CreatePostUseCase : IUseCase<RequestCreatePost, ResponseCreatePost>
{
    public Task<ResponseCreatePost> Execute(RequestCreatePost request)
    {
        // Salvar a imagem na pasta Uploads
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

        // Criar uma Instancia do objeto Post

        var newPost = new Post();
        // Salvar no banco de dados
        throw new NotImplementedException();
    }
}