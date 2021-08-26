using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Models;
using NETCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonRepository personRepository;
        public PersonsController(PersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }
        [HttpPost]
        public ActionResult Insert(Person person)
        {
            personRepository.Insert(person);
            return Ok();
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(personRepository.Get());
        }

        [HttpGet ("{NIK}")]
        public ActionResult Get(string NIK)
        {
            return Ok(personRepository.Get(NIK));
        }

        [HttpPut]
        public ActionResult Update(Person person)
        {
            return Ok(personRepository.Update(person));
        }

        [HttpDelete ("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            return Ok(personRepository.Delete(NIK));
        }
    }
}
