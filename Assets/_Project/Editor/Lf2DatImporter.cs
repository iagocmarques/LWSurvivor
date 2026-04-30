using System.IO;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Project.EditorTools
{
    /// <summary>
    /// Imports .dat files (LF2 character/projectile data) as TextAsset
    /// so Resources.Load&lt;TextAsset&gt;("LF2/davis") works correctly.
    /// </summary>
    [ScriptedImporter(1, "dat")]
       public class Lf2DatImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var bytes = File.ReadAllBytes(ctx.assetPath);
            var textAsset = new TextAsset(bytes);
            ctx.AddObjectToAsset("main", textAsset);
            ctx.SetMainObject(textAsset);
        }
    }
}
