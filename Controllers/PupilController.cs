using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RabbitMq1.Domain;

namespace RabbitMq1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PupilController : ControllerBase
    {
        
        private readonly IPublishEndpoint _publishEndpoint;

        public PupilController(IPublishEndpoint publishEndpoint, IServiceProvider serviceProvider)
        {
            _publishEndpoint = publishEndpoint;
      
        }


        [HttpPost]
        public async Task<IActionResult> CreatePupil([FromBody] Pupil pupil)
        {
            await _publishEndpoint.Publish(pupil);

            Console.WriteLine("Message published successfully!");
            return Ok();
        }
    }
}
