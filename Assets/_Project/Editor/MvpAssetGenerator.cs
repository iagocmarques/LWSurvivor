using Project.Data;
using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    public static class MvpAssetGenerator
    {
        private const string DataFolder = "Assets/_Project/Data";

        [MenuItem("Tools/LF2/Generate MVP Assets", false, 100)]
        public static void GenerateAll()
        {
            EnsureFolder(DataFolder);
            CreateCharacterDefinitionDavis();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[MvpAssetGenerator] All MVP assets generated under " + DataFolder);
        }

        private static void CreateCharacterDefinitionDavis()
        {
            const string path = DataFolder + "/CharacterDefinition_Davis.asset";
            var so = LoadOrCreate<CharacterDefinition>(path);
            so.lf2Id = 0;
            so.displayName = "Davis";
            EditorUtility.SetDirty(so);
        }

        private static T LoadOrCreate<T>(string assetPath) where T : ScriptableObject
        {
            var existing = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (existing != null)
                return existing;

            var instance = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(instance, assetPath);
            return instance;
        }

        private static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path))
                return;

            var parts = path.Split('/');
            var current = parts[0];
            for (var i = 1; i < parts.Length; i++)
            {
                var next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(current, parts[i]);
                current = next;
            }
        }
    }
}
