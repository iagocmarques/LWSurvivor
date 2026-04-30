using Project.Core.Tick;
using System;
using UnityEngine;

namespace Project.Net.Runtime
{
    [DisallowMultipleComponent]
    public sealed class NetSessionManager : MonoBehaviour, ITickable
    {
        [SerializeField] private NetRole role = NetRole.Offline;
        [SerializeField] private int snapshotSendRateHz = 15;

        private INetTransport _transport;
        private float _snapshotTimer;
        private EnemySnapshotReplicator _replicator;
        private string _lastAddress = "loopback";
        private int _simulatedPingMs;
        private float _pingTimer;
        private bool _lastConnected;

        public NetRole Role => role;
        public bool IsConnected => _transport != null && _transport.IsConnected;
        public int SimulatedPingMs => _simulatedPingMs;
        public string LastAddress => _lastAddress;
        public string LastStatusMessage { get; private set; } = "offline";

        public event Action<NetRole> OnRoleChanged;
        public event Action<bool> OnConnectionChanged;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _transport = gameObject.AddComponent<LoopbackTransport>();
            _transport.OnData += OnTransportData;
            _replicator = gameObject.AddComponent<EnemySnapshotReplicator>();
        }

        private void OnEnable()
        {
            FixedTickSystem.Register(this);
        }

        private void OnDisable()
        {
            FixedTickSystem.Unregister(this);
            if (_transport != null)
                _transport.OnData -= OnTransportData;
        }

        public void StartHost()
        {
            role = NetRole.Host;
            _transport.StartHost();
            LastStatusMessage = "host_started";
            EmitRoleChanged();
        }

        public void StartClient(string address)
        {
            role = NetRole.Client;
            _lastAddress = string.IsNullOrWhiteSpace(address) ? "loopback" : address;
            _transport.StartClient(address);
            LastStatusMessage = "client_started";
            EmitRoleChanged();
        }

        public void GoOffline()
        {
            role = NetRole.Offline;
            _transport.Disconnect("offline");
            LastStatusMessage = "offline";
            EmitRoleChanged();
        }

        public void ReconnectLast()
        {
            if (role == NetRole.Host)
            {
                StartHost();
                return;
            }

            StartClient(_lastAddress);
        }

        public void Tick(in TickContext context)
        {
            if (role != NetRole.Host || !IsConnected)
            {
                UpdateConnectionState();
                UpdatePing(context.FixedDelta);
                return;
            }

            _snapshotTimer += context.FixedDelta;
            var interval = 1f / Mathf.Max(1, snapshotSendRateHz);
            if (_snapshotTimer < interval)
                return;

            _snapshotTimer = 0f;
            var snapshot = _replicator.Capture(context.Tick);
            var bytes = EnemySnapshotSerializer.Serialize(snapshot);
            _transport.Send(bytes);
            UpdateConnectionState();
            UpdatePing(context.FixedDelta);
        }

        private void OnTransportData(byte[] data)
        {
            if (data == null || data.Length == 0)
                return;

            var snap = EnemySnapshotSerializer.Deserialize(data);
            if (role == NetRole.Client)
                _replicator.ApplyRemoteSnapshot(snap);
            LastStatusMessage = $"snapshot_t{snap.Tick}";
        }

        private void UpdatePing(float dt)
        {
            if (!IsConnected)
            {
                _simulatedPingMs = 0;
                return;
            }

            _pingTimer -= dt;
            if (_pingTimer > 0f)
                return;

            _pingTimer = 0.5f;
            _simulatedPingMs = UnityEngine.Random.Range(28, 92);
        }

        private void UpdateConnectionState()
        {
            var connected = IsConnected;
            if (connected == _lastConnected)
                return;

            _lastConnected = connected;
            OnConnectionChanged?.Invoke(connected);
        }

        private void EmitRoleChanged()
        {
            OnRoleChanged?.Invoke(role);
        }
    }
}
