using ApplicationForm.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationForm
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<UpdateContracturResponse> UpdateContractur(UpdateContracturRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UpdateContracturResponse
            {
                Message="Work"
            });
        }
    }
}
