using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool instance;
    public List<GameObject> prefabs=new List<GameObject>();

    public Dictionary<string,Queue<GameObject>> pool=new Dictionary<string, Queue<GameObject>>();
    public Dictionary<string, int> poolSize=new Dictionary<string, int>();
    public Dictionary<string, int> prefabsId=new Dictionary<string, int>();
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
            
    }
    void Start()
    {
        InitPool();
        
        
    }
    void InitPool()
    {
        int i = 0;
        foreach (GameObject go in prefabs)
        {
            prefabsId[go.name] = i++;
            poolSize[go.name] = 5;
            Queue<GameObject> q = new Queue<GameObject>();
            pool.Add(go.name, q);
            AddPool(go.name);
        }
    }
    void AddPool(string name)
    {
        poolSize[name] *= 2;
        for (int i = 0; i < poolSize[name]; i++)
        {
            GameObject go = Instantiate(prefabs[prefabsId[name]]);
            go.SetActive(false);
            pool[name].Enqueue(go);
            
        }
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject GetPool(string name)
    {
        if (pool[name].Count == 0)
            AddPool(name);
        GameObject obj = pool[name].Dequeue();
        obj.SetActive(true);
        return obj; 
    }
    public void ReturnPool(string name,GameObject go)
    {
        go.SetActive(false);
        pool[name].Enqueue(go);
    }
}
