using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : BasePanel
{
    // Start is called before the first frame update
    public GameObject content;
    public GameObject viewer;
    public GameObject text;
    public GameObject equip;
    public GameObject slot;
    public GameObject slotData;
    public GridLayoutGroup grid;
    public GridLayoutGroup gridEquip;
    public GameObject prefab;
    public static CharacterPanel instance;
    public float padding = 5f;
    public float childLength;
    public List<Text> texts = new List<Text>();
    public List<GameObject> slots=new List<GameObject>();
    //public List<SlotData> slotList = new List<SlotData>();
    public void OnEnable()
    {
        SetAttribute();
        //ClearEquip();
        //int i = 0;
        //foreach (int id in PlayerManager.instance.player.equipments)
        //{
        //    slots[i].transform.GetChild(0).GetComponent<SlotData>().SetId(id);
        //    i++;
        //}
        //for (; i < 3; i++)
        //{
        //    slots[i].transform.GetChild(0).GetComponent<SlotData>().SetId(-1);
        //}
        HashSet<int> set = new HashSet<int>(PlayerManager.instance.player.equipments);
        List<int> box = new List<int>();
        for (int i = 0; i < 3; i++)
            if (set.Contains(slots[i].transform.GetChild(0).GetComponent<SlotData>().ID))
            {
                set.Remove(slots[i].transform.GetChild(0).GetComponent<SlotData>().ID);
            }
            else
            {
                box.Add(i);

            }
        int x = 0;
        foreach (int id in set)
        {
            slots[box[x++]].transform.GetChild(0).GetComponent<SlotData>().SetId(id);
        }

    }
    public void ClearEquip()
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].transform.GetChild(0).GetComponent<SlotData>().SetId(-1);
    }
    private void Awake()
    {
        instance = this;
        SetGrid();
    }
    public override void SetSize(int x, int y)
    {
        base.SetSize(x, y);
        int offset = y / 5;

        text.GetComponent<RectTransform>().sizeDelta = new Vector2(width, offset / 2);
        viewer.GetComponent<RectTransform>().localPosition = new Vector2(20f, -offset);
        viewer.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height - 2 * offset);

        equip.GetComponent<RectTransform>().localPosition = new Vector2(0, -3 * offset);
        equip.GetComponent<RectTransform>().sizeDelta = new Vector2(width, offset);
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height * 3);
    }
    void SetGrid()
    {
        childLength = (width)/2-10;

        //grid.padding = new RectOffset((int)padding*3, 0, 0, 0);
        grid.cellSize = new Vector2(childLength, childLength/5);
        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(prefab,content.transform);
            texts.Add(go.GetComponent<Text>());
        }
        int slotLength = (width - 30) / 5;




        gridEquip.padding = new RectOffset((int)padding, (int)padding, (int)padding, (int)padding);
        gridEquip.cellSize = new Vector2(slotLength, slotLength);
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(slot, equip.transform);
            GameObject equipData = Instantiate(slotData, go.transform);
            equipData.GetComponent<SlotData>().SetId(-1);
            equipData.GetComponent<SlotData>().SetSize(slotLength, slotLength);
            slots.Add(go);
        }
    }
    public void SetAttribute()
    {
        texts[0].text = $"生命上限: {PlayerManager.instance.player.HPMax}";
        texts[1].text = $"魔力上限: {PlayerManager.instance.player.MPMax}";
        texts[2].text = $"基础攻击力: {PlayerManager.instance.player.str}";
        texts[3].text = $"攻击力加成: {PlayerManager.instance.player.PerStr}%";
        texts[4].text = $"基础法力值: {PlayerManager.instance.player.mag}";
        texts[5].text = $"法力值加成: {PlayerManager.instance.player.PerMag}%";
        texts[6].text = $"伤害加成: {PlayerManager.instance.player.PerDamage}";
        texts[7].text = $"暴击率: {PlayerManager.instance.player.Cir}%";
        texts[8].text = $"暴击伤害: {PlayerManager.instance.player.CirEffect}%";
    }
    void Start()
    {
        
        SetAttribute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
