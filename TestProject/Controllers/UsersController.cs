using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TestProject.Models;
using TestProject.Services;
using TestProject.ViewModels;

namespace TestProject.Controllers;

[Route("api/users/")]
public class UsersController : ControllerBase
{
	private readonly IUserService _UserService;
	private readonly IMapper _Mapper;
	public UsersController(IUserService userService,
		IMapper mapper)
	{
		_UserService = userService;
		_Mapper = mapper;
	}

	[HttpPost]
	public async Task<IActionResult> Add([FromBody] UserViewModel user)
	{
		try
		{
			await _UserService.CreateAsync(_Mapper.Map<User>(user));
			return Ok("");
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				error = ex.Message
			});
		}
	}

	[HttpGet("{id}")]
	public IActionResult Get([FromRoute] string id)
	{
		try
		{
			var result = _UserService.GetAsync(id);
			return OkOrBadRequest(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				error = ex.Message
			});
		}
	}
	[HttpGet("all")]
	public  IActionResult GetAll()
	{
		try
		{
			var result = _UserService.GetAllAsync();
			return OkOrBadRequest(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	[HttpPut("{index}")]
	public async Task<IActionResult> ChangeUser([FromRoute] string index, [FromBody] UserViewModel user)
	{
		try
		{
			var isUserChanged = await _UserService.UpdateAsync(index, _Mapper.Map<User>(user));
			return Ok(isUserChanged);
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	[HttpDelete("{index}")]
	public async Task<IActionResult> Delete([FromRoute] string index)
	{
		try
		{
			var deleted = await _UserService.DeleteAsync(index);
			return Ok(deleted);
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	private ObjectResult OkOrBadRequest(object result)
	{
		return result == null ? BadRequest(result) : Ok(result);
	}
}
