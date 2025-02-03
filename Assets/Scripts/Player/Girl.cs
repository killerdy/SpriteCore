using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Girl : ActItem
{
    // Start is called before the first frame update
    public Animator animator;
    public float readyTimeE;
    public float maxReadyTimeE=10f;
    public bool isReadyTimeE;
    public GameObject weapon;
    public float flip = 0.3f;
    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();    
        maxSpeed = 5f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Move();
        UpdateReadyTime();
    }
    void UpdateReadyTime()
    {
        if (isReadyTimeE) readyTimeE += Time.deltaTime;
    }
    public override void Move()
    {
        if (isDeath)
            return;
        Flip();
        //transform.position += (Vector3)rb.velocity * Time.deltaTime;
        if(rb.velocity.magnitude>0.1f)
        {
            //ResetAnimation();
            animator.SetBool("isRun",true);
        }
        else
        {
            //ResetAnimation();
            //animator.SetTrigger("idle");
            animator.SetBool("isRun", false);
        }
        //ÖØÖÃ
        if (rb.velocity.magnitude > 0.1f)
        {
            ResetSpell();
            
        }
       
    }
    public void Flip()
    {
        if (rb.velocity.x < 0)
            transform.localScale = new Vector3(-flip, flip, flip);
        else if (rb.velocity.x > 0)
            transform.localScale = new Vector3(flip, flip, flip);
        else if(Tool.instance.mousePosition.x<transform.position.x)
            transform.localScale = new Vector3(-flip, flip, flip);
        else
            transform.localScale = new Vector3(flip, flip, flip);
    }
    public override void AttackA()
    {
        ResetSpell();
        animator.SetBool("attack",true);
    }

    public override void ReadyE()
    {
        ReadyReset();
        animator.SetBool("isLookUp", true);
        
        isReadyTimeE = true;
        CursorManager.Instance.SetCursor(Resources.Load<Texture2D>("Images/CursorTen"));
    }
    public override void AttackW()
    {
        animator.SetTrigger("skillA");
    }
    public override void Aim()
    {
        if (weapon!=null)
            Destroy(weapon);
        ReadyReset();

        animator.SetBool("isAim", true);
        CursorManager.Instance.SetCursor(Resources.Load<Texture2D>("Images/CursorTen"));

        if (DataManager.instance.weaponIndex%3 != 2)
            return;
        
        GameObject gun = Instantiate(Resources.Load<GameObject>("Weapons/Ak47"));
        weapon = gun;
        //Debug.Log(1);
        GameObject bone = transform.GetChild(0).Find("Weapon").gameObject;
        gun.transform.parent = transform;
        gun.transform.position = bone.transform.position;
        gun.transform.localScale=bone.transform.localScale;
        bone.SetActive(false);
        




    }
    public override void AttackE()
    {
        
        
        
        float scale = Mathf.Max(readyTimeE * 2,1);
        isReadyTimeE = false;
        readyTimeE = 0;

        animator.SetBool("isLookUp", false);
        if (DataManager.instance.weaponIndex%3 == 2)
            return;
        //GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/FireBall"));
        GameObject go = Pool.instance.GetPool("FireBall");
        go.GetComponent<Bullet>().SetDamage(this,mag);
        go.transform.position = Tool.instance.mousePosition;
        go.transform.localScale = new Vector3(scale, scale, scale);
        if(DataManager.instance.weaponIndex%3==1)
        go.transform.GetComponent<FireBall>().color = new Color(1f,0.4f,0.71f);
        
    }
    void ReadyReset()
    {
        PlayerManager.instance.targetPosion=transform.position;
        animator.SetBool("isRun", false);
        animator.SetBool("isJump", false);
        
        //animator.SetBool("idle")
    }
    void ResetSpell()
    {
        
        animator.SetBool("isLookUp", false);
        animator.SetBool("isAim",false);
        CursorManager.Instance.ResetCursor();
        transform.GetChild(0).Find("Weapon").gameObject.SetActive(true);
        Destroy(weapon);
        //animator.SetBool("isRun", false);
        //animator.SetBool("isJump", false);
    }
    
    public override void Death()
    {
        isDeath = true;
        animator.SetTrigger("Die");
    }
    
}
