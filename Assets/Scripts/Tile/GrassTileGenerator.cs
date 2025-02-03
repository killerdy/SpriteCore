using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrassTileGenerator : MonoBehaviour
{
    public TileBase grass,airwall;
    public Tilemap grassMap,airwallMap;
    public float frequency = 0.1f;
    public float amplitude = 1.0f;
    public int octaves = 4;
    public float threshold = 0.9f;


    public int mapWidth = 100;
    public int mapHeight = 100;
    public int seed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        GeneratorMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GeneratorMap()
    {

        Random.InitState(seed);
       
        for (int i = -mapWidth/2; i < mapWidth/2; i++)
        {
            for(int j = -mapHeight/2; j < mapHeight/2; j++)
            {
                if (grassMap.GetTile(new Vector3Int(i,j,0))!=null)
                    continue;
                float noiseValue = GetPerlinNoise(i, j);

                //float noiseValue=Random.Range(0f,1f);
                if(noiseValue > threshold)
                {
                    grassMap.SetTile(new Vector3Int(i, j, 0),grass);
                }
                else
                {
                    airwallMap.SetTile(new Vector3Int(i,j,0),airwall);
                }
            }
        }
    }
    public float GetPerlinNoise(float x, float y)
    {
        float noiseValue = 0.0f;
        for (int i = 0; i < octaves; i++)
        {
            float freq = frequency * Mathf.Pow(2, i);
            float amp = amplitude / Mathf.Pow(2, i);
            noiseValue += Mathf.PerlinNoise(x * freq, y * freq) * amp;

        }
        return noiseValue;
    }
}
