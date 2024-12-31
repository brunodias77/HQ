using HQ.Application.Dtos.Posts;
using HQ.Application.Dtos.Posts.Responses;
using HQ.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HQ.Api.Controllers;

public class PostController : BaseController
{
    [HttpPost("create/post")]
    [Consumes("multipart/form-data")]
    public IActionResult createPost([FromForm] RequestCreatePost request)
    {
        // Preciso salvar a url da foto do posto no banco de dados !!!
        
        request.PublishedAt ??= DateTime.UtcNow;
        if (request.Image == null || request.Image.Length == 0)
        {
            return BadRequest("Image is required.");
        }
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
        return Ok(new { Message = "Post created successfully!", FilePath = filePath });
    }

    [HttpGet]
    public IActionResult GetPost()
    {
        // Simulação de recuperação de dados (substitua pelo acesso ao banco ou serviço real)
        var post = new RequestCreatePost
        {
            Title = "Exemplo de Título",
            Content = "Exemplo de Conteúdo",
            PublishedAt = DateTime.UtcNow
        };

        // Nome do arquivo de imagem associado ao post (exemplo estático para teste)
        var imageName = "tampinhas.jpg";

        // Geração da URL completa para a imagem
        var imageUrl = Url.Content($"~/uploads/{imageName}");

        // Criar o objeto de resposta
        var response = new ResponseGetPost
        {
            Title = post.Title,
            Content = post.Content,
            DatePublishedAt = post.PublishedAt ?? DateTime.UtcNow,
            ImageUrl = imageUrl
        };

        return Ok(response);    }
}