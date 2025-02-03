using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GunAk47 : Weapon
{
    // Start is called before the first frame update
    public float interval;
    public Vector2 direction;
    public Transform muzzlePos;
    public Transform shellPos;
    public GameObject bullet;
    public GameObject shell;
    public ActItem user;
    public float filpY;
    void Start()
    {
        user=transform.parent.gameObject.GetComponent<ActItem>();
        filpY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        direction = Tool.instance.mousePosition - (Vector2)this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        //transform.right = direction;
        if(Tool.instance.mousePosition.x<transform.position.x)
            transform.localScale = new Vector3(-filpY,-filpY,filpY);
        else 
            transform.localScale = new Vector3(filpY,filpY,filpY);
        Shot();
    }
    
    void Shot()
    {
        //GameObject bullet=Instantiate(Resources.Load<GameObject>("Bullets/BulletYellow"));

        GameObject bullet = Pool.instance.GetPool("BulletYellow");
        bullet.transform.position = muzzlePos.position;
        bullet.GetComponent<BulletYellow>().SetSpeed(direction.normalized);
        bullet.GetComponent<BulletYellow>().SetDamage(user,user.str);
    }
}
