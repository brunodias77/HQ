using HQ.Application.Dtos.Users.Requests;
using HQ.Application.Dtos.Users.Responses;

namespace HQ.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
}