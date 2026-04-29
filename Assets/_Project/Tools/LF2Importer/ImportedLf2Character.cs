using System;
using System.Collections.Generic;
using UnityEngine;

namespace LF2Importer
{
    [CreateAssetMenu(fileName = "ImportedLf2Character", menuName = "LF2 Importer/Imported Character (LF2)", order = 1)]
    public sealed class ImportedLf2Character : ScriptableObject
    {
        public int dataTxtId;
        public string sourceDatRelativePath = "";
        public string displayName = "";
        public string headSpritePath = "";
        public string smallSpritePath = "";

        public List<string> importWarnings = new List<string>();

        public List<Lf2BmpSheetInfo> bmpSheets = new List<Lf2BmpSheetInfo>();
        public List<Lf2MovementParam> movementParams = new List<Lf2MovementParam>();

        public List<ImportedLf2Move> moves = new List<ImportedLf2Move>();
        public List<ImportedLf2FrameRow> frameRows = new List<ImportedLf2FrameRow>();

        public AnimationClip clipStanding;
        public AnimationClip clipWalking;
        public AnimationClip clipRunning;
        public AnimationClip clipDefend;

        [Tooltip("Clips extras: dash, punch chain, especiais, etc.")]
        public List<AnimationClip> extraClips = new List<AnimationClip>();
    }

    [Serializable]
    public struct Lf2BmpSheetInfo
    {
        public int picStart;
        public int picEnd;
        public string sourcePath;
        public int cellW;
        public int cellH;
        public int row;
        public int col;
    }

    [Serializable]
    public struct Lf2MovementParam
    {
        public string key;
        public float amount;
    }

    [Serializable]
    public sealed class ImportedLf2Move
    {
        public string name;
        public int startFrameId;
        public List<int> frameSequence = new List<int>();
        public List<Lf2InputBinding> inputBindings = new List<Lf2InputBinding>();
    }

    [Serializable]
    public struct Lf2InputBinding
    {
        public string hitKey;
        public int fromFrameId;
        public int toFrameId;
    }

    [Serializable]
    public sealed class ImportedLf2FrameRow
    {
        public int id;
        public string frameName;
        public int pic;
        public int state;
        public int wait;
        public int next;
        public int dvx;
        public int dvy;
        public int dvz;
        public string soundPath;

        public List<Lf2RectBlock> hurtboxes = new List<Lf2RectBlock>();
        public List<Lf2RectBlock> hitboxes = new List<Lf2RectBlock>();
        public List<Lf2OpointRow> opoints = new List<Lf2OpointRow>();
    }

    [Serializable]
    public struct Lf2RectBlock
    {
        public int kind;
        public int x;
        public int y;
        public int w;
        public int h;
        public string raw;
    }

    [Serializable]
    public struct Lf2OpointRow
    {
        public int oid;
        public string resolvedRelativeFile;
        public string raw;
    }
}
