using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Tilemaps;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public static EnemyGenerator instance;
    public Tilemap grassMap;    
    public List<string> enemyNames = new List<string>();
    public List<int> enemyScore=new List<int>();
    public int rank;
    private void Awake()
    {
        instance = this;
        enemyNames.Add("Slime");
        enemyScore.Add(5);
        
    }

    private void Update()
    {
        if (rank < PlayerManager.instance.player.level)
        {
            rank++;
            GenerateEnemy();
        }
    }
    public Vector2 GenerateRandomPositionAround(Vector2 position, float minDistance, float maxDistance)
    {
        float angle = Random.Range(0f,2f*Mathf.PI);
        float distance=Random.Range(minDistance,maxDistance);   
        float offsetX=distance*Mathf.Cos(angle);
        float offsetY=distance*Mathf.Sin(angle);
        return new Vector2(position.x+offsetX,position.y+offsetY);
    }   
    public void GenerateEnemy()
    {
        int id=Random.Range(0,enemyNames.Count);
        if (id == 0)
        {
            Color color = Random.ColorHSV();
            
            int num = rank * 100 / enemyScore[id];
            Vector3 position = new Vector3(1,1,1);

            
           
            for (int i = 0; i < num; i++)
            {
                while (true)
                {
                    position = GenerateRandomPositionAround(PlayerManager.instance.player.transform.position, 15f, 30f);
                    Vector3Int cellPosition = grassMap.WorldToCell(position);
                    if (grassMap.GetTile(cellPosition) != null)
                        break;
                }

                if ( Pool.instance.poolSize[enemyNames[id]]*2-1 - Pool.instance.pool[enemyNames[id]].Count >= 200)
                    return;
                GameObject go=Pool.instance.GetPool(enemyNames[id]);
                go.GetComponent<Collider2D>().enabled = true;
                go.GetComponent<Rigidbody2D>().simulated = true;
                ActItem actItem = go.GetComponent<ActItem>();
                actItem.HPMax = actItem.HP = 10 * rank;
                actItem.exp = 10 + rank;
                Slime slime=(Slime)actItem;
                slime.isDeath = false;
                slime.color = color;
                go.transform.position = position;
            }
        }
    }
    
    
}
