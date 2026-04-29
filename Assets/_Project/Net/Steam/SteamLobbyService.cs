using UnityEngine;

namespace Project.Net.Steam
{
    public sealed class SteamLobbyService : MonoBehaviour
    {
        public bool IsInitialized { get; private set; }
        public bool InLobby { get; private set; }

        public bool Initialize()
        {
#if UNITY_STANDALONE && STEAMWORKS_NET
            // Integração real será plugada com Steamworks.NET nesta camada.
            IsInitialized = true;
#else
            IsInitialized = false;
#endif
            return IsInitialized;
        }

        public bool CreateLobby()
        {
            if (!IsInitialized)
                return false;
            InLobby = true;
            return true;
        }

        public bool JoinLobby(string lobbyId)
        {
            if (!IsInitialized || string.IsNullOrWhiteSpace(lobbyId))
                return false;
            InLobby = true;
            return true;
        }

        public void LeaveLobby()
        {
            InLobby = false;
        }
    }
}
