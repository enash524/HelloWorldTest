using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HelloWorldTest.Infrastructure.Messages.Queries.GetHelloWorld
{
    public class GetHelloWorldQueryHandler : IRequestHandler<GetHelloWorldQuery, HelloWorldViewModel>
    {
        public Task<HelloWorldViewModel>Handle(GetHelloWorldQuery request, CancellationToken cancellationToken)
        {
            HelloWorldViewModel model = new HelloWorldViewModel
            {
                MessageText = "Hello World"
            };

            return Task.FromResult(model);
        }
    }
}