using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PerlinNoiseTilemapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    //public TileBase dirtTile;
    public static PerlinNoiseTilemapGenerator instance;
    public TileBase grassTile;
    //public TileBase waterTile;
    public float frequency = 0.1f;
    public float amplitude = 1.0f;
    public int octaves = 4;
    public float threshold = 0.5f;
    public int mapWidth = 100;
    public int mapHeight = 100;
    public int seed = 0;
    public Tilemap GrassMap;
    void Start()
    {
        instance = this;
        //GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateMap()
    {
        Random.InitState(seed);
        GrassMap.ClearAllTiles();
        //for(int i = -mapHeight-20; i <= mapHeight + 20; i++)
        //{
        //    for(int j=-mapWidth-20;j<mapWidth + 20; j++)
        //    {
        //        //WaterMap.SetTile(new Vector3Int(i, j, 0), waterTile);
        //    }
        //}
        for (int i = -mapHeight; i < mapHeight; i++)
        {
            for(int j = -mapWidth; j < mapWidth; j++)
            {
                float noiseValue=GetPerlinNoise(i,j);
                if (noiseValue > threshold)
                {
                    GrassMap.SetTile(new Vector3Int(i, j, 0), grassTile);//sprout lands
                }
                else
                {
                    //tilemap.SetTile(new Vector3Int(i,j,0),dirtTile);
                }
               
            }
        }

    }
    public float GetPerlinNoise(float x,float y)
    {
        float noiseValue = 0.0f;
        for (int i = 0; i < octaves; i++)
        {
            float freq = frequency * Mathf.Pow(2, i);
            float amp=amplitude/Mathf.Pow(2, i);
            noiseValue += Mathf.PerlinNoise(x * freq, y * freq) * amp;
            
        }
        return noiseValue;
    }
}
