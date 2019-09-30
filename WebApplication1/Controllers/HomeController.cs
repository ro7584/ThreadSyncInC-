using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Adapter;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISocketAdapter _socketAdapter;
        private readonly IMockSocket _mockSocket;

        public HomeController(ISocketAdapter socketAdapter, IMockSocket mockSocket)
        {
            _socketAdapter = socketAdapter;
            _mockSocket = mockSocket;
        }

        [HttpGet("home/{key}")]
        public ActionResult<string> Index(string key)
        {
            _mockSocket.Send("any message to socket");

            var responseMessage = _socketAdapter.GetSocketResponse(key);

            return responseMessage;
        }

        [HttpGet("home/about/{key}/{receiveMessage}")]
        public ActionResult<string> About(string key, string receiveMessage)
        {
            _mockSocket.Receive(key, receiveMessage);

            return $"Sending Receive Message({receiveMessage}) to key({key})";
        }
    }
}
