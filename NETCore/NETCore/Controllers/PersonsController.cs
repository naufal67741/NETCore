using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.Base;
using NETCore.Context;
using NETCore.Models;
using NETCore.Repository.Data;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    /*[Authorize(Roles = "Manager")]*/
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : BaseController<Person, PersonRepository, string>
    {
        private readonly PersonRepository repository;
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public PersonsController(PersonRepository repository, MyContext myContext, IConfiguration _configuration) : base(repository)
        {
            this.myContext = myContext;
            this.repository = repository;
            this._configuration = _configuration;
        }
        //api/persons/getperson
        
        /*[EnableCors("AllowOrigin")]*/
        [HttpGet("GetPerson")]
        public ActionResult GetPerson()
        {
            var getPerson = repository.GetPersonVMs();
            if (getPerson != null)
            {
                var get = Ok(new
                {
                    status = HttpStatusCode.OK,
                    result = getPerson,
                    message = "Success"
                });
                return get;
            }
            else
            {
                var get = NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    result = getPerson,
                    message = "Data Empty"
                });
                return get;
            }
        }

        [HttpGet("GetPerson/{NIK}")]
        public ActionResult GetPerson(string NIK)
        {
            var getPerson = repository.GetPersonVMs(NIK);
            if (getPerson != null)
            {
                var get = Ok(new
                {
                    status = HttpStatusCode.OK,
                    result = getPerson,
                    message = "Success"
                });
                return get;
            }
            else
            {
                var get = NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    result = getPerson,
                    message = "Data Empty"
                });
                return get;
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public ActionResult Insert(PersonVM personVM)
        {
            try
            {
                int output = 0;
                if (ModelState.IsValid)
                {

                    output = repository.Insert(personVM);
                }
                else
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Check Format",
                    });
                }
                if (output == 100)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Duplicate email",
                    });
                }
                else if (output == 200)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Duplicate NIK",
                    });
                }
                else if (output == 300)
                {
                    return BadRequest(new
                    {
                        status = HttpStatusCode.BadRequest,
                        message = "Duplicate Email",
                    });
                }
                return Ok(new
                {
                    statusCode = StatusCode(200),
                    status = HttpStatusCode.OK,
                    message = "Success"
                });
            }
            catch
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Error duplicate data",
                });
            }
        }
        
        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult Login(LoginVM loginVM)
        {
            int output = repository.Login(loginVM);
            if (output == 100)
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Email not available",
                });
            }
            else if (output == 200)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Wrong Password",
                });
            }
            else
            {
                var data = (from p in myContext.Persons
                            join a in myContext.Accounts on
                            p.NIK equals a.NIK
                            join ar in myContext.AccountRoles on
                            a.NIK equals ar.NIK
                            join r in myContext.Roles on
                            ar.RoleId equals r.RoleId
                            where p.Email == $"{ loginVM.Email}"
                            select new PayloadVM
                            {
                                NIK = p.NIK,
                                Email = p.Email,
                                Role = r.RoleName
                            }).ToList();
                var asd = data;
                var claim = new List<Claim>();

                claim.Add(new Claim("NIK", data[0].NIK));
                claim.Add(new Claim("Email", data[0].Email));
                foreach (var d in data)
                {
                    claim.Add(new Claim("roles", d.Role));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                                 _configuration["Jwt:Audience"],
                                                 claim, expires: DateTime.UtcNow.AddDays(1),
                                                 signingCredentials: signIn);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    status = HttpStatusCode.OK,
                    message = "Login Success !"
                });
            }

        }
    }
}
