using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : ControlActor
{
    public override void Move()
    {
        base.Move();
        if (rb.velocity.magnitude > 0.1f)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }
    public override void Flip()
    {
        if (rb.velocity.x < 0)
            transform.localScale = new Vector3(flip, flip, flip);
        else if (rb.velocity.x > 0)
            transform.localScale = new Vector3(-flip, flip, flip);
        else if (Tool.instance.mousePosition.x < transform.position.x)
            transform.localScale = new Vector3(flip, flip, flip);
        else
            transform.localScale = new Vector3(-flip, flip, flip);
    }

}
