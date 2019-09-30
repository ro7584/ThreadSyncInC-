using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1.Adapter
{
    public interface ISocketAdapter
    {
        void ReceiveFromSocket(string key, string responseMessage);
        Task<string> GetSocketResponse(string key, Action task);
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

        public async Task<string> GetSocketResponse(string key, Action action)
        {
            var taskCompletionSource = new TaskCompletionSource<string>();
            Task task = new Task(action);
            task.Start();

            while (true)
            {
                _waitHandle.WaitOne();

                if (_dataQueue.ContainsKey(key))
                {
                    var responseMessage = _dataQueue[key];
                    _dataQueue.Remove(key);

                    taskCompletionSource.SetResult(responseMessage);
                    break;
                }

                _waitHandle.Set();
            }

            return await taskCompletionSource.Task;
        }
    }
}