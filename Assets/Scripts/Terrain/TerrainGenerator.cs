#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    [SerializeField] private int depth;
    [SerializeField] private int width;

    [SerializeField] private int lenght;

    [SerializeField] private float scale;

    [SerializeField] private Vector2 offset;

    [SerializeField] private Terrain _terrain;

    private float[,,] alphadata;

    [SerializeField] private float HeightRandomizerMax;

    private const int GRASS = 0;
    private const int ROCK = 1;
    private const int DESERT = 2;
    private const int SNOW = 3;

    TerrainData GenerateMap(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, lenght);

        
        
        terrainData.SetHeights(0,0,GenerateHeights());
        alphadata = terrainData.GetAlphamaps(0,0,terrainData.alphamapWidth, terrainData.alphamapHeight);
        //TextureApply(terrainData);
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] lenghts = new float[width, lenght];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < lenght; y++)
            {
                lenghts[x, y] = CalculateHeight(x, y) + Random.Range(0,HeightRandomizerMax/100);
            }
        }

        return lenghts;
    }

    float CalculateHeight(int x, int y)
    {

        float xcoord = ((float)x / width * scale + offset.x);
        float ycoord = ((float)y / lenght * scale + offset.y);
        
        return Mathf.PerlinNoise(xcoord, ycoord);
    }

    public void GenerateMap()
    {
        _terrain.terrainData = GenerateMap(_terrain.terrainData);
    }

    public void TextureApply(TerrainData terrainData)
    {

        for (int y = 0; y < terrainData.heightmapResolution; y++)
        {
            for (int x = 0; x < terrainData.heightmapResolution; x++)
            {
                float height = terrainData.GetHeight(x, y);
                if (height > 5.1f)
                {
                    alphadata[x, y, ROCK] = 1;
                    alphadata[x, y, GRASS] = 0;
                    
                }
                else
                {
                    alphadata[x, y, GRASS] = 1;
                    alphadata[x, y, ROCK] = 0;
                } 
                
            }
        }
        terrainData.SetAlphamaps(0,0,alphadata);
    }
}
#endif