using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using Project.Data;
using Project.Gameplay.LF2;
using Project.Gameplay.Combat;

namespace LF2Importer.EditorTools
{
    public static class Lf2BatchCharacterImporter
    {
        public struct CharacterSpec
        {
            public int id;
            public string displayName;
            public string datFileName;
        }

        public static readonly CharacterSpec[] Batch1 =
        {
            new CharacterSpec { id = 1,  displayName = "Deep",   datFileName = "deep" },
            new CharacterSpec { id = 2,  displayName = "John",   datFileName = "john" },
            new CharacterSpec { id = 4,  displayName = "Woody",  datFileName = "woody" },
            new CharacterSpec { id = 5,  displayName = "Dennis", datFileName = "dennis" },
            new CharacterSpec { id = 6,  displayName = "Freeze", datFileName = "freeze" },
            new CharacterSpec { id = 7,  displayName = "Firen",  datFileName = "firen" },
            new CharacterSpec { id = 8,  displayName = "Louis",  datFileName = "louis" },
            new CharacterSpec { id = 9,  displayName = "Rudolf", datFileName = "rudolf" },
            new CharacterSpec { id = 10, displayName = "Henry",  datFileName = "henry" },
        };

        private const string CharacterOutputDir = "Assets/_Project/Data/Characters";
        private const string DatabasePath = "Assets/_Project/Data/Lf2CharacterDatabase.asset";
        private const string SharedReactivePath = CharacterOutputDir + "/ReactiveMoveSet_Shared.asset";

        [MenuItem("LF2/Import Batch 1 Characters")]
        public static void ImportBatch1Menu()
        {
            var (ok, fail, report) = ImportBatch1();
            Debug.Log($"[LF2BatchImporter] Done. ok={ok} fail={fail}");
            Debug.Log(report);
        }

        public static (int ok, int fail, string report) ImportBatch1(
            string datRoot = null,
            string outputDir = null,
            string databasePath = null)
        {
            if (string.IsNullOrEmpty(datRoot))
                datRoot = Path.GetFullPath(Path.Combine(Application.dataPath, "_Project", "Resources", "LF2"));
            if (string.IsNullOrEmpty(outputDir))
                outputDir = CharacterOutputDir;
            if (string.IsNullOrEmpty(databasePath))
                databasePath = DatabasePath;

            EnsureUnityDir(outputDir);

            var reactiveMoves = CreateOrLoadSharedReactiveMoveSet(SharedReactivePath);

            var database = AssetDatabase.LoadAssetAtPath<Lf2CharacterDatabase>(databasePath);
            if (database == null)
            {
                database = ScriptableObject.CreateInstance<Lf2CharacterDatabase>();
                AssetDatabase.CreateAsset(database, databasePath);
            }
            database.characters.Clear();
            database.ClearCache();

            var sb = new StringBuilder();
            sb.AppendLine("# LF2 Batch 1 — Character Import Report");
            sb.AppendLine();
            sb.AppendLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"DAT root: `{datRoot}`");
            sb.AppendLine($"Output: `{outputDir}`");
            sb.AppendLine();

            int ok = 0, fail = 0;

