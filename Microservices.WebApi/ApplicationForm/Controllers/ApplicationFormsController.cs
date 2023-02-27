using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationForm.Enum;
using Microsoft.Extensions.Logging;
using RabbitMQ;

namespace ApplicationForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationFormsController : ControllerBase
    {

        private IEventBus _eventBus;
        Data.ApplicationFormDbContext _context;
        private readonly ILogger<ApplicationFormsController> _logger;
        public ApplicationFormsController(ILogger<ApplicationFormsController> logger, Data.ApplicationFormDbContext context, IEventBus eventBus)
        {
            _logger = logger;
            _context = context;
            _eventBus = eventBus;
        }
        // GET: api/<ConstructionsController>
        [HttpGet]
        public IEnumerable<Entity.ApplicationForm> Get()
        {
            return _context.ApplicationForm.ToList();
        }

        // GET api/<ConstructionsController>/5
        [HttpGet("{id}")]
        public Entity.ApplicationForm Get(int id)
        {
            return _context.ApplicationForm.Find(id);
        }

        // POST api/<ConstructionsController>
        [HttpPost]
        public Entity.ApplicationForm Post(Entity.ApplicationForm applicationForm)
        {
            try
            {
                applicationForm.Status = ApplicationStatus.Pending_payment.ToString();
                _context.ApplicationForm.Add(applicationForm);
                _context.SaveChanges();
                _eventBus.Publish(new NotificationMessage
                {
                    ApplicationId = applicationForm.Id,
                    Message = "Your applicaiton number is in progres.",
                    SubmitDate = applicationForm.CreatedAt
                });
                return applicationForm;
            }
            catch (Exception)
            {
                _logger.LogInformation("ApplicationForm-Add Api: {applicationForm} -- {DateTime.Now}");
                return null;
            }

        }

        // PUT api/<ConstructionsController>/5
        [HttpPut("{id}")]
        public Entity.ApplicationForm Put(int id, Entity.ApplicationForm applicationForm)
        {
            Entity.ApplicationForm _applicationForm = _context.ApplicationForm.Find(id);
            _applicationForm.ConstructionId = applicationForm.ConstructionId;
            _applicationForm.ContractorId = applicationForm.ContractorId;
            _applicationForm.Status = applicationForm.Status;
            _applicationForm.UpdatedAt = DateTime.Now;
            _context.ApplicationForm.Update(_applicationForm);
            _context.SaveChanges();
            return applicationForm;
        }

        // DELETE api/<ConstructionsController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            Entity.ApplicationForm _applicationForm = _context.ApplicationForm.Find(id);
            _context.ApplicationForm.Remove(_applicationForm);
            return _context.SaveChanges() > 0;
        }

    }
}
