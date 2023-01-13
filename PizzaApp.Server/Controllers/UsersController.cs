using Microsoft.AspNetCore.Mvc;
using MudBlazor.Extensions;
using PizzaApp.Models;
using PizzaApp.Server.DAL;
using PizzaApp.Server.DAL.Models;
using PizzaApp.Server.Helpers;
using PizzaApp.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaApp.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly PizzaContext _context;

		public UsersController(PizzaContext context)
		{
			_context = context;
		}

		// GET: api/<UserController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		/// <summary>
		/// create new user - store in db
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost("create-user")]
		public async Task<ValidResponse> CreateUser([FromBody] CreateUserModel model)
		{
			ValidResponse response = new();

			try
			{
				// init repository
				using var repository = new PizzaRepository(_context);

				// check if email is already in use
				if (await repository.Users.EmailAlreadyRegistered(model.Email))
				{
					response.IsValid = false;
					response.ResponseMessage = "Email has already been registered. Please try again";
				}
				else
				{
					// create user
					var user = new User()
					{
						ID = 0,
						CreatedDate = DateTime.Now,
						DateOfBirth = model.DateOfBirth.HasValue ? new DateOnly(model.DateOfBirth.Value.Year, model.DateOfBirth.Value.Month, model.DateOfBirth.Value.Day) : null,
						Email = model.Email,
						FirstName = model.FirstName,
						LastName = model.LastName,
						ModifiedDate = DateTime.Now,
						Password = PasswordHelper.CreateHashPassword(model.Password),
						PhoneNumber = model.PhoneNumber,
						ProfilePicture = model.ProfilePicture
					};

                    await repository.Users.InsertOrUpdate(user);

                    // valid response
                    response = new CookieHelper().CreateCookie(user.ID);
                }
			}
			catch (Exception e)
			{
				// todo - log
				response.IsValid = false;
				response.ResponseMessage = "Error creating user. Please try again";
			}

			return response;
		}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
