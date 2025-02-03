using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipData
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
    public string ImagePath;
    public string Description;

    public EquipData ShallowCopy()
    {
        return (EquipData)this.MemberwiseClone();
    }
}

[CreateAssetMenu(fileName="EnemyConfig",menuName="ScriptableObjects/Enemy Config",order =1)]
public class EquipConfig : ScriptableObject
{
    public List<EquipData> list=new List<EquipData>();
    //public EquipData[] list = new EquipData[10];
}
