using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SimHubAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace SimHubAPI.Controllers
{
    public class OwnersController : ApiController
    {
        private SimHubAPIContext db = new SimHubAPIContext();

        // GET: api/Owners
        //public IQueryable<Owner> GetOwners()
        //{
        //    return db.Owners;
        //}

        // GET: api/Owners/5
        [ResponseType(typeof(Owner))]
        public async Task<IHttpActionResult> GetOwnerDetail(int id)
        {
            var owner = from o in db.Owners
                        where o.Id == id
                        select new OwnerDTO()
                        {
                            Id = o.Id,
                            Name = o.Name,
                            Phone = o.Phone,
                            Age = o.Age,
                            Address = o.Address,
                            DateJoined = o.DateJoined,
                            Sign = o.Sign
                        };
            if (owner == null)
            {
                return NotFound();
            }
            return Ok(owner);
        }

        //login
        //GET api/owners?username=admin
        [ResponseType(typeof(Owner))]
        public async Task<IHttpActionResult> GetOwner(string username)
        {
            var owner = from o in db.Owners
                        where o.Name == username
                        select o;
            if (owner == null)
            {
                return NotFound();
            }
            return Ok(owner);
        }



        // PUT: api/Owners/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOwner(int id, Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != owner.Id)
            {
                return BadRequest();
            }

            db.Entry(owner).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Owners
        [ResponseType(typeof(Owner))]
        public async Task<IHttpActionResult> PostOwner(Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Owners.Add(owner);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = owner.Id }, owner);
        }

        // DELETE: api/Owners/5
        [ResponseType(typeof(Owner))]
        public async Task<IHttpActionResult> DeleteOwner(int id)
        {
            Owner owner = await db.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            db.Owners.Remove(owner);
            await db.SaveChangesAsync();

            return Ok(owner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OwnerExists(int id)
        {
            return db.Owners.Count(e => e.Id == id) > 0;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}