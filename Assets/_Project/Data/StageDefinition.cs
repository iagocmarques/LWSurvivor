using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Data
{
    public enum SpawnRole
    {
        Soldier,
        Boss,
        Item
    }

    [Serializable]
    public class StageSpawnEntry
    {
        public int objectId;
        public int hpOverride;
        public int times = 1;
        public float ratio = 1f;
        public SpawnRole role;
    }

    [Serializable]
    public class StagePhaseDefinition
    {
        public int bound;
        public string musicPath;
        public List<StageSpawnEntry> entries = new();
    }

    [CreateAssetMenu(fileName = "NewStage", menuName = "_Project/Data/Stage Definition", order = 4)]
    public sealed class StageDefinition : ScriptableObject
    {
        [Header("Identity")]
        public int lf2StageId;
        public string displayName;

        [Header("Background")]
        public BackgroundDefinition background;

        [Header("Phases (ordered)")]
        public List<StagePhaseDefinition> phases = new();
    }
}
