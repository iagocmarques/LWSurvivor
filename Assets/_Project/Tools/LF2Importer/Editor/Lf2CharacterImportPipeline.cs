using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace LF2Importer.EditorTools
{
    public static class Lf2CharacterImportPipeline
    {
        public sealed class ImportResult
        {
            public bool success;
            public string message = "";
            public ImportedLf2Character character;
            public readonly List<string> lines = new List<string>();
        }

        public static ImportResult ImportSingle(
            Lf2ImportSettings settings,
            Lf2ObjectEntry entry,
            IReadOnlyList<Lf2ObjectEntry> allObjects,
            string outputSubFolder = null)
        {
            var result = new ImportResult();
            if (settings == null || entry == null)
            {
                result.message = "Settings or entry null.";
                return result;
            }

            if (!Lf2DataTxtParser.TryResolveDatPath(settings.lf2GameRootPath, entry, out var datAbs))
            {
                result.message = $"DAT not found for id {entry.id}: {entry.file}";
                return result;
            }

            Lf2SpriteSheetResolver.ClearCache();
            var parsed = Lf2DatParser.ParseFromFile(datAbs);
            var resolver = new Lf2OidResolver(allObjects);

            var slug = SanitizeFolderName(Path.GetFileNameWithoutExtension(entry.file));
            var outRoot = string.IsNullOrEmpty(settings.outputRootPath)
                ? "Assets/_Imported/LF2"
                : settings.outputRootPath.Replace('\\', '/');
            var charFolder = string.IsNullOrEmpty(outputSubFolder)
                ? $"{outRoot}/Characters/{entry.id:D3}_{slug}"
                : outputSubFolder.Replace('\\', '/');

            EnsureDir(charFolder);
            EnsureDir(charFolder + "/Animations");
            EnsureDir(charFolder + "/Data");
            if (settings.writeDecryptedDebugCopy)
                EnsureDir(charFolder + "/Raw");

            if (settings.writeDecryptedDebugCopy)
            {
                var rawPath = charFolder + "/Raw/decrypted.txt";
                File.WriteAllText(FullProjectPath(rawPath), Lf2DatDecryptor.ReadDatAsText(datAbs), new UTF8Encoding(false));
            }

            var warnings = new List<string>(parsed.parseWarnings);
            ValidatePics(parsed, warnings);

            var so = ScriptableObject.CreateInstance<ImportedLf2Character>();
            so.dataTxtId = entry.id;
            so.sourceDatRelativePath = entry.file.Replace('\\', '/');
            so.displayName = string.IsNullOrEmpty(parsed.name) ? slug : parsed.name;
            so.headSpritePath = parsed.head;
            so.smallSpritePath = parsed.small;
            so.importWarnings = warnings;

            foreach (var b in parsed.bmpEntries)
            {
                so.bmpSheets.Add(new Lf2BmpSheetInfo
                {
                    picStart = b.start,
                    picEnd = b.end,
                    sourcePath = b.path,
                    cellW = b.cellW,
                    cellH = b.cellH,
                    row = b.row,
                    col = b.col
                });
            }

            foreach (var kv in parsed.movement)
                so.movementParams.Add(new Lf2MovementParam { key = kv.Key, amount = kv.Value });

            FillFrameRows(so, parsed, resolver);
            BuildMovesFromHitBindings(so, parsed);

            var artRoot = string.IsNullOrEmpty(settings.convertedSpritesRoot)
                ? "Assets/Art/lf2_ref/characters"
                : settings.convertedSpritesRoot.Replace('\\', '/');

            var animFolder = charFolder + "/Animations";
            if (settings.importClips)
            {
                so.clipStanding = SaveClip(
                    BuildLocomotionClip(settings, parsed, artRoot, "standing", 0),
                    animFolder + "/standing.anim");

                var walkIds = Lf2ClipBuilder.CollectWalkLikeFrameIds(parsed);
                if (walkIds.Count == 0)
                {
                    var ws = Lf2ClipBuilder.FindFirstFrameIdWithState(parsed, 1);
                    if (ws >= 0)
                        walkIds = Lf2ClipBuilder.FollowNextUntilTerminal(parsed, ws, 1, 256);
                }

                so.clipWalking = SaveClip(
                    BuildClipFromFrameIds(settings, parsed, artRoot, "walking", walkIds),
                    animFolder + "/walking.anim");

                var runIds = Lf2ClipBuilder.CollectRunLocomotionFrameIds(parsed);
                so.clipRunning = SaveClip(
                    BuildClipFromFrameIds(settings, parsed, artRoot, "running", runIds),
                    animFolder + "/running.anim");

                var defendIds = parsed.frames.Values
                    .Where(f => Lf2ClipBuilder.GetState(f) == 7
                        && f.frameName.IndexOf("defend", StringComparison.OrdinalIgnoreCase) >= 0
                        && f.frameName.IndexOf("broken", StringComparison.OrdinalIgnoreCase) < 0)
                    .Select(f => f.id)
                    .Distinct()
                    .OrderBy(id => id)
                    .Take(8)
                    .ToList();
                so.clipDefend = SaveClip(
                    BuildClipFromFrameIds(settings, parsed, artRoot, "defend", defendIds),
                    animFolder + "/defend.anim");

                BuildExtraAttackClips(settings, parsed, artRoot, so, animFolder);
            }

            var soPath = charFolder + $"/Data/{slug}_Imported.asset";
            AssetDatabase.CreateAsset(so, soPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            result.success = true;
            result.character = AssetDatabase.LoadAssetAtPath<ImportedLf2Character>(soPath);
            result.message = $"Imported -> {soPath}";
            result.lines.AddRange(warnings);
            return result;
        }

        private static AnimationClip SaveClip(AnimationClip clip, string unityAnimPath)
        {
            if (clip == null)
                return null;
            var p = unityAnimPath.Replace('\\', '/');
            var dir = Path.GetDirectoryName(p);
            if (!string.IsNullOrEmpty(dir))
                EnsureDir(dir.Replace('\\', '/'));
            AssetDatabase.CreateAsset(clip, p);
            return AssetDatabase.LoadAssetAtPath<AnimationClip>(p);
        }

        /// <returns>Contagem de imports com sucesso e com falha (type:0 em data.txt).</returns>
        public static (int ok, int fail) ImportAllCharacters(Lf2ImportSettings settings, Action<float, string> progress = null)
        {
            if (settings == null)
                return (0, 0);
            var dataTxt = Path.Combine(settings.lf2GameRootPath, "data", "data.txt");
            var objs = Lf2DataTxtParser.ReadObjects(dataTxt);
            var chars = Lf2DataTxtParser.GetCharacters(objs).ToList();
            var report = new StringBuilder();
            report.AppendLine("# LF2 batch import report");
            report.AppendLine();
            var ok = 0;
            var fail = 0;
            for (var i = 0; i < chars.Count; i++)
            {
                var c = chars[i];
                progress?.Invoke((float)i / chars.Count, c.file);
                try
                {
                    var r = ImportSingle(settings, c, objs);
                    if (r.success)
                    {
                        ok++;
                        report.AppendLine($"## OK id={c.id} `{c.file}`");
                        foreach (var w in r.lines)
                            report.AppendLine($"- {w}");
                    }
                    else
                    {
                        fail++;
                        report.AppendLine($"## FAIL id={c.id} `{c.file}`");
                        report.AppendLine($"- {r.message}");
                    }
                }
                catch (Exception ex)
                {
                    fail++;
                    report.AppendLine($"## ERROR id={c.id} `{c.file}`");
                    report.AppendLine($"- {ex.Message}");
                }
            }

            report.AppendLine();
            report.AppendLine($"Summary: ok={ok} fail={fail}");
            var outRoot = (string.IsNullOrEmpty(settings.outputRootPath) ? "Assets/_Imported/LF2" : settings.outputRootPath).Replace('\\', '/');
            EnsureDir(outRoot);
            var reportPath = outRoot + "/import_report.md";
            File.WriteAllText(FullProjectPath(reportPath), report.ToString(), new UTF8Encoding(false));
            AssetDatabase.Refresh();
            progress?.Invoke(1f, "Done");
            Debug.Log($"[LF2Importer] Batch complete. Report: {reportPath}");
            return (ok, fail);
        }

        private static AnimationClip BuildLocomotionClip(
            Lf2ImportSettings settings,
            Lf2ParsedDat dat,
            string artRoot,
            string clipBaseName,
            int state)
        {
            var start = Lf2ClipBuilder.FindFirstFrameIdWithState(dat, state);
            if (start < 0)
                return null;
            var ids = Lf2ClipBuilder.FollowNextUntilTerminal(dat, start, state, 256);
            if (ids.Count == 0)
                return null;
            return BuildClipFromFrameIds(settings, dat, artRoot, clipBaseName, ids);
        }

        private static AnimationClip BuildClipFromFrameIds(
            Lf2ImportSettings settings,
            Lf2ParsedDat dat,
            string artRoot,
            string clipBaseName,
            List<int> frameIds)
        {
            var sprites = new List<Sprite>();
            var waits = new List<int>();
            foreach (var fid in frameIds)
            {
                if (!dat.frames.TryGetValue(fid, out var fr))
                    continue;
                if (!fr.props.TryGetValue("pic", out var ps) || !int.TryParse(ps, out var pic))
                    continue;
                if (Lf2SpriteSheetResolver.TryGetSpriteForPic(dat, artRoot, pic, out var sp, out _))
                {
                    sprites.Add(sp);
                    waits.Add(Lf2ClipBuilder.ReadWait(fr));
                }
            }

            if (sprites.Count == 0)
                return null;

            var clip = Lf2ClipBuilder.BuildSpriteClip(clipBaseName, sprites, waits, settings.timeUnitSeconds);
            return clip;
        }

        private const int MaxExtraAttackClips = 64;

        private static void BuildExtraAttackClips(
            Lf2ImportSettings settings,
            Lf2ParsedDat dat,
            string artRoot,
            ImportedLf2Character so,
            string animFolder)
        {
            so.extraClips.Clear();
            var used = new HashSet<string>();
            var n = 0;
            foreach (var mv in so.moves.OrderBy(m => m.startFrameId))
            {
                if (n >= MaxExtraAttackClips)
                    break;
                if (mv.frameSequence.Count == 0)
                    continue;
                var clipName = $"atk_{SanitizeClipName(mv.name)}_{mv.startFrameId}";
                if (!used.Add(clipName))
                    continue;
                var clip = BuildClipFromFrameIds(settings, dat, artRoot, clipName, mv.frameSequence);
                if (clip == null)
                    continue;
                var path = $"{animFolder}/{clipName}.anim";
                AssetDatabase.CreateAsset(clip, path);
                var saved = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
                if (saved != null)
                    so.extraClips.Add(saved);
                n++;
            }

            AssetDatabase.SaveAssets();
        }

        private static string SanitizeClipName(string n)
        {
            if (string.IsNullOrEmpty(n))
                return "move";
            var sb = new StringBuilder();
            foreach (var ch in n)
            {
                if (char.IsLetterOrDigit(ch) || ch == '_')
                    sb.Append(ch);
                else if (char.IsWhiteSpace(ch))
                    sb.Append('_');
            }

            return sb.Length > 0 ? sb.ToString() : "move";
        }

        private static void FillFrameRows(ImportedLf2Character so, Lf2ParsedDat parsed, Lf2OidResolver resolver)
        {
            var ordered = parsed.frames.Values.OrderBy(f => f.id);
            foreach (var fr in ordered)
            {
                var row = new ImportedLf2FrameRow
                {
                    id = fr.id,
                    frameName = fr.frameName
                };
                if (fr.props.TryGetValue("pic", out var p))
                    int.TryParse(p, out row.pic);
                if (fr.props.TryGetValue("state", out var st))
                    int.TryParse(st, out row.state);
                if (fr.props.TryGetValue("wait", out var w))
                    int.TryParse(w, out row.wait);
                if (fr.props.TryGetValue("next", out var nx))
                    int.TryParse(nx, out row.next);
                if (fr.props.TryGetValue("dvx", out var dx))
                    int.TryParse(dx, out row.dvx);
                if (fr.props.TryGetValue("dvy", out var dy))
                    int.TryParse(dy, out row.dvy);
                if (fr.props.TryGetValue("dvz", out var dz))
                    int.TryParse(dz, out row.dvz);
                if (fr.props.TryGetValue("sound", out var snd))
                    row.soundPath = snd;

                foreach (var b in fr.bdyBlocks)
                    row.hurtboxes.Add(DictToRect(b));
                foreach (var it in fr.itrBlocks)
                    row.hitboxes.Add(DictToRect(it));
                foreach (var op in fr.opointBlocks)
                {
                    var oid = 0;
                    if (op.TryGetValue("oid", out var os))
                        int.TryParse(os, out oid);

                    row.opoints.Add(new Lf2OpointRow
                    {
                        oid = oid,
                        resolvedRelativeFile = resolver.GetRelativeFileOrEmpty(oid),
                        raw = string.Join(" ", op.Select(kv => kv.Key + "=" + kv.Value))
                    });
                }

                so.frameRows.Add(row);
            }
        }

        private static Lf2RectBlock DictToRect(Dictionary<string, string> d)
        {
            var r = new Lf2RectBlock { raw = string.Join(" ", d.Select(kv => kv.Key + ":" + kv.Value)) };
            if (d.TryGetValue("kind", out var k) && int.TryParse(k, out var kind))
                r.kind = kind;
            if (d.TryGetValue("x", out var x) && int.TryParse(x, out var xi))
                r.x = xi;
            if (d.TryGetValue("y", out var y) && int.TryParse(y, out var yi))
                r.y = yi;
            if (d.TryGetValue("w", out var w) && int.TryParse(w, out var wi))
                r.w = wi;
            if (d.TryGetValue("h", out var h) && int.TryParse(h, out var hi))
                r.h = hi;
            return r;
        }

        private static void BuildMovesFromHitBindings(ImportedLf2Character so, Lf2ParsedDat parsed)
        {
            var byStart = new Dictionary<int, ImportedLf2Move>();
            foreach (var fr in parsed.frames.Values)
            {
                foreach (var kv in fr.props)
                {
                    if (!kv.Key.StartsWith("hit_", StringComparison.OrdinalIgnoreCase))
                        continue;
                    if (!int.TryParse(kv.Value, out var target))
                        continue;
                    if (!parsed.frames.ContainsKey(target))
                        continue;
                    if (!byStart.TryGetValue(target, out var mv))
                    {
                        mv = new ImportedLf2Move
                        {
                            name = parsed.frames[target].frameName,
                            startFrameId = target,
                            frameSequence = Lf2ClipBuilder.FollowNextUntilTerminal(parsed, target, null, 512)
                        };
                        byStart[target] = mv;
                    }

                    mv.inputBindings.Add(new Lf2InputBinding
                    {
                        hitKey = kv.Key,
                        fromFrameId = fr.id,
                        toFrameId = target
                    });
                }
            }

            so.moves.AddRange(byStart.Values.OrderBy(m => m.startFrameId));
        }

        private static void ValidatePics(Lf2ParsedDat dat, List<string> warnings)
        {
            foreach (var fr in dat.frames.Values)
            {
                if (!fr.props.TryGetValue("pic", out var ps) || !int.TryParse(ps, out var pic))
                    continue;
                if (!Lf2DatParser.IsPicInRange(dat, pic))
                    warnings.Add($"Frame {fr.id}: pic {pic} outside bmp ranges.");
            }
        }

        private static void EnsureDir(string unityPath)
        {
            var full = FullProjectPath(unityPath);
            if (!Directory.Exists(full))
                Directory.CreateDirectory(full);
        }

        private static string FullProjectPath(string unityPath)
        {
            var rel = unityPath.Replace('\\', '/');
            if (!rel.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
                rel = "Assets/" + rel.TrimStart('/');
            return Path.GetFullPath(Path.Combine(Application.dataPath, rel.Substring(7)));
        }

        private static string SanitizeFolderName(string name)
        {
            var invalid = Path.GetInvalidFileNameChars();
            var sb = new StringBuilder();
            foreach (var ch in name)
            {
                if (Array.IndexOf(invalid, ch) >= 0)
                    sb.Append('_');
                else
                    sb.Append(ch);
            }

            return sb.Length > 0 ? sb.ToString() : "char";
        }
    }
}
