using LF2Importer.EditorTools;
using UnityEditor;

/// <summary>
/// Entrada CLI para batchmode: <c>RunDecryptSmoke</c>, <c>RunDennisImport</c>, <c>RunImportAll</c> (<c>-executeMethod Lf2ImporterCliEntry.*</c>).
/// (nome global curto — o Unity falha a resolver alguns namespaces aninhados com "LF2Importer...").
/// </summary>
public static class Lf2ImporterCliEntry
{
    public static void RunDecryptSmoke() => Lf2ImporterBatchTest.RunDecryptSmoke();

    public static void RunDennisImport() => Lf2ImporterBatchTest.RunDennisImport();

    public static void RunImportAll() => Lf2ImporterBatchTest.RunImportAll();
}
