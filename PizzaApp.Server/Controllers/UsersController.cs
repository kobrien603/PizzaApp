using Microsoft.AspNetCore.Mvc;
using PizzaApp.Models;
using PizzaApp.Server.DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaApp.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly PizzaContext _context;
		private readonly PizzaRepository repository;

		public UsersController(PizzaContext context)
		{
			_context = context;
			// todo - consider just using this in code instead of init here so we can use using and dispose properly
			repository = new(_context); 
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

		// POST api/<UserController>
		[HttpPost]
		public void Post([FromBody] CreateUserModel model)
		{
			// map to DAL object
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
