using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PersistObject : MonoBehaviour
{
    // Start is called before the first frame update
    public static PersistObject instance;

    public string IP = "10.11.10.11", Name;
    public int Port= 4000;
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

        //string s="";
        StringBuilder sb=new StringBuilder();
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for(int i = 0; i < length; i++)
        {
            sb.Append(chars[Random.Range(0, chars.Length)]);
            //s += chars[Random.Range(0, chars.Length)];
        }
        
        return string.Intern(sb.ToString());
    }
}
