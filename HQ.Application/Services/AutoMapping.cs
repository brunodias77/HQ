using AutoMapper;
using HQ.Application.Dtos.Users.Requests;
using HQ.Domain.Entities;

namespace HQ.Application.Services;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToDomain();
        DomainToResponse();
    }

    private void RequestToDomain()
    {
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(user => user.Password, opt => opt.Ignore()); // Ignora o mapeamento da propriedade Password
    }

    private void DomainToResponse()
    {
    }
}