using System.IO;
using System.Linq;
using LF2Importer;
using UnityEditor;
using UnityEngine;

namespace LF2Importer.EditorTools
{
    /// <summary>
    /// Testes automatizados via linha de comando (CI ou Cursor).
    /// Batch: <c>Lf2ImporterCliEntry.RunDecryptSmoke</c>, <c>RunDennisImport</c>, <c>RunImportAll</c> (ver <c>Assets/Editor/Lf2ImporterCliEntry.cs</c>).
    /// Nota: a classe não pode ser <c>static</c> — o Unity falha a resolver <c>-executeMethod</c> nesse caso.
    /// </summary>
    public sealed class Lf2ImporterBatchTest
    {
        private const string LogP = "[LF2ImporterBatchTest]";

        /// <summary>
        /// Sai no próximo tick do editor para evitar processo a terminar com código 1 em batchmode
        /// (shutdown imediato após <c>-executeMethod</c> pode deixar alocadores temporários e relatório de erro nativo).
        /// </summary>
        private static void ExitDeferred(int code)
        {
            WriteBatchExitCodeFile(code);
            EditorApplication.delayCall += () =>
                EditorApplication.delayCall += () => EditorApplication.Exit(code);
        }

        /// <summary>
        /// O Unity por vezes não descarrega o <c>-logFile</c> a tempo para o script PowerShell ler a última linha;
        /// este ficheiro é escrito de forma síncrona para o CI/script validar o código de saída real.
        /// </summary>
        private static void WriteBatchExitCodeFile(int code)
        {
            try
            {
                var logsDir = Path.GetFullPath(Path.Combine(Application.dataPath, "..", "Logs"));
                Directory.CreateDirectory(logsDir);
                var path = Path.Combine(logsDir, "lf2_batch_last_exit.txt");
                File.WriteAllText(path, code.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            catch (IOException)
            {
                // ignorar — o EditorApplication.Exit ainda corre
            }
        }

        private static Lf2ImportSettings CreateBatchImportSettings(string lf2Root)
        {
            var settings = ScriptableObject.CreateInstance<Lf2ImportSettings>();
            settings.lf2GameRootPath = lf2Root;
            settings.outputRootPath = "Assets/_Imported/LF2";
            settings.convertedSpritesRoot = "Assets/Art/lf2_ref/characters";
            settings.timeUnitSeconds = 1f / 30f;
            settings.importSprites = true;
            settings.importClips = true;
            settings.importData = true;
            settings.writeDecryptedDebugCopy = false;
            return settings;
        }

        public static void RunDennisImport()
        {
            var lf2Root = Path.GetFullPath(Path.Combine(Application.dataPath, "GameExample", "LittleFighter"));
            Debug.Log($"{LogP} lf2Root={lf2Root} exists={Directory.Exists(lf2Root)}");

            if (!Directory.Exists(lf2Root))
            {
                Debug.LogError($"{LogP} Pasta LF2 não encontrada (esperada em Assets/GameExample/LittleFighter).");
                ExitDeferred(1);
                return;
            }

            var dataTxt = Path.Combine(lf2Root, "data", "data.txt");
            if (!File.Exists(dataTxt))
            {
                Debug.LogError($"{LogP} data.txt não encontrado: {dataTxt}");
                ExitDeferred(2);
                return;
            }

            var settings = CreateBatchImportSettings(lf2Root);

            var objs = Lf2DataTxtParser.ReadObjects(dataTxt);
            var entry = objs.FirstOrDefault(o => o.id == 9);
            if (entry == null)
            {
                Debug.LogError($"{LogP} Personagem id=9 não encontrado em data.txt.");
                ExitDeferred(3);
                return;
            }

            var result = Lf2CharacterImportPipeline.ImportSingle(settings, entry, objs);
            Debug.Log($"{LogP} Import success={result.success} message={result.message}");
            foreach (var w in result.lines)
                Debug.Log($"{LogP} warning: {w}");

            if (result.character != null)
            {
                Debug.Log($"{LogP} standing={result.character.clipStanding != null} walking={result.character.clipWalking != null} " +
                          $"running={result.character.clipRunning != null} defend={result.character.clipDefend != null} " +
                          $"extraClips={result.character.extraClips.Count} moves={result.character.moves.Count} frames={result.character.frameRows.Count}");
            }

            AssetDatabase.Refresh();
            ExitDeferred(result.success ? 0 : 4);
        }

        /// <summary>Importa todos os type:0 de data.txt (igual ao menu Tools/LF2/Import All).</summary>
        public static void RunImportAll()
        {
            var lf2Root = Path.GetFullPath(Path.Combine(Application.dataPath, "GameExample", "LittleFighter"));
            Debug.Log($"{LogP} RunImportAll lf2Root={lf2Root}");

            if (!Directory.Exists(lf2Root))
            {
                Debug.LogError($"{LogP} Pasta LF2 não encontrada (esperada em Assets/GameExample/LittleFighter).");
                ExitDeferred(1);
                return;
            }

            var dataTxt = Path.Combine(lf2Root, "data", "data.txt");
            if (!File.Exists(dataTxt))
            {
                Debug.LogError($"{LogP} data.txt não encontrado: {dataTxt}");
                ExitDeferred(2);
                return;
            }

            var settings = CreateBatchImportSettings(lf2Root);
            var (ok, fail) = Lf2CharacterImportPipeline.ImportAllCharacters(settings, null);
            Debug.Log($"{LogP} ImportAll summary ok={ok} fail={fail}");
            ExitDeferred(fail > 0 ? 6 : 0);
        }

        /// <summary>Decripta e confirma marcadores no texto (sem gravar assets).</summary>
        public static void RunDecryptSmoke()
        {
            var lf2Root = Path.GetFullPath(Path.Combine(Application.dataPath, "GameExample", "LittleFighter"));
            var dat = Path.Combine(lf2Root, "data", "dennis.dat");
            if (!File.Exists(dat))
            {
                Debug.LogError($"{LogP} dennis.dat não encontrado: {dat}");
                ExitDeferred(1);
                return;
            }

            var text = Lf2DatDecryptor.ReadDatAsText(dat);
            var hasBmp = text.IndexOf("<bmp_begin>", System.StringComparison.OrdinalIgnoreCase) >= 0;
            var frameCount = 0;
            for (var i = 0; i < text.Length; i++)
            {
                if (i + 7 <= text.Length && string.Compare(text, i, "<frame>", 0, 7, System.StringComparison.OrdinalIgnoreCase) == 0)
                    frameCount++;
            }

            Debug.Log($"{LogP} decrypt smoke: len={text.Length} hasBmpBegin={hasBmp} frameTagOccurrences={frameCount}");
            var parsed = Lf2DatParser.ParseText(text, dat);
            Debug.Log($"{LogP} parsed frames={parsed.frames.Count} warnings={parsed.parseWarnings.Count}");
            ExitDeferred(hasBmp && parsed.frames.Count > 50 ? 0 : 5);
        }
    }
}
