using System;
using UnityEngine;

namespace Project.Net.Runtime
{
    public sealed class LoopbackTransport : MonoBehaviour, INetTransport
    {
        public event Action<byte[]> OnData;
        public bool IsConnected { get; private set; }

        public void StartHost()
        {
            IsConnected = true;
        }

        public void StartClient(string address)
        {
            IsConnected = true;
        }

        public void Send(byte[] payload)
        {
            if (!IsConnected || payload == null)
                return;

            OnData?.Invoke(payload);
        }

        public void Disconnect(string reason)
        {
            IsConnected = false;
            Debug.Log($"[LoopbackTransport] Disconnect: {reason}");
        }
    }
}
