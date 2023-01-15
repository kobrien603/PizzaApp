﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PizzaApp.Models;
using PizzaApp.Server.DAL;
using PizzaApp.Server.DAL.Models;
using PizzaApp.Server.Helpers;
using PizzaApp.Server.Models;
using PizzaApp.Server.Services;
using PizzaApp.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PizzaApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthUserService _authUserService;
        private readonly PizzaContext _context;

        public AuthController(IConfiguration configuration, AuthUserService authUserService, PizzaContext context)
        {
            _configuration = configuration;
            _authUserService = authUserService;
            _context = context;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetUsername()
        {
            return Ok(_authUserService.Username());
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
                    var dbUser = new User()
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

                    await repository.Users.InsertOrUpdate(dbUser);

                    // valid response
                    response.IsValid = true;
                    response.ResponseMessage = CreateToken(dbUser);
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

        /// <summary>
		/// create new user - store in db
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost("login")]
        public async Task<ValidResponse> Login([FromBody] LoginModel model)
        {
            ValidResponse response = new();

            try
            {
                // init repository
                using var repository = new PizzaRepository(_context);

                // get user
                var dbUser = await repository.Users.GetUserByEmail(model.Email);
                if (dbUser != null && PasswordHelper.ValidatePassword(model.Password, dbUser.Password))
                {
                    response.IsValid = true;
                    response.ResponseMessage = CreateToken(dbUser);
                }
                else
                {
                    response.IsValid = false;
                    response.ResponseMessage = "Login attempt failed. Please try again";
                }


            }
            catch (Exception ex)
            {
                // todo - log
                response.IsValid = false;
                response.ResponseMessage = "Error attempting to login. Please try again later";
            }

            return response;
        }

        /// <summary>
        /// build jwt token with user info stored
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtConfig:secret").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
