using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;
        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            switch (request.UserId)
            {
                case 1:
                    output.FirstName = "Igriet";
                    output.LastName = "Gonzalez";
                    break;
                case 2:
                    output.FirstName = "Carla";
                    output.LastName = "Gonzalez";
                    break;
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomer(NewCustomerRequest request,
            IServerStreamWriter<CustomerModel> responseStream,
            ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel> {
                new CustomerModel
                {
                    FirstName="Igriet",
                    LastName="Eliseo",
                    Email="igrietgoal@gmail.com",
                    Age=34,
                    IsAlive=true
                },
                new CustomerModel
                {
                    FirstName="Carla",
                    LastName="Gonzalez",
                    Email="arlieegt@gmail.com",
                    Age=31,
                    IsAlive=true
                },
                new CustomerModel
                {
                    FirstName="Peter",
                    LastName="Parker",
                    Email="peter.parker@marvel.com",
                    Age=21,
                    IsAlive=false
                }
            };

            foreach(var customer in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(customer);
            }
        }
    }
}
