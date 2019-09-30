using System.Collections.Generic;
using WebApplication1.Controllers;

namespace WebApplication1.Adapter
{
    public interface ISocketAdapter
    {
        //string SendToSocket(string key, string message);
        void ReceiveFromSocket(string key, string responseMessage);
        string GetSocketResponse(string key);
    }

    public class SocketAdapter : ISocketAdapter
    {
        private readonly System.Threading.EventWaitHandle _waitHandle = new System.Threading.AutoResetEvent(false);
        //private readonly IMockSocket _socket;
        private readonly Dictionary<string, string> _dataQueue = new Dictionary<string, string>();

        //public SocketAdapter(IMockSocket socket)
        //{
        //    _socket = socket;
        //}

        //public string SendToSocket(string key, string message)
        //{
        //    _socket.Send(message);

        //    return GetSocketResponse(key);
        //}

        public void ReceiveFromSocket(string key, string responseMessage)
        {
            _dataQueue[key] = responseMessage;

            _waitHandle.Set();
        }

        public string GetSocketResponse(string key)
        {
            while (true)
            {
                _waitHandle.WaitOne();

                if (_dataQueue.ContainsKey(key))
                {
                    var responseMessage = _dataQueue[key];
                    _dataQueue.Remove(key);

                    return responseMessage;
                }

                _waitHandle.Set();
            }
        }
    }
}