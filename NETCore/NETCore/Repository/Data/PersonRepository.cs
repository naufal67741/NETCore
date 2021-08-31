using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Models;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository.Data
{
    public class PersonRepository : GeneralRepository<MyContext, Person, string>
    {
        private readonly MyContext myContext;
        public PersonRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public IEnumerable<PersonVM> GetPersonVMs()
        {
            var getPersonVMs = (from p in myContext.Persons
                                join a in myContext.Accounts on
                                p.NIK equals a.NIK
                                join prf in myContext.Profilings on
                                a.NIK equals prf.NIK
                                join e in myContext.Educations on
                                prf.EducationId equals e.EducationId
                                select new PersonVM
                                {
                                    NIK = p.NIK,
                                    FullName = p.FirstName + " " + p.LastName,
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    Phone = p.Phone,
                                    BirthDate = p.BirthDate,
                                    /*gender = Enum.GetValues(typeof(p.gender)).Cast<PersonVM.Gender>(),*/
                                    /*gender = Enum.Parse(PersonVM.Gender,1),*/
                                    Salary = p.Salary,
                                    Email = p.Email,
                                    Password = a.Password,
                                    Degree = e.Degree,
                                    GPA = e.GPA
                                }).ToList();
            return getPersonVMs;
        }

        public PersonVM GetPersonVMs(string NIK)
        {
            var getPersonVMs = (from p in myContext.Persons
                                join a in myContext.Accounts on
                                p.NIK equals a.NIK
                                join prf in myContext.Profilings on
                                a.NIK equals prf.NIK
                                join e in myContext.Educations on
                                prf.EducationId equals e.EducationId
                                select new PersonVM
                                {
                                    NIK = p.NIK,
                                    FullName = p.FirstName + " " + p.LastName,
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    Phone = p.Phone,
                                    BirthDate = p.BirthDate,
                                    /*gender = p.gender.getS,*/
                                    Salary = p.Salary,
                                    Email = p.Email,
                                    Password = a.Password,
                                    Degree = e.Degree,
                                    GPA = e.GPA
                                }).Where(p => p.NIK == NIK).First();
            return getPersonVMs;
        }

        public int Insert(PersonVM personVM)
        {
            try
            {
                /*myContext.per.Add(entity);*/
                Person person = new Person(personVM.NIK,
                                               personVM.FirstName,
                                               personVM.LastName,
                                               personVM.Phone,
                                               personVM.BirthDate,
                                               personVM.Salary,
                                               personVM.Email
                                               );
                myContext.Persons.Add(person);
                myContext.SaveChanges();

                Account account = new Account(personVM.NIK, personVM.Password);
                myContext.Accounts.Add(account);
                myContext.SaveChanges();

                /*University university = new University("Temp Name");
                myContext.Universities.Add(university);*/

                Education education = new Education(personVM.Degree, personVM.GPA, 3);
                myContext.Educations.Add(education);
                myContext.SaveChanges();

                Profiling profiling = new Profiling(personVM.NIK, education.EducationId);
                myContext.Profilings.Add(profiling);
                var insert = myContext.SaveChanges();

                return insert;
            }
            catch
            {
                throw new DbUpdateException();
            }
        }
    }
}
