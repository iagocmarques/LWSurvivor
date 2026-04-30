using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public static class Lf2FrameDataConverter
    {
        public static Lf2CharacterData ConvertFromParsed(
            string name, string head, string small,
            Dictionary<string, float> movement,
            List<Lf2BmpEntry> bmpEntries,
            Dictionary<int, (Dictionary<string, string> props,
                List<Dictionary<string, string>> itrBlocks,
                List<Dictionary<string, string>> bdyBlocks,
                List<Dictionary<string, string>> opointBlocks,
                string frameName)> frames)
        {
            var character = new Lf2CharacterData
            {
                Name = name ?? "",
                Head = head ?? "",
                Small = small ?? "",
                Movement = movement ?? new Dictionary<string, float>(),
                BmpEntries = bmpEntries ?? new List<Lf2BmpEntry>(),
                Frames = new Dictionary<int, Lf2FrameData>()
            };

            if (frames != null)
            {
                foreach (var kv in frames)
                {
                    var frame = ConvertFrame(kv.Value.props, kv.Value.itrBlocks,
                        kv.Value.bdyBlocks, kv.Value.opointBlocks, kv.Key, kv.Value.frameName);
                    character.Frames[kv.Key] = frame;
                }
            }

            return character;
        }

        public static Lf2FrameData ConvertFrame(Dictionary<string, string> props,
            List<Dictionary<string, string>> itrBlocks,
            List<Dictionary<string, string>> bdyBlocks,
            List<Dictionary<string, string>> opointBlocks,
            int frameId, string frameName)
        {
            var frame = new Lf2FrameData
            {
                Id = frameId,
                Name = frameName,
                Pic = GetInt(props, "pic", 0),
                State = (Lf2State)GetInt(props, "state", 0),
                Wait = GetInt(props, "wait", 0),
                Next = GetInt(props, "next", 999),
                Dvx = GetFloat(props, "dvx", 0f),
                Dvy = GetFloat(props, "dvy", 0f),
                CenterX = GetFloat(props, "centerx", 0f),
                CenterY = GetFloat(props, "centery", 0f),
                SoundId = ParseSoundPath(GetString(props, "sound", "")),
                ExtraProps = new Dictionary<string, string>(props, System.StringComparer.OrdinalIgnoreCase)
            };

            if (itrBlocks != null && itrBlocks.Count > 0)
            {
                frame.Itrs = new Lf2ItrData[itrBlocks.Count];
                for (int i = 0; i < itrBlocks.Count; i++)
                {
                    frame.Itrs[i] = ConvertItr(itrBlocks[i]);
                }
            }
            else
            {
                frame.Itrs = System.Array.Empty<Lf2ItrData>();
            }

            if (bdyBlocks != null && bdyBlocks.Count > 0)
            {
                frame.Bdy = new Lf2BdyData[bdyBlocks.Count];
                for (int i = 0; i < bdyBlocks.Count; i++)
                {
                    var b = bdyBlocks[i];
                    frame.Bdy[i] = new Lf2BdyData(new Rect(
                        GetFloat(b, "x", 0), GetFloat(b, "y", 0),
                        GetFloat(b, "w", 0), GetFloat(b, "h", 0)));
                }
            }
            else
            {
                frame.Bdy = System.Array.Empty<Lf2BdyData>();
            }

            if (opointBlocks != null && opointBlocks.Count > 0)
            {
                frame.Opoints = new Lf2OpointData[opointBlocks.Count];
                for (int i = 0; i < opointBlocks.Count; i++)
                {
                    var o = opointBlocks[i];
                    frame.Opoints[i] = new Lf2OpointData(
                        GetInt(o, "oid", 0),
                        new Vector2(GetFloat(o, "x", 0), GetFloat(o, "y", 0)),
                        new Vector2(GetFloat(o, "dvx", 0), GetFloat(o, "dvy", 0)),
                        GetInt(o, "facing", 0));
                }
            }
            else
            {
                frame.Opoints = System.Array.Empty<Lf2OpointData>();
            }

            return frame;
        }

        private static Lf2ItrData ConvertItr(Dictionary<string, string> d)
        {
            return new Lf2ItrData(
                (Lf2ItrKind)GetInt(d, "kind", 0),
                new Rect(GetFloat(d, "x", 0), GetFloat(d, "y", 0),
                         GetFloat(d, "w", 0), GetFloat(d, "h", 0)),
                GetFloat(d, "dvx", 0),
                GetFloat(d, "dvy", 0),
                GetInt(d, "fall", 70),
                GetInt(d, "arest", 0),
                GetInt(d, "vrest", 0),
                GetInt(d, "injury", 0),
                (Lf2EffectType)GetInt(d, "effect", 0),
                GetInt(d, "bdefend", 0)
            );
        }

        private static int ParseSoundPath(string soundPath)
        {
            if (string.IsNullOrEmpty(soundPath)) return 0;

            int slash = soundPath.LastIndexOf('/');
            int dot = soundPath.LastIndexOf('.');
            if (slash < 0) slash = -1;

            string numStr = dot > slash
                ? soundPath.Substring(slash + 1, dot - slash - 1)
                : soundPath.Substring(slash + 1);

            return int.TryParse(numStr, System.Globalization.NumberStyles.Integer,
                System.Globalization.CultureInfo.InvariantCulture, out var id) ? id : 0;
        }

        private static string GetString(Dictionary<string, string> d, string key, string def)
        {
            return d.TryGetValue(key, out var v) ? v : def;
        }

        private static int GetInt(Dictionary<string, string> d, string key, int def)
        {
            if (d.TryGetValue(key, out var v) && int.TryParse(v, NumberStyles.Integer, CultureInfo.InvariantCulture, out var r))
                return r;
            return def;
        }

        private static float GetFloat(Dictionary<string, string> d, string key, float def)
        {
            if (d.TryGetValue(key, out var v) && float.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out var r))
                return r;
            return def;
        }
    }
}
