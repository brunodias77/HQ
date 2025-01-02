namespace HQ.Application.Dtos.Posts;

public class PostDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string UserName { get; set; }
    public DateTime PublicationDate  { get; set; }
    public String Category { get; set; }
}