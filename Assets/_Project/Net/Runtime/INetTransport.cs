using System;

namespace Project.Net.Runtime
{
    public interface INetTransport
    {
        event Action<byte[]> OnData;
        bool IsConnected { get; }
        void StartHost();
        void StartClient(string address);
        void Send(byte[] payload);
        void Disconnect(string reason);
    }
}
