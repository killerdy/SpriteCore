using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletYellow : Bullet
{
    // Start is called before the first frame update
    private float speed=10f;
    private float distance = 0f;
    public float maxDistance = 100f;
    public GameObject explosionPrefab;
    public float deflectionAngle=5f;
    //public  BulletYellow(ActItem actItem,int num)
    //{
    //    damageInit = num;
    //    attacker = actItem;

    //}
    private void OnEnable()
    {
        distance = 0f;
    }
    void Start()
    {
       
    }
    
    public void SetSpeed(Vector2 direction)
    {

        direction = Quaternion.AngleAxis(deflectionAngle, Vector3.forward) * direction;
        Quaternion quaternion= Quaternion.AngleAxis(deflectionAngle, Vector3.forward);
        float angleRad = Mathf.Atan2(direction.y,direction.x);
        float angleDeg = angleRad * Mathf.Rad2Deg + Random.Range(-5f, 5f);
        
        transform.rotation = quaternion * Quaternion.AngleAxis(angleDeg, Vector3.forward);


        angleRad = Mathf.Deg2Rad*angleDeg;
        float angleX=Mathf.Cos(angleRad);
        float angleY=Mathf.Sin(angleRad);
        direction=new Vector2(angleX,angleY);   
        rb.velocity = direction.normalized* speed;
    }
    // Update is called once per frame
    void Update()
    {
        distance += rb.velocity.magnitude * Time.deltaTime;
        if (distance >= maxDistance)
            Explode();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<ActItem>() != null&&collision.transform.CompareTag("Enemy"))
            DamageManager.instance.DamageGeneration(damageInit, attacker, collision.gameObject.GetComponent<ActItem>(), collision.transform.position - transform.position);
        Explode();
    }


}
