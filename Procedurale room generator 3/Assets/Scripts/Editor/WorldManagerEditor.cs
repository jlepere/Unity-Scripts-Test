using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldManager))]
public class WorldManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.FlexibleSpace();
        WorldManager worldManager = (WorldManager)target;
        if (GUILayout.Button("GenerateWorld"))
            worldManager.GenerateWorld();
        if (GUILayout.Button("CleanChunks"))
            worldManager.CleanChunks();
    }
}
