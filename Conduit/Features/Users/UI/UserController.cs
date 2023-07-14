using AutoMapper;
using Conduit.Features.Users.Infrastructure;
using Conduit.Features.Users;
using Duende.IdentityServer.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography;


namespace Conduit.Features.Users.UI
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        public UserController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var command = new RegisteringUser(request);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var command = new LoginUser(request);
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}

