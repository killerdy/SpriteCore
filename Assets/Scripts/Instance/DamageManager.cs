using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    bool isCir=false;
    // Start is called before the first frame update
    public static DamageManager instance;
    
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    public void DamageGeneration(float damageInit,ActItem attacker,ActItem Defender,Vector2 dirction,int tag=0)
    {
        if (tag == 0)
            damageInit *= (1 + attacker.PerStr/100f);
        else
            damageInit *= (1 + attacker.PerMag/100f);
        damageInit*=(1+attacker.PerDamage/ 100f);
        damageInit*= 100f / (100f + Defender.def);
        if (Random.Range(0f, 100f) <= attacker.Cir)
        {
            damageInit *= (100 + attacker.CirEffect) / 100f;
            isCir = true;
        }
        int damage=Mathf.Max(1,(int)damageInit);
        
        GameObject num= Instantiate(Resources.Load<GameObject>("Prefabs/DamageNumber"));
        
        num.transform.GetComponent<DamageNumber>().SetParams(dirction,damage,isCir,Defender.transform);
        isCir=false;
        Defender.HP -= damage;
        if (Defender.HP <= 0)
        {
            Defender.HP = 0;
            Defender.Death();
        }
    }
}
