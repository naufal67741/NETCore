/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.Context;
using NETCore.Models;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly MyContext myContext;

        public TokenController(IConfiguration config, MyContext myContext)
        {
            _configuration = config;
            this.myContext = myContext;
        }

        [HttpPost]
        public ActionResult Post(LoginVM loginVM)
        {
            *//*var account = myContext.Accounts.Where(e => e.NIK == person.NIK).FirstOrDefault();*//*
            if (loginVM != null && loginVM.Email != null && loginVM.Password != null)
            {
                var user = GetUser(loginVM.Email, loginVM.Password);

                if (user != null)
                {
                    var person = myContext.Persons.Where(e => e.Email == loginVM.Email).FirstOrDefault();
                    var account = myContext.Accounts.Where(e => e.NIK == person.NIK).FirstOrDefault();
                    
                    //alat penampung untuk selanjutnya digunakan untuk isi List RoleId
                    List<AccountRole> accountRole = (from a in myContext.AccountRoles
                              where a.NIK == account.NIK
                              select new AccountRole { NIK = a.NIK}).ToList();
                    //tampung semua RoleId
                    List<int> roleId = new List<int>();
                    foreach (var ar in accountRole)
                    {
                        roleId.Add(ar.RoleId);
                    }
                    //Query untuk ambil RoleName berdasarkan list dari RoleId
                    var sql = $"SELECT RoleName From Roles Where RoleId in {roleId}";

                    List<Role> roleName = myContext.Roles.FromSqlRaw(sql).ToList();
                    //Ambil string roleNamenya aja, kita split dari Role
                    List<string> roleNames = new List<string>();
                    foreach(var rm in roleName)
                    {
                        roleNames.Add(rm.RoleName.ToString());
                    }
                    var data = (from p in myContext.Persons
                                join a in myContext.Accounts on
                                p.NIK equals a.NIK
                                join ar in myContext.AccountRoles on
                                a.NIK equals ar.NIK
                                join r in myContext.Roles on
                                ar.RoleId equals r.RoleId
                                select new PayloadVM { 
                                    NIK = p.NIK,
                                    Email = p.Email,
                                    Roles = roleNames
                                }).ToList();
                    //create claims details based on the user information
                    *//*var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("UserName", user.UserName),
                    new Claim("Email", user.Email)*//*
                    var claims = new List<Claim>();
                    foreach(var item in data)
                    {
                        claims.Add(new Claim("NIK", item.NIK));
                        claims.Add(new Claim("Email", item.Email));
                        *//*claims.Add(new Claim("Roles", item.Roles));*//*
                    }
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
*/