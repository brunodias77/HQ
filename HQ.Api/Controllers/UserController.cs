using HQ.Application.Abstractions;
using HQ.Application.Dtos;
using HQ.Application.Dtos.Users.Requests;
using HQ.Application.Dtos.Users.Responses;
using HQ.Application.UseCases.Users.Register;
using HQ.Domain.Repositories;
using HQ.Domain.Security.PasswordEncripter;
using Microsoft.AspNetCore.Mvc;

namespace HQ.Api.Controllers;

public class UserController : BaseController
{
    public UserController(IUserRepository userRepository, IPasswordEncripter passwordEncripter)
    {
        _userRepository = userRepository;
        _passwordEncripter = passwordEncripter;
    }

    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncripter _passwordEncripter;

    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }

    [HttpPost("criar-conta")]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] IUseCase<RequestRegisterUserJson, ResponseRegisterUserJson> useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] IUseCase<RequestLoginUser, ResponseLoginUser> useCase,
        [FromBody] RequestLoginUser request)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }
}