using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public List<string> weapons;
    public int weaponIndex=0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        weapons = new List<string>();
        weapons.Add("BlueBall");
        weapons.Add("BangBang");
        weapons.Add("Ak47");
    }
    public string GetNextWeapon()
    {
        return weapons[(++weaponIndex) % weapons.Count];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
