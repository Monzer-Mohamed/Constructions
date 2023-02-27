using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.Protos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        Data.PaymentDbContext _context;
        public PaymentsController(Data.PaymentDbContext context)
        {
            _context = context;
        }
        // GET: api/<ConstructionsController>
        [HttpGet]
        public IEnumerable<Entities.Payment> Get()
        {
            return _context.Payment.ToList();
        }

        // GET api/<ConstructionsController>/5
        [HttpGet("{id}")]
        public Entities.Payment Get(int id)
        {
            return _context.Payment.Find(id);
        }

        // POST api/<ConstructionsController>
        [HttpPost]
        public Entities.Payment Post(Entities.Payment payment)
        {
            payment.CreatedAt = DateTime.Now;
            payment.IsPayed = true;
            _context.Payment.Add(payment);

            _context.SaveChanges();
            //var data = new HelloRequest { Name = "Mukesh" };
            //var grpcChannel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(grpcChannel);
            //var response = await client.SayHelloAsync(data);
            return payment;
        }

        // PUT api/<ConstructionsController>/5
        [HttpPut("{id}")]
        public Entities.Payment Put(int id, Entities.Payment payment)
        {
            Entities.Payment _payment = _context.Payment.Find(id);
            _payment.ApplicaitonId = payment.ApplicaitonId;
            _payment.CardNumber = payment.CardNumber;
            _payment.CardExpiry = payment.CardExpiry;
            _payment.Amount = payment.Amount;
            _payment.IsPayed = payment.IsPayed;
            _payment.UpdatedAt = DateTime.Now;
            _context.Payment.Update(_payment);
            _context.SaveChanges();
            return payment;
        }

        // DELETE api/<ConstructionsController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            Entities.Payment _payment = _context.Payment.Find(id);
            _context.Payment.Remove(_payment);
            return _context.SaveChanges() > 0;
        }
    }
}
