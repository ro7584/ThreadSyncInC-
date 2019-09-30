using System.Collections.Generic;
using System.Threading;

namespace WebApplication1.Adapter
{
    public interface ISocketAdapter
    {
        void ReceiveFromSocket(string key, string responseMessage);
        string GetSocketResponse(string key);
    }

    public class SocketAdapter : ISocketAdapter
    {
        private readonly Dictionary<string, string> _dataQueue = new Dictionary<string, string>();
        private readonly EventWaitHandle _waitHandle = new AutoResetEvent(false);

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