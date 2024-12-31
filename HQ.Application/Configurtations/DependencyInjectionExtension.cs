using HQ.Application.Abstractions;
using HQ.Application.Dtos.Donations.Requests;
using HQ.Application.Dtos.Donations.Responses;
using HQ.Application.Dtos.Users.Requests;
using HQ.Application.Dtos.Users.Responses;
using HQ.Application.Services;
using HQ.Application.UseCases.Donations.Generate.Pix;
using HQ.Application.UseCases.Users.Login;
using HQ.Application.UseCases.Users.Register;
using HQ.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HQ.Application.Configurtations;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
        AddAutoMapper(services, configuration);
        AddServices(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IUseCase<RequestRegisterUserJson, ResponseRegisterUserJson>, RegisterUserUseCase>();
        services.AddScoped<IUseCase<RequestGeneratePixDonation, ResponseGeneratePixDonation>, GeneratePixDonationUseCase>();
        services.AddScoped<IUseCase<RequestLoginUser, ResponseLoginUser>, LoginUserUseCase>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IGenerateQrCodeService, GenerateQrCodeService>();
        
    }
    
    private static void AddAutoMapper(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(DependencyInjectionExtension).Assembly);

        // var sqids = new SqidsEncoder<long>(new()
        // {
        //     MinLength = 10,
        //     Alphabet = configuration.GetValue<string>("achIugtW19s7vA4ldomHjULNFYbery0EpTMxkBiQ6qJ2SKXZG35Cz8RDfnPOVw")!
        // });

        // Se for usar o o sqids com autoMapper
        // services.AddScoped(option => new AutoMapper.MapperConfiguration(autoMapperOptions =>
        // {
        //     sqids = option.GetService<SqidsEncoder<long>>();
        //     autoMapperOptions.AddProfile(new AutoMapping(sqids));
        // }));

        // services.AddScoped(option =>
        //     new AutoMapper.MapperConfiguration(opt => { opt.AddProfile(new AutoMapping()); }).CreateMapper());
    }


}