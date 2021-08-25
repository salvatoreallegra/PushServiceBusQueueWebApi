using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace servicebus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IConfiguration _configuration;

        public OrdersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderModel order)
        {
            IQueueClient queueClient = new QueueClient(_configuration["QueueConnectionString"], _configuration["QueueName"]);
            var orderJSON = JsonConvert.SerializeObject(order);
            var orderMessage = new Message(Encoding.UTF8.GetBytes(orderJSON))
            {
                MessageId = Guid.NewGuid().ToString(),
                ContentType = "application/json"
            };
            await queueClient.SendAsync(orderMessage).ConfigureAwait(false);

            return Ok("Create order message has been successfully pushed to queue");
        }
    
}
}
