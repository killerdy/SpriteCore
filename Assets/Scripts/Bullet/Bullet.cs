using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float damageInit;
    public ActItem attacker;
    // Start is called before the first frame update

    public void SetDamage(ActItem actItem, float num)
    {

        damageInit = num;
        attacker = actItem;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Explode()
    {
        Pool.instance.ReturnPool(this.name.Replace("(Clone)",""),gameObject);
    }
    
}
