#if UNITY_EDITOR
using UnityEditor;
using Project.Data;
using System.Collections.Generic;
using UnityEngine;

public static class StageDefinitionGenerator
{
    [MenuItem("_Project/Generate Stage Definitions")]
    public static void Generate()
    {
        var stages = new[]
        {
            new { id = 1, name = "Grassland", phases = new[] {
                new { enemies = new[] { (4, 3), (5, 2) } },
                new { enemies = new[] { (6, 2), (7, 1) } },
                new { enemies = new[] { (13, 1) } }
            }},
            new { id = 2, name = "Arena", phases = new[] {
                new { enemies = new[] { (8, 2), (9, 2) } },
                new { enemies = new[] { (14, 2), (15, 1) } },
                new { enemies = new[] { (18, 1) } }
            }},
            new { id = 3, name = "Temple", phases = new[] {
                new { enemies = new[] { (16, 3), (17, 2) } },
                new { enemies = new[] { (19, 2), (20, 1) } },
                new { enemies = new[] { (22, 1) } }
            }},
            new { id = 4, name = "Dark Forest", phases = new[] {
                new { enemies = new[] { (21, 4), (13, 2) } },
                new { enemies = new[] { (10, 2), (15, 2) } },
                new { enemies = new[] { (23, 1) } }
            }},
            new { id = 5, name = "Final Stage", phases = new[] {
                new { enemies = new[] { (4, 2), (6, 2), (7, 2) } },
                new { enemies = new[] { (20, 1), (18, 1), (19, 1) } },
                new { enemies = new[] { (22, 1), (23, 1) } }
            }}
        };

        if (!AssetDatabase.IsValidFolder("Assets/_Project/Data/Stages"))
        {
            AssetDatabase.CreateFolder("Assets/_Project/Data", "Stages");
        }

        foreach (var stage in stages)
        {
            var path = $"Assets/_Project/Data/Stages/Stage{stage.id}.asset";
            var existing = AssetDatabase.LoadAssetAtPath<StageDefinition>(path);
            if (existing != null) continue;

            var so = ScriptableObject.CreateInstance<StageDefinition>();
            so.lf2StageId = stage.id;
            so.displayName = stage.name;
            so.phases = new List<StagePhaseDefinition>();

            foreach (var phase in stage.phases)
            {
                var phaseDef = new StagePhaseDefinition();
                phaseDef.entries = new List<StageSpawnEntry>();

                foreach (var (enemyId, count) in phase.enemies)
                {
                    phaseDef.entries.Add(new StageSpawnEntry
                    {
                        objectId = enemyId,
                        times = count,
                        role = enemyId >= 22 ? SpawnRole.Boss : SpawnRole.Soldier
                    });
                }

                so.phases.Add(phaseDef);
            }

            AssetDatabase.CreateAsset(so, path);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("5 Stage definitions generated!");
    }
}
#endif
