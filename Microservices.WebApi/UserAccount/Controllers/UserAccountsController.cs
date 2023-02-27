using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UserAccount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {

        Data.UserAccountDbContext _context;
        public UserAccountsController(Data.UserAccountDbContext context)
        {
            _context = context;
        }
        // GET: api/<ConstructionsController>
        [HttpGet]
        public IEnumerable<Entities.UserAccount> Get()
        {
            return _context.UserAccount.ToList();
        }

        // GET api/<ConstructionsController>/5
        [HttpGet("{id}")]
        public Entities.UserAccount Get(int id)
        {
            return _context.UserAccount.Find(id);
        }

        // POST api/<ConstructionsController>
        [HttpPost]
        public Entities.UserAccount Post(Entities.UserAccount userAccount)
        { 
            _context.UserAccount.Add(userAccount);
            _context.SaveChanges();
            return userAccount;
        }

        // PUT api/<ConstructionsController>/5
        [HttpPut("{id}")]
        public Entities.UserAccount Put(int id, Entities.UserAccount userAccount)
        {
            Entities.UserAccount _userAccount = _context.UserAccount.Find(id);
            _userAccount.FullNameEn = userAccount.FullNameEn;
            _userAccount.FullNameAr = userAccount.FullNameAr;
            _userAccount.Role = userAccount.Role;
            _userAccount.UpdatedAt = DateTime.Now;
            _context.UserAccount.Update(_userAccount);
            _context.SaveChanges();
            return userAccount;
        }

        // DELETE api/<ConstructionsController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            Entities.UserAccount _userAccount = _context.UserAccount.Find(id);
            _context.UserAccount.Remove(_userAccount);
            return _context.SaveChanges() > 0;
        }

    }
}
