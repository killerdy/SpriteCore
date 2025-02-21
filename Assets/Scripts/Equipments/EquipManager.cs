using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager instance;
    private void Awake()
    {
        instance = this;
    }
    public string Attribute(int id)
    {
        string ret = "";
        EquipData equip = GlobelDataManager.instance.equip[id];
        if (equip.Str != 0)
            ret += $"¹¥»÷: {equip.Str}\n";
        if (equip.Mag != 0)
            ret += $"Ä§Á¦: {equip.Mag}\n";
        if (equip.HPMax != 0)
            ret += $"ÉúÃüÉÏÏÞ: {equip.HPMax}\n";
        if (equip.Def!= 0)
            ret += $"·ÀÓù: {equip.Def}\n";
        if (equip.Cir != 0)
            ret += $"±©»÷: {equip.Cir}\n";
        if (equip.CirEEfect != 0)
            ret += $"±©»÷ÉËº¦: {equip.CirEEfect}\n";
        return ret;
    }
    public int GenEquip(int id)
    {

        //EquipData a=new EquipData();
        //EquipData equipData = (EquipData)a.MemberwiseClone();
        EquipData equipData = LoadAsset.instance.equipConfig[id].ShallowCopy();
        if (id==0)
            equipData.Str += Random.Range(0, 10);
        GlobelDataManager.instance.equip.Add(++GlobelDataManager.instance.dataConfig.equipId,equipData);
        return GlobelDataManager.instance.dataConfig.equipId;
    }
    public void EquipToBag(int id)
    {
        BagManager.instance.equipSet.Add(id);
        if (BagPanel.instance != null)
            BagPanel.instance.OnEnable();
        CharacterPanel.instance.SetAttribute();
        //int x = -1;

        //for (int i = 0; i < BagPanel.instance.row * BagPanel.instance.col; i++)
        //    if (BagPanel.instance.slots[i].transform.childCount == 0)
        //    {
        //        x = i;
        //        break;
        //    }
        //if (x < 0) return;
        //id = EquipManager.instance.GenEquip(id);
        //GameObject equipData = Instantiate(BagPanel.instance.slotData, BagPanel.instance.slots[x].transform);
        //equipData.GetComponent<SlotData>().SetId(id);
        //equipData.GetComponent<SlotData>().SetSize(BagPanel.instance.slotLength, BagPanel.instance.slotLength);
    }
    public void AttachEquip(ActItem actItem,int id)
    {

        
        EquipData equip = GlobelDataManager.instance.equip[id];
        BagManager.instance.equipSet.Remove(id);
        actItem.equipments.Add(id);
        //if(actItem.equipments.Count>=actItem.EquipMax)
        //{
        //    if (!BagManager.instance.equipSet.Contains(id))
        //    {
        //        BagManager.instance.equipSet.Add(id);
        //    }

        //    return;
        //}
        //PlayerManager.instance.p
        actItem.equipments.Add(id);
        actItem.str += equip.Str;
        actItem.mag += equip.Mag;
        actItem.HPMax += equip.HPMax;
        //actItem.HP += equip.HP;
        actItem.Cir += equip.Cir;
        actItem.CirEffect += equip.CirEEfect;


        CharacterPanel.instance.OnEnable();
        BagPanel.instance.OnEnable();
    }
    public void DetachEquip(ActItem actItem, int id)
    {
        EquipData equip = GlobelDataManager.instance.equip[id];

        actItem.str -= equip.Str;
        actItem.mag -= equip.Mag;
        actItem.HPMax -= equip.HPMax;
        //actItem.HP -= equip.HP;
        actItem.Cir -= equip.Cir;
        actItem.CirEffect -= equip.CirEEfect;


        BagManager.instance.equipSet.Add(id);
        actItem.equipments.Remove(id);


        CharacterPanel.instance.OnEnable();
        BagPanel.instance.OnEnable();
    }
    
}
