using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChoosePanel : BasePanel
{
    public static ChoosePanel instance;
    // Start is called before the first frame update
    List<string> str=new List<string>();
    List<GameObject> go=new List<GameObject>();
    List<Button> btns= new List<Button>();  
    List<Text> text=new List<Text>();
    public GridLayoutGroup grid;
    public delegate void Buff();
    public List<Buff> buffs = new List<Buff>();
   void Awake()
    {
        instance = this;
        //Debug.Log(this);
    }
    void Start()
    {

        buffs.Add(AddMaxHp);
        buffs.Add(AddStr);
        buffs.Add(AddCir);
        buffs.Add(AddCirEffect);
        buffs.Add(AddMag);
        //buffs.Add(AddPercentStr);
        //buffs.Add(AddPercentMag);
        buffs.Add(AddPercentDamage);
        buffs.Add(AddEquip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetChoose(int num)
    {

        if (go.Count > 0 && go[0]!=null) {
            btns[Random.Range(0,btns.Count)].onClick.Invoke();

            for (int i = 0; i < go.Count; i++)
            {
                Destroy(go[i]);
            }

        }
        go.Clear();
        btns.Clear();
        text.Clear();
        str.Clear();
        
        float childWidth = width / num - 5;
        float childHeight = childWidth * 1.6f;
        grid.cellSize = new Vector2(childWidth,childHeight);
        for (int i = 0; i < num; i++)
        {
            go.Add(Instantiate(new GameObject("1")));
            
            go[i].AddComponent<RectTransform>();
            btns.Add(go[i].AddComponent<Button>());
            go[i].GetComponent<RectTransform>().sizeDelta = new Vector2(childWidth, childHeight);
            go[i].AddComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu_setting");
            GameObject child = Instantiate(new GameObject("2"));
            child.AddComponent<RectTransform>().sizeDelta= new Vector2(childWidth*1.6f, childHeight*1.6f);
            child.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1);
            go[i].transform.SetParent(transform, false);
            child.transform.SetParent(go[i].transform, false);
            text.Add(child.AddComponent<Text>());
        }
        for(int i = 0; i < num; i++)
        {
            if (PlayerManager.instance.player.level%5==0&&i == num - 1)
            {
                AddCharactor();
            }
            else
            {
                int index = Random.Range(0, buffs.Count);
                buffs[index]();
            }
            
            text[i].font = MainPanel.instance.font;
            text[i].fontSize = 50;
            text[i].alignment = TextAnchor.MiddleCenter;
            text[i].text = str[i];
            
        }
    }
    
    void AfterClick()
    {
        for (int i = 0; i < go.Count; i++)
        {

            //go[i].SetActive(false);
            btns[i].interactable = false;
            Destroy(go[i]);
            //Destory(go[i]);
        }
        go.Clear();
        //StartCoroutine(Tool.instance.DelayedAction(() =>
        //{
            
        //}, 0.5f));
    }
    void AddCharactor()
    {
        int id = Random.Range(0, LoadAsset.instance.humanConfig.Count);
        int index = str.Count;
        HumanData humanData = LoadAsset.instance.humanConfig[id];
        str.Add($"添加一名伙伴 {humanData.Name}");
        string s = str[index];
        btns[index].onClick.AddListener(() =>
        {
            ActItemManager.instance.GenHumanObject(id);
            
            
            //EquipManager.instance.AttachEquip(PlayerManager.instance.player,id);
            //Debug.Log($"攻击+{GlobelDataManager.instance.equip[id].Str}");
            foreach (GameObject obj in go)
                obj.SetActive(false);
            AfterClick();
        });
    }
    void AddEquip()
    {
        int id = Random.Range(0, LoadAsset.instance.equipConfig.Count);
        int index = str.Count;
        EquipData equipData = LoadAsset.instance.equipConfig[id];
        str.Add($"获得一把 {equipData.Name}");
        string s = str[index];
        btns[index].onClick.AddListener(() =>
        {
            id = EquipManager.instance.GenEquip(id);
            EquipManager.instance.EquipToBag(id);
            //EquipManager.instance.AttachEquip(PlayerManager.instance.player,id);
            //Debug.Log($"攻击+{GlobelDataManager.instance.equip[id].Str}");
            foreach (GameObject obj in go)
                obj.SetActive(false);
            AfterClick();
        });
    }
    void AddMaxHp()
    {
        int num=Random.Range(10,40);
        int index = str.Count;
        str.Add("增加 " + num.ToString() + " 点生命上限");
        string s = str[index];
        btns[index].onClick.AddListener(() =>
        {
            Debug.Log(s);
            PlayerManager.instance.player.HPMax += num;
            PlayerManager.instance.player.HP += num;
            foreach(GameObject obj in go)
            obj.SetActive(false);
            AfterClick();


        });
        
    }
    void AddStr()
    {
        int num = Random.Range(2,20);
        int index = str.Count;
        str.Add("增加 " + num.ToString() + " 点攻击");

        string s = str[index];
        btns[index].onClick.AddListener(() =>
        {
            Debug.Log(s);
            PlayerManager.instance.player.str += num;
            foreach (GameObject obj in go)
                obj.SetActive(false);
            AfterClick();
        });
        
    }
    void AddPercentStr()
    {
        int num = Random.Range(1, 10);
        int index = str.Count;
        str.Add("增加 " + num.ToString() + "% 攻击");

        string s = str[index];
        btns[index].onClick.AddListener(() =>
        {
            Debug.Log(s);
            PlayerManager.instance.player.PerStr += num;
 
            foreach (GameObject obj in go)
                obj.SetActive(false);
            AfterClick();
        });

    }
    void AddPercentDamage()
    {
        int num = Random.Range(1, 10);
        int index = str.Count;
        str.Add($"增加{num}% 伤害加成");

        string s = str[index];
        btns[index].onClick.AddListener(() =>
        {
            Debug.Log(s);
            PlayerManager.instance.player.PerDamage += num;

            foreach (GameObject obj in go)
                obj.SetActive(false);
            AfterClick();
        });

    }
    void AddPercentMag()
    {
        int num = Random.Range(4, 10);
        int index = str.Count;
        str.Add("增加 " + num.ToString() + "% 魔力");

        string s = str[index];
        btns[index].onClick.AddListener(() =>
        {
            PlayerManager.instance.player.PerMag += num;
            foreach (GameObject obj in go)
                obj.SetActive(false);
            AfterClick();
        });

    }
    void AddMag()
    {
        int num = Random.Range(4, 40);
        int index = str.Count;
        str.Add("增加 " + num.ToString() + " 点魔力");

        string s = str[index];
        btns[index].onClick.AddListener(() =>
        {
            Debug.Log(s);
            PlayerManager.instance.player.mag += num;
            foreach (GameObject obj in go)
                obj.SetActive(false);
            AfterClick();
        });

    }
    void AddCir()
    {
        int num = Random.Range(1, 10);
        int index = str.Count;
        str.Add("增加 " + num.ToString() + "% 暴击率");

        string s = str[index];
        btns[index].onClick.AddListener(() =>
        {
            Debug.Log(s);
            PlayerManager.instance.player.Cir += num;
            foreach (GameObject obj in go)
                obj.SetActive(false);
            AfterClick();
        });
        
    }
    void AddCirEffect()
    {
        int num = Random.Range(5, 20);
        int index = str.Count;
        str.Add("增加 " + num.ToString() + "% 暴击伤害");
        string s = str[index];
        btns[index].onClick.AddListener(() =>
        {
            Debug.Log(s);
            PlayerManager.instance.player.CirEffect += num;
            foreach (GameObject obj in go)
                obj.SetActive(false);
            AfterClick();
        });
       
    }
}
