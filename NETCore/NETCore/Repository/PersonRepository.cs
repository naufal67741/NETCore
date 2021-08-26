using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Models;
using NETCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MyContext myContext;
        public PersonRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(string NIK)
        {
            /*throw new NotImplementedException();*/
            var data = myContext.Persons.Find(NIK);
            if(data == null)
            {
                throw new ArgumentNullException();
            }
            myContext.Persons.Remove(data);
            return myContext.SaveChanges();
        }

        public IEnumerable<Person> Get()
        {
            return myContext.Persons.ToList();
            /*throw new NotImplementedException();*/
        }

        public Person Get(string NIK)
        {
            /*throw new NotImplementedException();*/
            return myContext.Persons.Find(NIK);
        }

        public int Insert(Person person)
        {
            myContext.Persons.Add(person);
            var insert = myContext.SaveChanges();
            return insert;
        }

        public int Update(Person person)
        {
            /*var data = myContext.Persons.Find(NIK);*/
            /*if(data != null)
            {*/
                myContext.Entry(person).State = EntityState.Modified;
            return myContext.SaveChanges();
            /*}*/
            /*throw new NotImplementedException();*/
        }
    }
}
