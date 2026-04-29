using UnityEngine;

namespace LF2Importer
{
    [CreateAssetMenu(fileName = "Lf2ImportSettings", menuName = "LF2 Importer/Import Settings", order = 0)]
    public sealed class Lf2ImportSettings : ScriptableObject
    {
        [Tooltip("Pasta raiz do jogo LF2 (contém data/, sprite/). Ex.: Assets/GameExample/LittleFighter")]
        public string lf2GameRootPath = "";

        [Tooltip("Saída gerada pelo importador (sprites/clips/SOs). Não versionar conteúdo proprietário.")]
        public string outputRootPath = "Assets/_Imported/LF2";

        [Tooltip("Pasta com PNGs já convertidos (prepare_lf2_assets). Sprites nomeados {sheet}_{index}.")]
        public string convertedSpritesRoot = "Assets/Art/lf2_ref/characters";

        [Tooltip("Unidade de tempo LF2: duração de um tick wait (segundos).")]
        public float timeUnitSeconds = 1f / 30f;

        public bool importSprites = true;
        public bool importClips = true;
        public bool importData = true;

        [Tooltip("Salvar texto .dat decriptado em Raw/ para debug.")]
        public bool writeDecryptedDebugCopy;
    }
}
