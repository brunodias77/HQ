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

        var post = new Post
        {
            Id = Guid.NewGuid(),
            Title = "Título do Post",
            Content = "Conteúdo do Post",
            PublishedAt = DateTime.UtcNow,
            ImageUrl = "https://example.com/image.jpg",
            UserId = Guid.NewGuid(), // ID do usuário associado
            CategoryId = Guid.NewGuid(), // ID da categoria associada
            User = new User
            {
                Id = Guid.NewGuid(),
                Name = "Usuário Exemplo"
            },
            Category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Categoria Exemplo"
            },
            Comments = new List<Comment>
            {
                new Comment
                {
                    Id = Guid.NewGuid(),
                    Content = "Primeiro comentário",
                    UserID = Guid.NewGuid(),
                    CommentedAt = DateTime.UtcNow
                }
            },
            PostTags = new List<PostTag>
            {
                new PostTag
                {
                    PostID = Guid.NewGuid(),
                    TagID = Guid.NewGuid()
                }
            }
        };
        throw new NotImplementedException();
    }
}