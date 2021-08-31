using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Base;
using NETCore.Models;
using NETCore.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : BaseController<Person, PersonRepository, string>
    {
        private readonly PersonRepository repository;
        public PersonsController(PersonRepository repository): base(repository)
        {
            this.repository = repository;
        }

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

    }
}
