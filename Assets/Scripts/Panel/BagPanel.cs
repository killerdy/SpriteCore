using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : BasePanel
{
    
    public GameObject slot;
    public GameObject content;
    public GameObject viewer;
    public GameObject text;
    public GameObject slotData;
    public int col = 5;
    public int row = 10;
    public GridLayoutGroup grid;
    public float padding = 5f;

    public List<GameObject> slots = new List<GameObject>();
    public List<SlotData> slotList = new List<SlotData>();

    public float slotLength;
    public static BagPanel instance;
    private void Awake()
    {
        instance = this;
        SetGrid();
        CreateSlots();
    }
    public override void SetSize(int x, int y)
    {
        base.SetSize(x, y);
        int offset = y / 5;

        text.GetComponent<RectTransform>().sizeDelta =new Vector2(width,offset/2);
        viewer.GetComponent<RectTransform>().localPosition=new Vector2(0,-offset);
        viewer.GetComponent<RectTransform>().sizeDelta= new Vector2(width, height-2*offset);
        //content.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height*3);
    }
    void Start()
    {
        
        OnEnable();
    }
    void SetGrid()
    {
        slotLength = (rect.rect.width-padding*(col+1))/ col;
        grid.padding = new RectOffset((int)padding, (int)padding, (int)padding, (int)padding);
        grid.cellSize=new Vector2(slotLength, slotLength);
    }
    //public void OnLoad()
    //{
        
    //}
    public void OnEnable()
    {
        //int i = 0;
        //foreach(int id in BagManager.instance.equipSet)
        //{
        //    slots[i].transform.GetChild(0).GetComponent<SlotData>().SetId(id);
        //    i++;
        //}
        //for (; i < col * row; i++)
        //{
        //    slots[i].transform.GetChild(0).GetComponent<SlotData>().SetId(-1);
        //}
        HashSet<int> set = new HashSet<int>(BagManager.instance.equipSet);
        List<int> box = new List<int>();
        for (int i = 0; i < row * col; i++)
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
    void CreateSlots()
    {

        for (int i = 0; i < col * row; i++)
        {
            GameObject go = Instantiate(slot, content.transform);
            GameObject equipData = Instantiate(slotData, go.transform);
            equipData.GetComponent<SlotData>().SetId(-1);
            equipData.GetComponent <SlotData>().SetSize(slotLength, slotLength); 
            slots.Add(go);
        }

        //GenEquip(0);

        //GenEquip(1);
        //int ID = EquipManager.instance.GenEquip(0);
        //GameObject equipData = Instantiate(slotData, slots[0].transform);
        //equipData.GetComponent<SlotData>().SetId(ID);
        //equipData.GetComponent <SlotData>().SetSize(slotLength, slotLength);    
        

        //ID=EquipManager.instance.GenEquip(1);
        //equipData=Instantiate(slotData, slots[1].transform);
        //equipData.GetComponent<SlotData>().SetId(ID);
        //equipData.GetComponent<SlotData>().SetSize(slotLength, slotLength);
        //for (int i=0;i<col*row;i++)
        //{
        //    GameObject go = Instantiate(slot, content.transform);
        //    SlotData slotdata = go.GetComponent<SlotData>();
        //    slotList.Add(slotdata);
        //    go.name = $"Slot_{i}";
        //    slotdata.SetSize(slotLength, slotLength);
        //    slotdata.SetId(-1);

        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
