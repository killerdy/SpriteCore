using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : ActItem
{
    // Start is called before the first frame update
    public Color color=Color.green;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    public float moveSpeed = 2f;
    public float maxDistance=1;
    //GameObject gameObject;
    GameManager GM;
    Fish fish;
    //Fish<SlimeManager> fish;
    Vector2 clickPosition= Vector2.zero;
    public bool isJump=false;
    public override void Start()
    {
        base.Start();
        GM=Camera.main.transform.GetComponent<GameManager>();
        animator=GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fish= gameObject.GetComponent<Fish>();
        //fish= gameObject.AddComponent<Fish<SlimeManager>>();
    }

    // Update is called once per frame
    public override void Update()
    {
     
        base.Update();
        spriteRenderer.color = color;
        rb.velocity = fish.velocity;
        Move();
        if (!isJump && Random.Range(0, 100f) > 99)
            Jump();
    }
    public override void Move()
    {
        if (isDeath)
            return;
        if(rb.velocity.x<0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX= false;
        //transform.position += (Vector3)rb.velocity * Time.deltaTime;
    }
    public override void Death()
    {
        if(isDeath) return;
        isDeath = true;
        animator.SetTrigger("Die");
        base.Death();
        PlayerManager.instance.player.exp += expGen;
        StartCoroutine(GM.DelayedAction(() => { Pool.instance.ReturnPool("Slime", gameObject); },10f));
    }
    void Jump()
    {
        isJump = true;
        animator.SetTrigger("Jump");
        StartCoroutine(GM.DelayedAction(() =>
        {
            isJump = false;
        },1f));
   
    }
}
