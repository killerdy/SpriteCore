using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlActor : ActItem
{
    public Animator animator;
    public float flip = 1.0f;
    public override void Start()
    {
        base.Start();        
        animator= GetComponent<Animator>();
        maxSpeed = 3f;
    }
    public override void Update()
    {
        base.Update();
        Move();
    }
    public override void Move()
    {
        if(isDeath) return;
        Flip();
    }

    public virtual void Flip()
    {
        if (rb.velocity.x < 0)
            transform.localScale = new Vector3(-flip, flip, flip);
        else if (rb.velocity.x > 0)
            transform.localScale = new Vector3(flip, flip, flip);
        else if (Tool.instance.mousePosition.x < transform.position.x)
            transform.localScale = new Vector3(-flip, flip, flip);
        else
            transform.localScale = new Vector3(flip, flip, flip);
    }
}
