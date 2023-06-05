using FoodMessages.Interfaces;
using FoodMessages.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodMessages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessenger _messenger;

        public MessageController(IMessenger messenger)
        {
            _messenger = messenger;
        }

        [HttpPost]
        public IActionResult PostMessage([FromBody] MessageModel message)
        {
            try
            {
                _messenger.SendMessage(message);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
