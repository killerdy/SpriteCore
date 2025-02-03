using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    // Start is called before the first frame update
    public BasePanel left,middle,right,setting,tip,choose;
    public Font font;
    public static MainPanel instance;
    public List<string> tipList = new List<string>();
    public int tipListIndex;
    

    void Start()
    {
        instance = this;
        height = Screen.height;
        width = Screen.width;
        SetActive();
        base.SetSize(width, height);
        int childWidth = (width) / 3;
        left.SetSize(childWidth, height);
        middle.SetSize(childWidth, height); 
        right.SetSize(childWidth, height);

        left.SetActive();
        right.SetActive();
        middle.SetActive();
        StartCoroutine(Tool.instance.DelayedAction(() =>
        {
            left.SetActive();
            right.SetActive();
            middle.SetActive();
        }));
        
        setting.SetSize(width/4, height/5*4);
        tip.SetSize(childWidth/2, height / 6);
        choose.SetSize(width/2,height/2);
        tip.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta =new Vector2(childWidth*2/3, height / 6);
        tip.transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1);
        ConfigTips();
        ClickTips();
    }
    void ConfigTips()
    {
        tipList.Add("你好，点击开始游戏！");

        // 基础操作
        AddTip("按 [Esc] 打开设置");
        AddTip("按 [B] 打开背包，点击装备。");
        tipList.Add("鼠标滚轮缩放场景，点击继续。");

        // 移动和操作
        AddTip("鼠标右键控制角色移动。");
        AddTip("移动鼠标控制窗口。");
        AddTip("按 [R] 切换武器。");

        // 战斗相关
        AddTip("法杖释放技能，枪械瞄准射击。");
        AddTip("按 [A] 进行普通攻击。");
        AddTip("按住 [E] 引导技能，鼠标指引方向，松开 [E] 释放（演示）。");
        AddTip("只有法杖才能释放技能哦！");
        AddTip("按 [W] 让角色转圈圈。");
        AddTip("按 [F] 进入枪械瞄准，只有枪械才能射击！");

        // 结束提示
        tipList.Add("指引结束，祝你游戏愉快！");
    }
    void ClickTips()
    {
        tip.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (tipListIndex == tipList.Count)
            {
                tip.gameObject.SetActive(false);
            }
            else tip.GetComponentInChildren <Text>().text = tipList[tipListIndex++];

        });
    }
    public void AddTip(string str)
    {
        tipList.Add(str);
    }
    // Update is called once per frame
    void Update()
    {
        PanelControl();
    }
    void PanelControl()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            
            left.SetActive();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
         
            middle.SetActive();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
           
            right.SetActive();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            setting.SetActive();    
        }
        //if(Input.GetKeyDown(Key))
    }
}
