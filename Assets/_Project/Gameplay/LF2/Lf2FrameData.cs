using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    /// <summary>
    /// Strongly-typed frame data converted from Lf2ParsedFrame.
    /// </summary>
    public readonly struct Lf2ItrData
    {
        public readonly Lf2ItrKind Kind;
        public readonly Rect Rect;
        public readonly float Dvx;
        public readonly float Dvy;
        public readonly int Fall;
        public readonly int Arest;
        public readonly int Vrest;
        public readonly int Injury;
        public readonly Lf2EffectType Effect;
        public readonly int Bdefend;

        public Lf2ItrData(Lf2ItrKind kind, Rect rect, float dvx, float dvy, int fall, int arest, int vrest, int injury, Lf2EffectType effect, int bdefend)
        {
            Kind = kind; Rect = rect; Dvx = dvx; Dvy = dvy;
            Fall = fall; Arest = arest; Vrest = vrest;
            Injury = injury; Effect = effect; Bdefend = bdefend;
        }
    }

    public readonly struct Lf2BdyData
    {
        public readonly Rect Rect;
        public Lf2BdyData(Rect rect) { Rect = rect; }
    }

    public readonly struct Lf2OpointData
    {
        public readonly int Oid;
        public readonly Vector2 Position;
        public readonly Vector2 Velocity;
        public readonly int Facing;

        public Lf2OpointData(int oid, Vector2 position, Vector2 velocity, int facing)
        {
            Oid = oid; Position = position; Velocity = velocity; Facing = facing;
        }
    }

    public sealed class Lf2FrameData
    {
        public int Id;
        public string Name = "";
        public int Pic;
        public Lf2State State;
        public int Wait;
        public int Next;
        public float Dvx;
        public float Dvy;
        public float CenterX;
        public float CenterY;

        public Lf2ItrData[] Itrs;
        public Lf2BdyData[] Bdy;
        public Lf2OpointData[] Opoints;

        public int SoundId;

        public Dictionary<string, string> ExtraProps;
    }

    /// <summary>
    /// Full character data parsed from a .dat file, ready for runtime use.
    /// </summary>
    public sealed class Lf2CharacterData
    {
        public string Name = "";
        public string Head = "";
        public string Small = "";
        public Dictionary<string, float> Movement;
        public Dictionary<int, Lf2FrameData> Frames;
        public List<Lf2BmpEntry> BmpEntries;
        public Lf2FrameRoleIds RoleIds;
    }

    public sealed class Lf2BmpEntry
    {
        public int Start;
        public int End;
        public string Path = "";
        public int CellW;
        public int CellH;
        public int Row;
        public int Col;
    }
}
