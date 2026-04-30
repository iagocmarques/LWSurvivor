using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Project.Gameplay.LF2;

namespace Project.EditorTools
{
    public static class Lf2Wave73Verifier
    {
        private const string DataRoot = "Assets/GameExample/LittleFighter/data";
        private const string DbPath = "Assets/_Project/Gameplay/LF2/Data/LF2CharacterDatabase.asset";

        private static readonly Dictionary<int, string> TargetCharacters = new Dictionary<int, string>
        {
            { 7, "firen.dat" },
            { 6, "louis.dat" },
            { 5, "rudolf.dat" },
            { 4, "henry.dat" }
        };

        private static readonly Dictionary<int, string[]> ProjectileFiles = new Dictionary<int, string[]>
        {
            { 7, new[] { "firen_ball.dat", "firen_flame.dat" } },
            { 5, new[] { "rudolf_weapon.dat" } },
            { 4, new[] { "henry_arrow1.dat", "henry_arrow2.dat", "henry_wind.dat" } }
        };

        [MenuItem("_Project/LF2/Verify Wave 7.3 (Firen, Louis, Rudolf, Henry)")]
        public static void VerifyWave73()
        {
            var dataFolder = Path.GetFullPath(Path.Combine(Application.dataPath, "..", DataRoot));
            var errors = new List<string>();
            var allOk = true;

            foreach (var kv in TargetCharacters)
            {
                var datPath = Path.Combine(dataFolder, kv.Value);
                if (!File.Exists(datPath))
                {
                    Debug.LogError($"[Wave73] {kv.Value} not found at {datPath}");
                    allOk = false;
                    continue;
                }

                var bytes = File.ReadAllBytes(datPath);
                var data = Lf2DatRuntimeLoader.LoadFromBytes(bytes);

                if (data == null || data.Frames == null || data.Frames.Count == 0)
                {
                    Debug.LogError($"[Wave73] {kv.Value}: parse failed or 0 frames");
                    allOk = false;
                    continue;
                }

                Debug.Log($"[Wave73] === {data.Name} (id={kv.Key}, file={kv.Value}) ===");
                Debug.Log($"  Frames: {data.Frames.Count}, BmpEntries: {data.BmpEntries?.Count ?? 0}");

                var roles = data.RoleIds;
                if (roles == null)
                {
                    Debug.LogError($"  RoleIds is null!");
                    allOk = false;
                    continue;
                }

                Debug.Log($"  Standing={roles.Standing} Walking={roles.Walking} Running={roles.Running}");
                Debug.Log($"  Jump={roles.Jump} Defend={roles.Defend} Lying={roles.Lying}");
                Debug.Log($"  AttackNeutral={roles.AttackNeutral} AttackForward={roles.AttackForward} AttackBack={roles.AttackBack}");
                Debug.Log($"  EnergyBlast={roles.EnergyBlast} Shrafe={roles.Shrafe} LeapAttack={roles.LeapAttack} DragonPunch={roles.DragonPunch}");

                var issues = new List<string>();
                ValidateFrame(data, roles.Standing, "Standing", issues);
                ValidateFrame(data, roles.Walking, "Walking", issues);
                ValidateFrame(data, roles.Running, "Running", issues);
                ValidateFrame(data, roles.Jump, "Jump", issues);
                ValidateFrame(data, roles.Defend, "Defend", issues);
                ValidateFrame(data, roles.AttackNeutral, "AttackNeutral", issues);

                if (roles.EnergyBlast == 200 && !data.Frames.ContainsKey(200))
                    issues.Add($"EnergyBlast not detected (default 200 not in frames)");
                if (roles.Shrafe == 210 && !data.Frames.ContainsKey(210))
                    issues.Add($"Shrafe not detected (default 210 not in frames)");

                var opointFrames = new List<string>();
                foreach (var fk in data.Frames)
                {
                    if (fk.Value.Opoints != null && fk.Value.Opoints.Length > 0)
                        opointFrames.Add($"{fk.Key}({fk.Value.Name})");
                }
                Debug.Log($"  Opoint frames [{opointFrames.Count}]: {string.Join(", ", opointFrames)}");

                var itrFrames = new List<string>();
                foreach (var fk in data.Frames)
                {
                    if (fk.Value.Itrs != null && fk.Value.Itrs.Length > 0)
                        itrFrames.Add($"{fk.Key}({fk.Value.Name},state={fk.Value.State})");
                }
                Debug.Log($"  Itr frames [{itrFrames.Count}]: {string.Join(", ", itrFrames)}");

                if (issues.Count > 0)
                {
                    foreach (var issue in issues)
                        Debug.LogWarning($"  ISSUE: {issue}");
                    allOk = false;
                }
                else
                {
                    Debug.Log($"  All critical frames present and valid!");
                }
            }

            if (ProjectileFiles.Count > 0)
            {
                Debug.Log("[Wave73] --- Projectile/Object DAT files ---");
                foreach (var kv in ProjectileFiles)
                {
                    foreach (var projFile in kv.Value)
                    {
                        var projPath = Path.Combine(dataFolder, projFile);
                        if (!File.Exists(projPath))
                        {
                            Debug.LogWarning($"[Wave73] Projectile {projFile} not found");
                            continue;
                        }
                        var projBytes = File.ReadAllBytes(projPath);
                        var projData = Lf2DatRuntimeLoader.LoadFromBytes(projBytes);
                        if (projData == null || projData.Frames == null || projData.Frames.Count == 0)
                        {
                            Debug.LogWarning($"[Wave73] Projectile {projFile}: parse failed or 0 frames");
                        }
                        else
                        {
                            Debug.Log($"[Wave73] Projectile {projFile}: {projData.Frames.Count} frames OK");
                        }
                    }
                }
            }

            var db = AssetDatabase.LoadAssetAtPath<Lf2CharacterDatabase>(DbPath);
            if (db == null)
            {
                Debug.Log("[Wave73] Database asset not found. Running batch import...");
                Lf2BatchImportEditor.ImportAllCharacters();
                db = AssetDatabase.LoadAssetAtPath<Lf2CharacterDatabase>(DbPath);
            }

            if (db != null)
            {
                Debug.Log($"[Wave73] Database has {db.Count} characters");
                foreach (var kv in TargetCharacters)
                {
                    bool has = db.HasCharacter(kv.Key);
                    Debug.Log($"  id={kv.Key} ({kv.Value}): {(has ? "PRESENT" : "MISSING")}");
                    if (!has) allOk = false;
                }
            }
            else
            {
                Debug.LogError("[Wave73] Failed to create/load database!");
                allOk = false;
            }

            if (allOk)
                Debug.Log("[Wave73] ALL 4 CHARACTERS (Firen, Louis, Rudolf, Henry) VERIFIED SUCCESSFULLY");
            else
                Debug.LogWarning("[Wave73] SOME ISSUES FOUND - see warnings above");
        }

        private static void ValidateFrame(Lf2CharacterData data, int frameId, string label, List<string> issues)
        {
            if (!data.Frames.ContainsKey(frameId))
                issues.Add($"{label} frame {frameId} not found in character frames!");
        }
    }
}
