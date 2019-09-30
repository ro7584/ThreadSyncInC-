using WebApplication1.Adapter;

namespace WebApplication1.Controllers
{
    public interface IMockSocket
    {
        void Send(string message);
        void Receive(string key, string message);
    }

    public class MockSocket : IMockSocket
    {
        private readonly ISocketAdapter _socketAdapter;

        public MockSocket(ISocketAdapter socketAdapter)
        {
            _socketAdapter = socketAdapter;
        }

        public void Send(string message)
        {
        }

        public void Receive(string key, string message)
        {
            _socketAdapter.ReceiveFromSocket(key, message);
        }
    }
}