using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Construction.Microservice.Entities;
using System.Linq;
using System;
using Mapster;
using System.Net.Http;
using Construction.Model;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Construction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstructionsController : ControllerBase
    {
        Data.ConstructionDbContext _context;

        public ConstructionsController(Data.ConstructionDbContext context)
        {
            _context = context;
        }
        // GET: api/<ConstructionsController>
        [HttpGet]
        public IEnumerable<Microservice.Entities.ConstructionDto> Get()
        {
            var constructions = _context.Construction.ToList();

            return constructions.Adapt<IEnumerable<Microservice.Entities.ConstructionDto>>(); 
        }

        // GET api/<ConstructionsController>/5
        [HttpGet("{id}")]
        public Microservice.Entities.ConstructionDto Get(int id)
        {
            var construction = _context.Construction.Find(id); 

            return construction.Adapt<ConstructionDto>();
        }

        // POST api/<ConstructionsController>
        [HttpPost]
        public ConstructionDto Post(ConstructionDto constructionDto)
        {
            var construction = constructionDto.Adapt<Construction.Microservice.Entities.Construction>();

            construction.CreatedAt = DateTime.Now;

            _context.Construction.Add(construction);

            _context.SaveChanges();

        //    await createApplicationForm(construction);

            return construction.Adapt<ConstructionDto>() ;
        }

        private async Task createApplicationForm(Construction.Microservice.Entities.Construction construction)
        {
            HttpClient client = new HttpClient();

            ApplicationForm applicationForm = new ApplicationForm();
            
            applicationForm.ConstructionId = construction.Id;
            
            applicationForm.ContractorId =1;
            
            client.DefaultRequestHeaders.Add("x-Gateway-APIKey", "pgH7QzFHJx4w46fI5Uzi4RvtTwlEXpwwqqaa");

            var response   = await client.PostAsJsonAsync("https://localhost:44319/gateway/Api/ApplicationForms/", applicationForm);
 
        }

        // PUT api/<ConstructionsController>/5
        [HttpPut("{id}")]
        public Microservice.Entities.ConstructionDto Put(int id, ConstructionDto constructionDto)
        {
            Microservice.Entities.Construction _construction = _context.Construction.Find(id);

            constructionDto.Adapt(_construction);

            _context.Construction.Update(_construction);

            _context.SaveChanges();

            return _construction.Adapt<ConstructionDto>();
        }

        // DELETE api/<ConstructionsController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            Microservice.Entities.Construction _construction = _context.Construction.Find(id);

            _context.Construction.Remove(_construction);

            return _context.SaveChanges() > 0;
        }
    }
}
