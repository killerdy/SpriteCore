using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistObject : MonoBehaviour
{
    // Start is called before the first frame update
    public static PersistObject instance;

    public string IP = "106.14.3.139", Name;
    public int Port= 8888;
    private void Awake()
    {
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Name = RandomStringGenerator(6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateConfig(string name,string ip, int port)
    {
        if(name!="")
        Name = name;
        if(ip!=null)
        IP = ip;
        if(port!=0)
        Port = port;
        Debug.Log(IP+Port);
    }
    public string RandomStringGenerator(int length)
    {

        string s="";
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for(int i = 0; i < length; i++)
        {
            s += chars[Random.Range(0, chars.Length)];
        }
        return s;
    }
}
