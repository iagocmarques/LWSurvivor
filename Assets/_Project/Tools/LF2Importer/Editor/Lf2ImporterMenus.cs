using System.IO;
using System.Linq;
using LF2Importer;
using UnityEditor;
using UnityEngine;

namespace LF2Importer.EditorTools
{
    public static class Lf2ImporterMenus
    {
        private const string MenuRoot = "Tools/LF2/";
        private const string DefaultSettingsPath = "Assets/_Project/Tools/LF2Importer/Lf2ImportSettings.asset";

        [MenuItem(MenuRoot + "Create Default Import Settings", false, 0)]
        private static void CreateDefaultSettings()
        {
            if (AssetDatabase.LoadAssetAtPath<Lf2ImportSettings>(DefaultSettingsPath) != null)
            {
                EditorUtility.DisplayDialog("LF2 Importer", "O asset já existe:\n" + DefaultSettingsPath, "OK");
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(DefaultSettingsPath);
                return;
            }

            var dir = Path.GetDirectoryName(DefaultSettingsPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var s = ScriptableObject.CreateInstance<Lf2ImportSettings>();
            s.lf2GameRootPath = Path.GetFullPath(Path.Combine(Application.dataPath, "GameExample", "LittleFighter"));
            s.outputRootPath = "Assets/_Imported/LF2";
            s.convertedSpritesRoot = "Assets/Art/lf2_ref/characters";
            s.timeUnitSeconds = 1f / 30f;
            s.importSprites = true;
            s.importClips = true;
            s.importData = true;
            AssetDatabase.CreateAsset(s, DefaultSettingsPath);
            AssetDatabase.SaveAssets();
            Selection.activeObject = s;
            EditorUtility.DisplayDialog("LF2 Importer", "Criado:\n" + DefaultSettingsPath + "\n\nAjuste lf2GameRootPath se o pack LF2 estiver em outro lugar.", "OK");
        }

        [MenuItem(MenuRoot + "Import Character (Dennis id=9) — teste", false, 20)]
        private static void ImportDennisTest()
        {
            var settings = LoadSettings();
            if (settings == null)
                return;
            var dataTxt = Path.Combine(settings.lf2GameRootPath, "data", "data.txt");
            var objs = Lf2DataTxtParser.ReadObjects(dataTxt);
            var entry = objs.FirstOrDefault(o => o.id == 9);
            if (entry == null)
            {
                EditorUtility.DisplayDialog("LF2 Importer", "Personagem id=9 não encontrado em data.txt.", "OK");
                return;
            }

            var r = Lf2CharacterImportPipeline.ImportSingle(settings, entry, objs);
            if (r.success)
            {
                Debug.Log("[LF2Importer] " + r.message);
                Selection.activeObject = r.character;
            }
            else
            {
                EditorUtility.DisplayDialog("LF2 Importer", r.message, "OK");
            }
        }

        [MenuItem(MenuRoot + "Import All Characters (data.txt)", false, 21)]
        private static void ImportAll()
        {
            var settings = LoadSettings();
            if (settings == null)
                return;
            if (!EditorUtility.DisplayDialog(
                    "LF2 Importer",
                    "Importar todos os type:0 de data.txt? Pode levar vários minutos.",
                    "Importar",
                    "Cancelar"))
                return;

            try
            {
                var (ok, fail) = Lf2CharacterImportPipeline.ImportAllCharacters(settings, (t, label) =>
                {
                    EditorUtility.DisplayProgressBar("LF2 Importer", label, t);
                });
                var reportRel = (string.IsNullOrEmpty(settings.outputRootPath) ? "Assets/_Imported/LF2" : settings.outputRootPath).Replace('\\', '/') + "/import_report.md";
                EditorUtility.DisplayDialog(
                    "LF2 Importer",
                    $"Concluído.\nOK: {ok}\nFalhas: {fail}\n\nRelatório:\n{reportRel}",
                    "OK");
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        [MenuItem(MenuRoot + "Debug: imprimir primeiras linhas de .dat (escolher arquivo)", false, 40)]
        private static void DebugPrintDat()
        {
            var p = EditorUtility.OpenFilePanel("Escolher .dat", Application.dataPath, "dat");
            if (string.IsNullOrEmpty(p))
                return;
            Lf2DatDecryptor.DebugPrintFirstLines(p, 45);
        }

        private static Lf2ImportSettings LoadSettings()
        {
            var s = AssetDatabase.LoadAssetAtPath<Lf2ImportSettings>(DefaultSettingsPath);
            if (s != null)
                return s;
            s = AssetDatabase.FindAssets("t:Lf2ImportSettings")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Lf2ImportSettings>)
                .FirstOrDefault(x => x != null);
            if (s != null)
                return s;
            EditorUtility.DisplayDialog(
                "LF2 Importer",
                "Crie primeiro o asset em:\nTools/LF2/Create Default Import Settings",
                "OK");
            return null;
        }
    }
}
