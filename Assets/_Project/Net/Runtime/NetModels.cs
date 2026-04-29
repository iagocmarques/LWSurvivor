using UnityEngine;

namespace Project.Net.Runtime
{
    public enum NetRole
    {
        Offline = 0,
        Host = 1,
        Client = 2
    }

    public struct NetInputFrame
    {
        public int Tick;
        public Vector2 Move;
        public bool AttackPressed;
        public bool DashPressed;
    }

    public struct EnemySnapshot
    {
        public int Id;
        public Vector2 Position;
        public int Hp;
        public byte Flags;
    }

    public struct HordeSnapshot
    {
        public int Tick;
        public EnemySnapshot[] Enemies;
    }
}
