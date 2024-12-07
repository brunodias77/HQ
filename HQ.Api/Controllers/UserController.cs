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
}