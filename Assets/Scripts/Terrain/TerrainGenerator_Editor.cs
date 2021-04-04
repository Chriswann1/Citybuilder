#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGenerator_Editor : Editor //Script that is changing the editor inspector
{
    public override void OnInspectorGUI()//adding GUI to editor, a button for the TerrainGenerator
    {
        DrawDefaultInspector();

        TerrainGenerator terrainscript = (TerrainGenerator) target;
        if (GUILayout.Button("Generate Map"))
        {
            terrainscript.GenerateMap();
        }
    }

    

}
#endif