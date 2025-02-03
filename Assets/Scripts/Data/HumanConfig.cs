using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class HumanData
{
    public int ID;
    public string Name;
    public int Level;
    public int HPMax;
    public int HP;
    public int MPMax;
    public int MP;
    public int Str;
    public int Mag;
    public int Def;
    public float Cir;
    public float CirEEfect;

    public float PerStr = 0;
    public float PerMag = 0;
    public float PerDamage = 0;
    public int exp = 0;

    public int ExpGen;
    public int ExpMax;
    public int MaxSpeed;
    public string PrefabPath;
    public HashSet<int> equipments = new HashSet<int>();
    public HumanData ShallowCopy()
    {
        return (HumanData)this.MemberwiseClone();
    }

}
public class HumanConfig : ScriptableObject
{
    public List<HumanData> list=new List<HumanData>();
}
