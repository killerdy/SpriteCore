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
        tipList.Add("��ã������ʼ��Ϸ��");

        // ��������
        AddTip("�� [Esc] ������");
        AddTip("�� [B] �򿪱��������װ����");
        tipList.Add("���������ų��������������");

        // �ƶ��Ͳ���
        AddTip("����Ҽ����ƽ�ɫ�ƶ���");
        AddTip("�ƶ������ƴ��ڡ�");
        AddTip("�� [R] �л�������");

        // ս�����
        AddTip("�����ͷż��ܣ�ǹе��׼�����");
        AddTip("�� [A] ������ͨ������");
        AddTip("��ס [E] �������ܣ����ָ�������ɿ� [E] �ͷţ���ʾ����");
        AddTip("ֻ�з��Ȳ����ͷż���Ŷ��");
        AddTip("�� [W] �ý�ɫתȦȦ��");
        AddTip("�� [F] ����ǹе��׼��ֻ��ǹе���������");

        // ������ʾ
        tipList.Add("ָ��������ף����Ϸ��죡");
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
