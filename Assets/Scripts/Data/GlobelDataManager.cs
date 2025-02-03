using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GlobelDataManager : MonoBehaviour
{
    public static GlobelDataManager instance;
   
    
    public class Bag
    {
        public HashSet<int> equipSet=new HashSet<int>();
    }
    public class Player
    {
        public HashSet<int> equipSet=new HashSet<int>();
        public HashSet<int> playerSet=new HashSet<int>();
        //public HashSet<int> humanSet=new HashSet<int>();
        //public ActItem actItem=new ActItem();
    }
    public class DataConfig
    {
        public string name;
        public int equipId;
        public int humanId;
    }
    
    [System.Serializable]
    public class Data
    {
        public DataConfig config=new DataConfig();
        public Dictionary<int,EquipData> equip=new Dictionary<int, EquipData>();
        public Dictionary<int, HumanData> human = new Dictionary<int, HumanData>();
        public Player player=new Player();
        public Bag bag = new Bag();
    }

    public Data data = new Data();
    public DataConfig dataConfig=>data.config;
    public Player player=>data.player;
    public Bag bag=>data.bag;
    public Dictionary<int, EquipData> equip => data.equip;
    public Dictionary<int, HumanData> human => data.human;
    void Awake()
    {
        instance = this;
        LoadData("auto");
    }
    private void Start()
    {
        StartCoroutine(Tool.instance.DelayedAction(() =>
        {
            PlayerManager.instance.player.equipments = player.equipSet;
            BagManager.instance.equipSet = bag.equipSet;
            PlayerManager.instance.players=player.playerSet;
        }));
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadData(string name="auto")
    {

        string filePath = Path.Combine(Application.persistentDataPath, "save_" + name + ".json");
        Debug.Log("Application.persistentDataPath: " + Application.persistentDataPath);
        if (File.Exists(filePath))
        {
            string json=File.ReadAllText(filePath);
            data = JsonConvert.DeserializeObject<Data>(json);
            //dataConfig = data.config;
            //equip=data.equip;
            //bag=data.bag;
            //player=data.player; 
            //if (data != null)
            //{
            //    equip = data.equip;
            Debug.Log($"存档{name}加载成功");
            //}
        }
    }
    public void SaveData(string name="auto")
    {
        Debug.Log("equip数量"+equip.Count);
        string filePath = Path.Combine(Application.persistentDataPath, "save_" + name + ".json");
        dataConfig.name = name;
        //data.equip = equip;
        //data.config=dataConfig;
        ActItemManager.instance.SaveData();
        string json=JsonConvert.SerializeObject(data);
        File.WriteAllText(filePath, json);
        Debug.Log($"存档{name}保存成功");
    }
    
}
