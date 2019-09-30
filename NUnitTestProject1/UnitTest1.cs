using NUnit.Framework;
using WebApplication1.Adapter;

namespace Tests
{
    public class Tests
    {
        private ISocketAdapter _socketAdapter;

        [SetUp]
        public void Setup()
        {
            _socketAdapter = new SocketAdapter();
        }

        [Test]
        public void Test1()
        {
            var response = _socketAdapter.SendToSocket("key", "hey");

            Assert.AreEqual(response, "bye!!");
        }
    }
}