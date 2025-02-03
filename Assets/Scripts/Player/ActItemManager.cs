using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using UnityEngine;
using static GlobelDataManager;

public class ActItemManager : MonoBehaviour
{
    public static ActItemManager instance;  
    //public Dictionary<int, ActItem> humans=new Dictionary<int, ActItem>();
    //public List<GameObject> goList = new List<GameObject>();
    public Dictionary<int,GameObject> humans=new Dictionary<int,GameObject>();  
    private void Awake()
    {
        instance = this;
    }
    //public void 
    void Start()
    {
        StartCoroutine(Tool.instance.DelayedAction(() =>
        {
            foreach (int id in GlobelDataManager.instance.human.Keys)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>(GlobelDataManager.instance.human[id].PrefabPath));
                //goList.Add(go);
                humans.Add(id,go);
                go.GetComponent<ActItem>().Init(GlobelDataManager.instance.human[id]);
                if (PlayerManager.instance.players.Contains(id))
                    PlayerManager.instance.playerList.Add(id);
                
            }
            if (PlayerManager.instance.playerList.Count == 0)
            {
                int id = GenHuman(0);
                AttachHuman(id);
                GameObject go = Instantiate(Resources.Load<GameObject>(GlobelDataManager.instance.human[id].PrefabPath));
                humans.Add(id, go);
                go.GetComponent<ActItem>().Init(GlobelDataManager.instance.human[id]);
                if (PlayerManager.instance.players.Contains(id))
                    PlayerManager.instance.playerList.Add(id);
                //PlayerManager.instance.NextPlayer();
            }
            PlayerManager.instance.NextPlayer();
            //foreach(int id in players)
            //{

            //}
        }));
    }
    public void GenHumanObject(int id)
    {

        Debug.Log("hh"+PlayerManager.instance.playerList.Count);
        id=GenHuman(id);
        AttachHuman(id);
        GameObject go = Instantiate(Resources.Load<GameObject>(GlobelDataManager.instance.human[id].PrefabPath));
        go.GetComponent<ActItem>().Init(GlobelDataManager.instance.human[id]);
        humans.Add(id, go);
        if (PlayerManager.instance.players.Contains(id))
            PlayerManager.instance.playerList.Add(id);

    }
    public int GenHuman(int id)
    {

        //EquipData a=new EquipData();
        //EquipData equipData = (EquipData)a.MemberwiseClone();
        HumanData humanData = LoadAsset.instance.humanConfig[id].ShallowCopy();
        GlobelDataManager.instance.human.Add(++GlobelDataManager.instance.dataConfig.humanId, humanData);
        return GlobelDataManager.instance.dataConfig.humanId;
    }
    public void AttachHuman(int id)
    {
        PlayerManager.instance.players.Add(id);
        //PlayerManager.instance.playerList.Add(id);
        
    }
    void Update()
    {
        
    }
    public void SaveData()
    {
        foreach(int id in humans.Keys)
        {
           GlobelDataManager.instance.human[id].Level= humans[id].GetComponent<ActItem>().level;
            GlobelDataManager.instance.human[id].ExpMax = humans[id].GetComponent<ActItem>().ExpMax;
            GlobelDataManager.instance.human[id].HPMax = humans[id].GetComponent<ActItem>().HPMax;
            GlobelDataManager.instance.human[id].HP = humans[id].GetComponent<ActItem>().HP;
            GlobelDataManager.instance.human[id].MPMax = humans[id].GetComponent<ActItem>().MPMax;
            GlobelDataManager.instance.human[id].MP = humans[id].GetComponent<ActItem>().MP;
            GlobelDataManager.instance.human[id].Str = humans[id].GetComponent<ActItem>().str;
            GlobelDataManager.instance.human[id].Mag = humans[id].GetComponent<ActItem>().mag;
            GlobelDataManager.instance.human[id].Def = humans[id].GetComponent<ActItem>().def;
            GlobelDataManager.instance.human[id].ExpGen = humans[id].GetComponent<ActItem>().expGen;
            GlobelDataManager.instance.human[id].Cir = humans[id].GetComponent<ActItem>().Cir;
            GlobelDataManager.instance.human[id].CirEEfect = humans[id].GetComponent<ActItem>().CirEffect;
            GlobelDataManager.instance.human[id].equipments = humans[id].GetComponent<ActItem>().equipments;

            GlobelDataManager.instance.human[id].exp = humans[id].GetComponent<ActItem>().exp;
            GlobelDataManager.instance.human[id].PerMag = humans[id].GetComponent<ActItem>().PerMag;
            GlobelDataManager.instance.human[id].PerStr = humans[id].GetComponent<ActItem>().PerStr;
            GlobelDataManager.instance.human[id].PerDamage = humans[id].GetComponent<ActItem>().PerDamage;
        }
    }
}
