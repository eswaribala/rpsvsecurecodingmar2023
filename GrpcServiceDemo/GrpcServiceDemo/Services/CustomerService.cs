using Grpc.Core;

namespace GrpcServiceDemo.Services
{
    public class CustomerService:CustomerProcess.CustomerProcessBase
    {
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<ShowResponse>AddCustomer(AddRequest addRequest, ServerCallContext context)
        {
            return Task.FromResult(new ShowResponse
            {
                Message = "Customer Added" + addRequest.Customer.FirstName
            }); 

        }
    }
}
