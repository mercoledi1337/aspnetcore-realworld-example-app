using AutoMapper;
using Conduit.Features.Users.Application;
using Duende.IdentityServer.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;



namespace Conduit.Features.Users.UI
{
    [Route("/api")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        public UserController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("users")]
        public async Task<IActionResult> Register([FromBody] Register.RegisteringUser command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("users/login")]
        public async Task<IActionResult> Login([FromBody] Login.LoginUser command)
        {
            
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}

