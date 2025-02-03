using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FireBall : Bullet
{
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    public Color color;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (color != default(Color))
            spriteRenderer.color = color;
        StartCoroutine(Tool.instance.DelayedAction(() =>
        {
            //gameObject.SetActive(false);
            Pool.instance.ReturnPool(this.name.Replace("(Clone)", ""), gameObject);
        }, 0.3f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<ActItem>() != null&&collision.transform.CompareTag("Enemy"))
        {
            DamageManager.instance.DamageGeneration(damageInit, attacker, collision.gameObject.GetComponent<ActItem>(), collision.transform.position - transform.position);
        }
    }


}
