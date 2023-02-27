using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contractor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionsController : ControllerBase
    {
        Data.ContractorDbContext _context;
        public ConstructionsController(Data.ContractorDbContext context)
        {
            _context = context;
        }
        // GET: api/<ConstructionsController>
        [HttpGet]   
        public IEnumerable<Entities.Contractor> Get()
        {
            return _context.Contractor.ToList();
        }

        // GET api/<ConstructionsController>/5
        [HttpGet("{id}")]
        public Entities.Contractor Get(int id)
        {
            return _context.Contractor.Find(id);
        }

        // POST api/<ConstructionsController>
        [HttpPost]
        public Entities.Contractor Post(Entities.Contractor contractor)
        {
            _context.Contractor.Add(contractor);
            _context.SaveChanges();
            return contractor;
        }

        // PUT api/<ConstructionsController>/5
        [HttpPut("{id}")]
        public Entities.Contractor Put(int id, Entities.Contractor contractor)
        {
            Entities.Contractor _contractor = _context.Contractor.Find(id);
            _contractor.FullNameEn = contractor.FullNameEn;
            _contractor.FullNameAr = contractor.FullNameAr;
            _contractor.UpdatedAt = DateTime.Now;
            _context.Contractor.Update(_contractor);
            _context.SaveChanges();
            return contractor;
        }

        // DELETE api/<ConstructionsController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            Entities.Contractor _contractor = _context.Contractor.Find(id);
            _context.Contractor.Remove(_contractor);
            return _context.SaveChanges() > 0;
        }
    }
}
