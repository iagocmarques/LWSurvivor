using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Project.Gameplay.LF2;

namespace Project.EditorTools
{
    public static class Lf2BatchImportEditor
    {
        private const string DataRoot = "Assets/GameExample/LittleFighter/data";
        private const string OutputDir = "Assets/_Project/Gameplay/LF2/Data";
        private const string OutputPath = OutputDir + "/LF2CharacterDatabase.asset";

        private static readonly Dictionary<int, string> CharacterFiles = new Dictionary<int, string>
        {
            { 0, "template.dat" },
            { 1, "deep.dat" },
            { 2, "john.dat" },
            { 4, "henry.dat" },
            { 5, "rudolf.dat" },
            { 6, "louis.dat" },
            { 7, "firen.dat" },
            { 8, "freeze.dat" },
            { 9, "dennis.dat" },
            { 10, "woody.dat" },
            { 11, "davis.dat" },
            { 30, "bandit.dat" },
            { 31, "hunter.dat" },
            { 32, "mark.dat" },
            { 33, "jack.dat" },
            { 34, "sorcerer.dat" },
            { 35, "monk.dat" },
            { 36, "jan.dat" },
            { 37, "knight.dat" },
            { 38, "bat.dat" },
            { 39, "justin.dat" },
            { 50, "louisEX.dat" },
            { 51, "firzen.dat" },
            { 52, "julian.dat" }
        };

        [MenuItem("_Project/LF2/Import All Characters")]
        public static void ImportAllCharacters()
        {
            var dataFolder = Path.GetFullPath(Path.Combine(Application.dataPath, "..", DataRoot));
            if (!Directory.Exists(dataFolder))
            {
                EditorUtility.DisplayDialog("LF2 Batch Import",
                    $"Data folder not found:\n{dataFolder}", "OK");
                return;
            }

            var db = ScriptableObject.CreateInstance<Lf2CharacterDatabase>();
            db.characters = new List<Lf2CharacterDatabase.CharacterEntry>();

            var errors = new List<string>();
            var imported = 0;

            foreach (var kv in CharacterFiles)
            {
                var datPath = Path.Combine(dataFolder, kv.Value);
                if (!File.Exists(datPath))
                {
                    errors.Add($"ID {kv.Key}: file not found ({kv.Value})");
                    continue;
                }

                try
                {
                    var bytes = File.ReadAllBytes(datPath);
                    var data = Lf2DatRuntimeLoader.LoadFromBytes(bytes);

                    if (data == null || data.Frames == null || data.Frames.Count == 0)
                    {
                        errors.Add($"ID {kv.Key}: parsed but no frames ({kv.Value})");
                        continue;
                    }

                    var name = string.IsNullOrEmpty(data.Name) ? kv.Value.Replace(".dat", "") : data.Name;

                    db.characters.Add(new Lf2CharacterDatabase.CharacterEntry
                    {
                        id = kv.Key,
                        characterName = name,
                        datBytes = bytes
                    });

                    imported++;
                    Debug.Log($"[LF2BatchImport] {name} (id={kv.Key}): {data.Frames.Count} frames, {data.BmpEntries?.Count ?? 0} bmp entries.");
                }
                catch (System.Exception ex)
                {
                    errors.Add($"ID {kv.Key}: {ex.Message} ({kv.Value})");
                }
            }

            if (!Directory.Exists(Path.GetFullPath(Path.Combine(Application.dataPath, "..", OutputDir))))
            {
                Directory.CreateDirectory(Path.GetFullPath(Path.Combine(Application.dataPath, "..", OutputDir)));
                AssetDatabase.Refresh();
            }

            var existing = AssetDatabase.LoadAssetAtPath<Lf2CharacterDatabase>(OutputPath);
            if (existing != null)
            {
                EditorUtility.CopySerialized(db, existing);
                AssetDatabase.SaveAssets();
                Object.DestroyImmediate(db);
                db = existing;
            }
            else
            {
                AssetDatabase.CreateAsset(db, OutputPath);
                AssetDatabase.SaveAssets();
            }

            AssetDatabase.Refresh();

            var msg = $"Imported {imported}/{CharacterFiles.Count} characters.\nDatabase: {OutputPath}";
            if (errors.Count > 0)
            {
                msg += "\n\nErrors:\n" + string.Join("\n", errors);
                Debug.LogWarning("[LF2BatchImport] Errors during import:\n" + string.Join("\n", errors));
            }

            EditorUtility.DisplayDialog("LF2 Batch Import", msg, "OK");
            Debug.Log($"[LF2BatchImport] Complete. {imported}/{CharacterFiles.Count} characters imported.");
        }
    }
}