            foreach (var spec in Batch1)
            {
                var datPath = Path.Combine(datRoot, spec.datFileName + ".dat");
                try
                {
                    if (!File.Exists(datPath))
                    {
                        sb.AppendLine($"## SKIP id={spec.id} {spec.displayName}");
                        sb.AppendLine($"- .dat not found: `{datPath}`");
                        fail++;
                        continue;
                    }

                    var rawBytes = File.ReadAllBytes(datPath);
                    var charData = Lf2DatRuntimeLoader.LoadFromBytes(rawBytes);

                    var assetPath = $"{outputDir}/{spec.displayName}.asset";
                    var existing = AssetDatabase.LoadAssetAtPath<CharacterDefinition>(assetPath);
                    CharacterDefinition def;
                    if (existing != null)
                    {
                        def = existing;
                    }
                    else
                    {
                        def = ScriptableObject.CreateInstance<CharacterDefinition>();
                        AssetDatabase.CreateAsset(def, assetPath);
                    }

                    def.BuildFromLf2Data(spec.id, charData, reactiveMoves, rawBytes);
                    EditorUtility.SetDirty(def);

                    database.characters.Add(new Lf2CharacterDatabase.CharacterEntry
                    {
                        id = spec.id,
                        characterName = spec.displayName,
                        datBytes = rawBytes
                    });

                    var roles = def.frameRoleIds;
                    sb.AppendLine($"## OK id={spec.id} {spec.displayName}");
                    sb.AppendLine($"- lf2Id: {def.lf2Id}");
                    sb.AppendLine($"- displayName: {def.displayName}");
                    sb.AppendLine($"- movement: walk={def.movement.walkSpeed:F1} run={def.movement.runSpeed:F1} jump={def.movement.jumpPower:F1} gravity={def.movement.gravity:F2}");
                    sb.AppendLine($"- frameRoleIds: Standing={roles.Standing} Walking={roles.Walking} Running={roles.Running} Jump={roles.Jump} Defend={roles.Defend} Lying={roles.Lying}");
                    sb.AppendLine($"- attackRoles: Neutral={roles.AttackNeutral} Forward={roles.AttackForward} Back={roles.AttackBack}");
                    sb.AppendLine($"- specialRoles: EnergyBlast={roles.EnergyBlast} Shrafe={roles.Shrafe} LeapAttack={roles.LeapAttack} DragonPunch={roles.DragonPunch}");
                    sb.AppendLine($"- frames: {charData.Frames?.Count ?? 0}");
                    sb.AppendLine($"- reactiveMoves: {(def.reactiveMoves != null ? "set" : "null")}");
                    sb.AppendLine($"- rawDatBytes: {rawBytes.Length} bytes");
                    sb.AppendLine();
                    ok++;
                }
                catch (Exception ex)
                {
                    sb.AppendLine($"## ERROR id={spec.id} {spec.displayName}");
                    sb.AppendLine($"- {ex.Message}");
                    fail++;
                }
            }

            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            sb.AppendLine("---");
            sb.AppendLine($"**Summary: ok={ok} fail={fail} total={Batch1.Length}**");

            var reportText = sb.ToString();
            var reportPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", outputDir, "batch1_report.md"));
            File.WriteAllText(reportPath, reportText, new UTF8Encoding(false));
            Debug.Log($"[LF2BatchImporter] Report saved: {outputDir}/batch1_report.md");

            return (ok, fail, reportText);
        }

        public static string VerifyBatch1(string databasePath = null)
        {
            if (string.IsNullOrEmpty(databasePath))
                databasePath = DatabasePath;

            var sb = new StringBuilder();
            sb.AppendLine("# Batch 1 Verification");
            sb.AppendLine();

            var database = AssetDatabase.LoadAssetAtPath<Lf2CharacterDatabase>(databasePath);
            if (database == null)
            {
                sb.AppendLine("FAIL: Lf2CharacterDatabase not found at " + databasePath);
                return sb.ToString();
            }

            sb.AppendLine($"Database entries: {database.Count}");
            sb.AppendLine();

            int pass = 0, fail = 0;

            foreach (var spec in Batch1)
            {
                var assetPath = $"{CharacterOutputDir}/{spec.displayName}.asset";
                var def = AssetDatabase.LoadAssetAtPath<CharacterDefinition>(assetPath);

                var issues = new List<string>();

                if (def == null)
                    issues.Add("CharacterDefinition asset missing");
                else
                {
                    if (def.lf2Id != spec.id)
                        issues.Add($"lf2Id mismatch: expected {spec.id} got {def.lf2Id}");
                    if (string.IsNullOrEmpty(def.displayName))
                        issues.Add("displayName is empty");
                    if (def.frameRoleIds == null)
                        issues.Add("frameRoleIds is null");
                    if (def.reactiveMoves == null)
                        issues.Add("reactiveMoves is null");
                    if (def.rawDatBytes == null || def.rawDatBytes.Length == 0)
                        issues.Add("rawDatBytes is empty");
                }

                var hasDbEntry = database.HasCharacter(spec.id);

                if (issues.Count == 0 && hasDbEntry)
                {
                    sb.AppendLine($"PASS id={spec.id} {spec.displayName}");
                    pass++;
                }
                else
                {
                    sb.AppendLine($"FAIL id={spec.id} {spec.displayName}");
                    foreach (var issue in issues)
                        sb.AppendLine($"  - {issue}");
                    if (!hasDbEntry)
                        sb.AppendLine($"  - Missing from Lf2CharacterDatabase");
                    fail++;
                }
            }

            sb.AppendLine();
            sb.AppendLine($"**Verify: pass={pass} fail={fail}**");

            var reportText = sb.ToString();
            Debug.Log(reportText);
            return reportText;
        }

