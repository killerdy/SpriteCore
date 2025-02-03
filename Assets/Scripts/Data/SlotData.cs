using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotData : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public int ID=-1;
    public Image image;
    public Text num;
    public GameObject introduce;
    public GameObject introduceGo;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetSize(float x,float y)
    {
        GetComponent<RectTransform>().sizeDelta= new Vector2(x,y);
    }
    public void onClick()
    {
        if (ID < 0) return;
        if (BagManager.instance.equipSet.Contains(ID))
        {
            if(PlayerManager.instance.player.equipments.Count < 3)
            {
                EquipManager.instance.AttachEquip(PlayerManager.instance.player, ID);
                SetId(-1);
            }
        }
        else
        {
            EquipManager.instance.DetachEquip(PlayerManager.instance.player, ID);
            SetId(-1);
        }
           
        
    }
    public void SetId(int id)
    {

        ID = id;
        if (id < 0)
        {
            Debug.Log(id);
            image.enabled = false;
            num.enabled = false;
        }
        else
        {
            image.sprite = Resources.Load<Sprite>(GlobelDataManager.instance.equip[id].ImagePath);
            image.enabled = true;
            num.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ID < 0)
            return;
        introduceGo = Instantiate(introduce, GameObject.Find("Canvas").GetComponent<RectTransform>());
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").GetComponent<RectTransform>(), eventData.position, eventData.enterEventCamera, out localPoint);
        introduceGo.GetComponent<RectTransform>().localPosition = localPoint+new Vector2(10,10);
        introduceGo.transform.GetChild(0).GetComponent<Text>().text = GlobelDataManager.instance.equip[ID].Name;
        introduceGo.transform.GetChild(1).GetComponent<Text>().text = EquipManager.instance.Attribute(ID);
        introduceGo.transform.GetChild(2).GetComponent<Text>().text = GlobelDataManager.instance.equip[ID].Description;
        StartCoroutine(Tool.instance.DelayedAction(() =>
        {
            Destroy(introduceGo); 
        },5f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(introduceGo);
    }
}
