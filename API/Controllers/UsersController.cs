using API.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create(string? login, string? password, [FromBody] CreateUserDto request)
    {
        if (login == null || password == null)
            return BadRequest();

        var sender = await _userRepository.GetUserByLogin(login);

        if (sender == null)
            return NotFound();

        if (!sender.Admin)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var user = await _userRepository.GetUserByLogin(request.Login);
        if (user != null)
            return BadRequest("user with this login already exists");

        user = _mapper.Map<User>(request);
        user.CreatedBy = login;
        await _userRepository.Create(user);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(string? login, string? password)
    {
        if (login == null || password == null)
            return BadRequest();

        var user = await _userRepository.GetUserByLoginAndPassword(login, password);

        if (user == null)
            return NotFound();

        if (user.RevokedOn != null)
            return BadRequest("User revoked");

        return Ok(user);
    }

    [HttpPatch("UpdateUser")]
    public async Task<IActionResult> Update(string? login, string? password, string? userLogin,
        [FromBody] UpdateUserDto? request)
    {
        if (login == null || password == null || userLogin == null || request == null)
            return BadRequest();

        var sender = await _userRepository.GetUserByLoginAndPassword(login, password);
        var user = await _userRepository.GetUserByLogin(userLogin);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        if (sender == null)
            return NotFound("wrong login or password");

        if (user == null)
            return NotFound("user with that userLogin not found");

        if (sender.RevokedBy != null || (login != userLogin && !sender.Admin))
            return BadRequest("you are not authorized to use this method");

        user = _mapper.Map(request, user);
        user.ModifiedBy = login;

        await _userRepository.Update(user);

        return Ok();
    }

    [HttpPut("UpdateLogin")]
    public async Task<IActionResult> UpdateLogin(string? login, string? password, [FromBody] UpdateLoginDto? request)
    {
        if (login == null || password == null || request == null)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var sender = await _userRepository.GetUserByLoginAndPassword(login, password);
        var user = await _userRepository.GetUserByLogin(request.OldLogin);

        if (sender == null)
            return NotFound("wrong login or password");

        if (user == null)
            return NotFound("user with that userLogin not found");

        if (sender.RevokedBy != null || (login != user.Login && !sender.Admin))
            return BadRequest("you are not authorized to use this method");

        user = _mapper.Map(request, user);
        user.ModifiedBy = login;
        await _userRepository.Update(user);

        return Ok();
    }

    [HttpPut("UpdatePassword")]
    public async Task<IActionResult> UpdatePassword(string? login, string? password,
        [FromBody] UpdatePasswordDto? request)
    {
        if (login == null || password == null || request == null)
            return BadRequest();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var sender = await _userRepository.GetUserByLoginAndPassword(login, password);
        var user = await _userRepository.GetUserByLogin(request.Login);

        if (sender == null)
            return NotFound("wrong login or password");

        if (user == null)
            return NotFound("user with that userLogin not found");

        if (sender.RevokedBy != null || (login != user.Login && !sender.Admin))
            return BadRequest("you are not authorized to use this method");

        user = _mapper.Map(request, user);
        user.ModifiedBy = login;
        await _userRepository.Update(user);

        return Ok();
    }

    [HttpGet("GetActiveUsers")]
    public async Task<IActionResult> GetActiveUsers(string? login, string? password)
    {
        if (login == null || password == null)
            return BadRequest();

        var user = await _userRepository.GetUserByLoginAndPassword(login, password);
        if (user == null)
            return NotFound();
        if (!user.Admin)
            return BadRequest("you are not authorized to use this method");

        var result = await _userRepository.GetActiveUsers();

        return Ok(result);
    }

    [HttpGet("GetByLogin")]
    public async Task<IActionResult> GetUserByLogin(string? login, string? password, string? userLogin)
    {
        if (login == null || password == null || userLogin == null)
            return BadRequest();
        var sender = await _userRepository.GetUserByLoginAndPassword(login, password);
        var user = await _userRepository.GetUserByLogin(userLogin);
        if (sender == null || user == null)
            return NotFound();
        if (!sender.Admin)
            return BadRequest();

        var result = _mapper.Map<UserDto>(user);
        return Ok(result);
    }

    [HttpGet("GetUserOlderThan")]
    public async Task<IActionResult> GetUserOlderThan(string? login, string? password, int year)
    {
        if (login == null || password == null)
            return BadRequest();
        var sender = await _userRepository.GetUserByLoginAndPassword(login, password);
        if (sender == null)
            return NotFound();
        if (!sender.Admin)
            return BadRequest();
        var result = await _userRepository.GetUsersOlderThan(year);
        return Ok(result);
    }

    [HttpPut("SoftDelete")]
    public async Task<IActionResult> SoftDelete(string? login, string? password, string? userLogin)
    {
        if (login == null || password == null || userLogin == null)
            return BadRequest();

        var sender = await _userRepository.GetUserByLoginAndPassword(login, password);
        var user = await _userRepository.GetUserByLogin(userLogin);

        if (sender == null)
            return NotFound("wrong login or password");

        if (user == null)
            return NotFound("user with that userLogin not found");

        if (!sender.Admin)
            return BadRequest();

        user.RevokedOn = DateTime.Now;
        user.RevokedBy = login;
        await _userRepository.Update(user);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string? login, string? password, string? userLogin)
    {
        if (login == null || password == null || userLogin == null)
            return BadRequest();

        var sender = await _userRepository.GetUserByLoginAndPassword(login, password);
        var user = await _userRepository.GetUserByLogin(userLogin);

        if (sender == null)
            return NotFound("wrong login or password");

        if (user == null)
            return NotFound("user with that userLogin not found");

        if (!sender.Admin)
            return BadRequest();

        await _userRepository.Delete(user);

        return Ok();
    }

    [HttpPut("Recovery")]
    public async Task<IActionResult> Recovery(string? login, string? password, string? userLogin)
    {
        if (login == null || password == null || userLogin == null)
            return BadRequest();

        var sender = await _userRepository.GetUserByLoginAndPassword(login, password);
        var user = await _userRepository.GetUserByLogin(userLogin);

        if (sender == null)
            return NotFound("wrong login or password");

        if (user == null)
            return NotFound("user with that userLogin not found");

        if (!sender.Admin)
            return BadRequest();

        user.RevokedOn = null;
        user.RevokedBy = null;
        await _userRepository.Update(user);

        return Ok();
    }
}