        private static ReactiveMoveSet CreateOrLoadSharedReactiveMoveSet(string assetPath)
        {
            var existing = AssetDatabase.LoadAssetAtPath<ReactiveMoveSet>(assetPath);
            if (existing != null)
                return existing;

            var so = ScriptableObject.CreateInstance<ReactiveMoveSet>();

            so.defend = new ReactiveMoveDefinition
            {
                stateId = ReactiveStateId.Defend,
                frames = new[] { new ReactiveFrameDefinition { durationTicks = 1, lockInput = true } },
                loop = true,
                nextStateOnFinish = ReactiveStateId.Defend
            };
            so.defendBreak = new ReactiveMoveDefinition
            {
                stateId = ReactiveStateId.DefendBreak,
                frames = new[] { new ReactiveFrameDefinition { durationTicks = 30, lockInput = true } },
                nextStateOnFinish = ReactiveStateId.GetUp
            };
            so.defendHit = new ReactiveMoveDefinition
            {
                stateId = ReactiveStateId.DefendHit,
                frames = new[] { new ReactiveFrameDefinition { durationTicks = 10, lockInput = true } },
                nextStateOnFinish = ReactiveStateId.Defend
            };
            so.hurtGrounded = new ReactiveMoveDefinition
            {
                stateId = ReactiveStateId.HurtGrounded,
                frames = new[] { new ReactiveFrameDefinition { durationTicks = 10, lockInput = true } },
                nextStateOnFinish = ReactiveStateId.GetUp
            };
            so.hurtAir = new ReactiveMoveDefinition
            {
                stateId = ReactiveStateId.HurtAir,
                frames = new[] { new ReactiveFrameDefinition { durationTicks = 15, lockInput = true } },
                nextStateOnFinish = ReactiveStateId.Lying
            };
            so.lying = new ReactiveMoveDefinition
            {
                stateId = ReactiveStateId.Lying,
                frames = new[] { new ReactiveFrameDefinition { durationTicks = 60, lockInput = true, invulnerable = true } },
                nextStateOnFinish = ReactiveStateId.GetUp
            };
            so.getUp = new ReactiveMoveDefinition
            {
                stateId = ReactiveStateId.GetUp,
                frames = new[] { new ReactiveFrameDefinition { durationTicks = 15, lockInput = true, invulnerable = true } },
                nextStateOnFinish = ReactiveStateId.Defend
            };
            so.dead = new ReactiveMoveDefinition
            {
                stateId = ReactiveStateId.Dead,
                frames = new[] { new ReactiveFrameDefinition { durationTicks = 999, lockInput = true, invulnerable = true } },
                loop = true,
                nextStateOnFinish = ReactiveStateId.Dead
            };

            AssetDatabase.CreateAsset(so, assetPath);
            return so;
        }

        private static void EnsureUnityDir(string unityPath)
        {
            var full = Path.GetFullPath(Path.Combine(Application.dataPath, "..", unityPath.Replace('/', Path.DirectorySeparatorChar)));
            if (!Directory.Exists(full))
                Directory.CreateDirectory(full);
        }
    }
}
