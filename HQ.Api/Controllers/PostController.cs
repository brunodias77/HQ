using HQ.Api.Attributes;
using HQ.Application.Abstractions;
using HQ.Application.Dtos.Posts;
using HQ.Application.Dtos.Posts.Responses;
using HQ.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HQ.Api.Controllers;

public class PostController : BaseController
{
    [HttpPost("new-post")]
    [Consumes("multipart/form-data")]
    [AuthenticatedUser]
    public async Task<IActionResult> createPost([FromServices] IUseCase<RequestCreatePost, ResponseCreatePost> useCase, [FromForm] RequestCreatePost request)
    {
        var result = await useCase.Execute(request);  
        return Ok(result);
    }

    [HttpGet("all-posts")]
    public async Task<IActionResult> GetAllPosts([FromServices] IUseCase<RequestGetAllPosts, ResponseGetAllPosts> useCase)
    {
        var result = await useCase.Execute(new RequestGetAllPosts());
        return Ok(result);
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

        return Ok(response);
    }
}