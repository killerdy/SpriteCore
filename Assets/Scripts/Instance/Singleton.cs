using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            instance=(T)FindObjectOfType(typeof(T));
            if(instance == null)
            {
                GameObject SingletonObject=new GameObject(typeof(T).Name);
                instance=SingletonObject.AddComponent<T>();
                DontDestroyOnLoad(instance.gameObject);
            }
            
            return instance;
        }
    }
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Debug.LogError($"instance of {typeof(T)} already exist");
            Destroy(this.gameObject);
        }
        
    }
}
