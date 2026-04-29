using Project.Gameplay.Combat;
using UnityEditor;
using UnityEngine;

namespace Project.Editor.Combat
{
    [CustomEditor(typeof(AttackDefinition))]
    public sealed class AttackDefinitionInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "m_Script", "hitboxFrames");

            var frames = serializedObject.FindProperty("hitboxFrames");
            EditorGUILayout.Space(8f);
            EditorGUILayout.LabelField("Hitbox Frames", EditorStyles.boldLabel);

            if (GUILayout.Button("Adicionar frame"))
            {
                var index = frames.arraySize;
                frames.InsertArrayElementAtIndex(index);
                var e = frames.GetArrayElementAtIndex(index);

                e.FindPropertyRelative("startTick").intValue = 0;
                e.FindPropertyRelative("endTick").intValue = 0;
                e.FindPropertyRelative("localOffset").vector2Value = new Vector2(0.65f, 0.05f);
                e.FindPropertyRelative("halfExtents").vector2Value = new Vector2(0.55f, 0.32f);
                e.FindPropertyRelative("damage").intValue = 5;
                e.FindPropertyRelative("knockback").vector2Value = new Vector2(5f, 0f);
                e.FindPropertyRelative("hitStopTicks").intValue = 2;
                e.FindPropertyRelative("screenShakeAmplitude").floatValue = 0.12f;
                e.FindPropertyRelative("isGrab").boolValue = false;
            }

            for (var i = 0; i < frames.arraySize; i++)
            {
                var frame = frames.GetArrayElementAtIndex(i);
                var shouldRemove = false;
                using (new EditorGUILayout.VerticalScope("box"))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField("Frame " + i, EditorStyles.boldLabel);
                        shouldRemove = GUILayout.Button("Remover", GUILayout.Width(80));
                    }

                    if (!shouldRemove)
                    {
                        EditorGUILayout.PropertyField(frame.FindPropertyRelative("startTick"));
                        EditorGUILayout.PropertyField(frame.FindPropertyRelative("endTick"));
                        EditorGUILayout.PropertyField(frame.FindPropertyRelative("localOffset"));
                        EditorGUILayout.PropertyField(frame.FindPropertyRelative("halfExtents"));
                        EditorGUILayout.PropertyField(frame.FindPropertyRelative("damage"));
                        EditorGUILayout.PropertyField(frame.FindPropertyRelative("knockback"));
                        EditorGUILayout.PropertyField(frame.FindPropertyRelative("hitStopTicks"));
                        EditorGUILayout.PropertyField(frame.FindPropertyRelative("screenShakeAmplitude"));
                        EditorGUILayout.PropertyField(frame.FindPropertyRelative("isGrab"));
                    }
                }

                if (shouldRemove)
                {
                    frames.DeleteArrayElementAtIndex(i);
                    break;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
