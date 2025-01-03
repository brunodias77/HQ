using HQ.Application.Dtos.Tokens;

namespace HQ.Application.Dtos.Users.Responses;

public class ResponseRegisterUserJson
{
    public string Name { get; set; } = string.Empty;
    public ResponseTokenJson Token { get; set; } = default!;
}