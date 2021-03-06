using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NETCore.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository <Entity, Key> 
        where Entity:class
        where Context:MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> dbSet;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            dbSet = myContext.Set<Entity>();
        }

        public int Delete(Key key)
        {
            var data = dbSet.Find(key);
            if (data == null)
            {
                throw new ArgumentNullException();
            }
            dbSet.Remove(data);
            return myContext.SaveChanges();
        }

        public IEnumerable<Entity> Get()
        {
            if (dbSet.ToList().Count == 0)
            {
                throw new ArgumentNullException();
            }
            return dbSet.ToList();
        }

        public Entity Get(Key key)
        {
            if (dbSet.Find(key) != null)
            {
                return dbSet.Find(key);
            }
            else
            {
                throw new ArgumentNullException();
            }
            /*throw new NotImplementedException();*/
        }

        public int Insert(Entity entity)
        {
            try
            {
                dbSet.Add(entity);
                var insert = myContext.SaveChanges();
                return insert;
            }
            catch
            {
                throw new DbUpdateException();
            }
        }

        public int Update(Entity entity)
        {
            try
            {
                myContext.Entry(entity).State = EntityState.Modified;
                return myContext.SaveChanges();
            }
            catch
            {
                throw new Exception();
            }
        }

        public static void Email(string htmlString, string toMailAddress)
        {
                string fromMail = "naufalbdo@gmail.com";
                string fromPassword = "PASSWORD_KAMU";
                MailMessage message = new MailMessage();
                
                message.From = new MailAddress(fromMail);
                message.To.Add(new MailAddress(toMailAddress));
                message.Subject = "Test";
                message.Body = "<html><body>"+htmlString+"<html><body>";
                message.IsBodyHtml = true;
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                    
                };
                
                smtpClient.Send(message);
        }

        /*public static void Email(string body, string toMailAddress)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.mailgun.org",587);

            smtpClient.Credentials = new System.Net.NetworkCredential("postmaster@sandboxd1a3cc40065b464083b637c8c1c8f115.mailgun.org", "88150a33edbfa8e4aa72d10f5a5c62d5-c4d287b4-21523c1b");
            //smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("postmaster@sandboxd1a3cc40065b464083b637c8c1c8f115.mailgun.org", "MyWeb Site");
            mail.To.Add(new MailAddress(toMailAddress));
            mail.Body = body;

            smtpClient.Send(mail);
        }*/
    }
}
