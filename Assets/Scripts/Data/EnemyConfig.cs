using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
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
    public int ExpGen;
    public int ExpMax;
    public int MaxSpeed;
    public string PrefabPath;
    public EnemyData ShallowCopy()
    {
        return (EnemyData)this.MemberwiseClone();
    }

}
public class EnemyConfig : ScriptableObject
{
    public List<EnemyData> list=new List<EnemyData>();
}
