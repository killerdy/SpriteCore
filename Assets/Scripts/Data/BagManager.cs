using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public static BagManager instance;
    public HashSet<int> equipSet=new HashSet<int>();

    private void Awake()
    {
        instance = this;
    }
    public void OpenBag()
    {
        
    }
    
}
