using System.Threading.Tasks;
using HelloWorldTest.Infrastructure.Messages.Queries.GetHelloWorld;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldTest.WebApi.Controllers
{
    public class MessagesController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(HelloWorldViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult<HelloWorldViewModel>> GetHelloWorld()
        {
            GetHelloWorldQuery query = new GetHelloWorldQuery();
            return Ok(await Mediator.Send(query));
        }
    }
}