using System;
using System.IO;

namespace Project.Net.Runtime
{
    public static class EnemySnapshotSerializer
    {
        public static byte[] Serialize(HordeSnapshot snapshot)
        {
            using (var ms = new MemoryStream(4096))
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(snapshot.Tick);
                var count = snapshot.Enemies != null ? snapshot.Enemies.Length : 0;
                bw.Write(count);
                for (var i = 0; i < count; i++)
                {
                    var e = snapshot.Enemies[i];
                    bw.Write(e.Id);
                    bw.Write((short)Math.Round(e.Position.x * 100f));
                    bw.Write((short)Math.Round(e.Position.y * 100f));
                    bw.Write((short)e.Hp);
                    bw.Write(e.Flags);
                }
                return ms.ToArray();
            }
        }

        public static HordeSnapshot Deserialize(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            using (var br = new BinaryReader(ms))
            {
                var tick = br.ReadInt32();
                var count = br.ReadInt32();
                var arr = new EnemySnapshot[count];
                for (var i = 0; i < count; i++)
                {
                    arr[i] = new EnemySnapshot
                    {
                        Id = br.ReadInt32(),
                        Position = new UnityEngine.Vector2(br.ReadInt16() / 100f, br.ReadInt16() / 100f),
                        Hp = br.ReadInt16(),
                        Flags = br.ReadByte()
                    };
                }

                return new HordeSnapshot
                {
                    Tick = tick,
                    Enemies = arr
                };
            }
        }
    }
}
