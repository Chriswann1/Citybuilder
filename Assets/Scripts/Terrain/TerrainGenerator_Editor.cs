using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGenerator_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TerrainGenerator terrainscript = (TerrainGenerator) target;
        if (GUILayout.Button("Generate Map"))
        {
            terrainscript.GenerateMap();
        }
    }

    

}
