using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LF2Importer.EditorTools
{
    public static class Lf2ClipBuilder
    {
        public static AnimationClip BuildSpriteClip(
            string clipName,
            IReadOnlyList<Sprite> sprites,
            IReadOnlyList<int> waits,
            float timeUnitSeconds)
        {
            if (sprites == null || sprites.Count == 0)
                return null;
            if (waits == null || waits.Count != sprites.Count)
                return null;

            var clip = new AnimationClip { name = clipName };
            clip.frameRate = 30f;

            var binding = EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite");
            var keys = new ObjectReferenceKeyframe[sprites.Count];
            var t = 0f;
            for (var i = 0; i < sprites.Count; i++)
            {
                keys[i] = new ObjectReferenceKeyframe { time = t, value = sprites[i] };
                var w = Mathf.Max(1, waits[i]);
                t += w * timeUnitSeconds;
            }

            AnimationUtility.SetObjectReferenceCurve(clip, binding, keys);
            clip.wrapMode = WrapMode.Loop;
            var set = AnimationUtility.GetAnimationClipSettings(clip);
            set.loopTime = true;
            AnimationUtility.SetAnimationClipSettings(clip, set);
            return clip;
        }

        public static int GetState(Lf2ParsedFrame f)
        {
            if (f.props.TryGetValue("state", out var s) && int.TryParse(s, out var v))
                return v;
            return -1;
        }

        public static List<int> FollowNextUntilTerminal(Lf2ParsedDat dat, int startId, int? sameState, int maxSteps)
        {
            var list = new List<int>();
            var visited = new HashSet<int>();
            var cur = startId;
            for (var step = 0; step < maxSteps; step++)
            {
                if (!dat.frames.TryGetValue(cur, out var fr))
                    break;
                if (sameState.HasValue && GetState(fr) != sameState.Value)
                    break;
                list.Add(cur);
                if (!fr.props.TryGetValue("next", out var ns) || !int.TryParse(ns, out var nx))
                    break;
                if (nx == 999)
                    break;
                if (visited.Contains(cur))
                    break;
                visited.Add(cur);
                cur = nx;
            }

            return list;
        }

        public static List<int> CollectWalkLikeFrameIds(Lf2ParsedDat dat)
        {
            var ids = new List<int>();
            foreach (var f in dat.frames.Values)
            {
                if (GetState(f) != 1)
                    continue;
                if (!f.props.TryGetValue("next", out var nx) || nx.Trim() != "999")
                    continue;
                if (f.frameName.IndexOf("heavy", StringComparison.OrdinalIgnoreCase) >= 0)
                    continue;
                if (f.frameName.IndexOf("weapon", StringComparison.OrdinalIgnoreCase) >= 0)
                    continue;
                ids.Add(f.id);
            }

            ids.Sort();
            return ids;
        }

        public static List<int> CollectRunLocomotionFrameIds(Lf2ParsedDat dat)
        {
            return dat.frames.Values
                .Where(f => GetState(f) == 2 && f.frameName.IndexOf("heavy", StringComparison.OrdinalIgnoreCase) < 0
                    && f.frameName.IndexOf("weapon", StringComparison.OrdinalIgnoreCase) < 0)
                .Select(f => f.id)
                .OrderBy(id => id)
                .ToList();
        }

        public static int FindFirstFrameIdWithState(Lf2ParsedDat dat, int state)
        {
            var best = int.MaxValue;
            foreach (var f in dat.frames.Values)
            {
                if (GetState(f) == state && f.id < best)
                    best = f.id;
            }

            return best == int.MaxValue ? -1 : best;
        }

        public static List<int> CollectStateFramesSorted(Lf2ParsedDat dat, int state)
        {
            var ids = new List<int>();
            foreach (var f in dat.frames.Values)
            {
                if (GetState(f) == state)
                    ids.Add(f.id);
            }

            ids.Sort();
            return ids;
        }

        public static int ReadWait(Lf2ParsedFrame f)
        {
            if (f.props.TryGetValue("wait", out var w) && int.TryParse(w, out var v))
                return Mathf.Max(1, v);
            return 1;
        }
    }
}
