using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActItem : MonoBehaviour
{

    public Rigidbody2D rb;
    public int ID = -1;
    public string Name = "killerdy";
    public int level=1;
    public int ExpMax = 100;
    public int exp=0;
    public int HPMax=100;
    public int MPMax=100;
    public int HP=100;
    public int MP=100;
    public int str=10;
    public int mag=10;
    public int def=0;
    public int expGen = 10;
    public float Cir = 5f;
    public float CirEffect = 50f;
    public float PerStr = 0;
    public float PerMag = 0;
    public float PerDamage = 0;
    public HashSet<int> equipments = new HashSet<int>();
    public int EquipMax = 6;
    //public Vector2 velocity;
    public bool isDeath=false;
    public float maxSpeed=1f;

    //public ActItem(HumanData data)
    //{
    //    ID = data.ID;
    //    Name = data.Name;
    //    level = data.Level;
    //    ExpMax = data.ExpMax;   
    //    HPMax = data.HPMax;
    //    HP = data.HP;
    //    MPMax = data.MPMax;
    //    MP = data.MP;
    //    str=data.Str;
    //    mag=data.Mag;
    //    def=data.Def;
    //    expGen = data.ExpGen;
    //    Cir = data.Cir;
    //    CirEffect = data.CirEEfect;
    //}
    public void Init(HumanData data)
    {
        ID = data.ID;
        Name = data.Name;
        level = data.Level;
        ExpMax = data.ExpMax;
        HPMax = data.HPMax;
        HP = data.HP;
        MPMax = data.MPMax;
        MP = data.MP;
        str = data.Str;
        mag = data.Mag;
        def = data.Def;
        expGen = data.ExpGen;
        Cir = data.Cir;
        CirEffect = data.CirEEfect;
        equipments = data.equipments;

        exp = data.exp;
        PerDamage = data.PerDamage;
        PerStr = data.PerStr;
        PerMag = data.PerMag;
    }
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void AttackA(){}
    public virtual void ReadyE() {}
    public virtual void AttackE(){}
    public virtual void AttackW(){}
    public virtual void Move() { }
    public virtual void Aim(){ }
    public virtual void Check()
    {
        //if (HP <= 0&&!isDeath)
        //{
        //    isDeath = true;
        //    HP = 0;
        //    Death();
        //}
        while (exp >= ExpMax)
        {
            exp-= ExpMax;
            level++;
            ExpMax += 100;
            if (gameObject.CompareTag("Player"))
            {
                //ChoosePanel.instance.SetActive();
                ChoosePanel.instance.GetChoose(Random.Range(5,6));
            }
                
        }
    } 
    public virtual void Death() {
        StartCoroutine(Tool.instance.DelayedAction(() => {
            transform.GetComponent<Collider2D>().enabled = false;
            rb.simulated = false;
        },1f));
       
    }
    // Update is called once per frame
   public virtual void Update()
    {

        Check();
    }
}
