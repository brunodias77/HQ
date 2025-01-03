namespace HQ.Application.Dtos.Posts.Responses;

public class ResponseGetPost
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime DatePublishedAt { get; set; }
    public string ImageUrl { get; set; } // URL pública da imagem
}