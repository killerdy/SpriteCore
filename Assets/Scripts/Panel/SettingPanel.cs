using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    // Start is called before the first frame update
    
    GridLayoutGroup grid;
    private string[] str = { "退出游戏", "保存游戏","载入游戏" ,"回到主页"};
    List<GameObject>  go = new List<GameObject>();
    List<Button> buttons = new List<Button>();
    void Start()
    {
        StartCoroutine(Tool.instance.DelayedAction(() =>
        {
            grid = GetComponent<GridLayoutGroup>();
            //grid.padding.top = height/ 5;
            grid.cellSize = new Vector2( width,height/10);
            //grid.spacing
            for (int i = 0; i < str.Length; i++)
            {
                go.Add(Instantiate(new GameObject()));
                Text text = go[i].AddComponent<Text>();
                buttons.Add(go[i].AddComponent<Button>());
                text.font=MainPanel.instance.font;
                text.fontSize = 50;
                text.alignment = TextAnchor.MiddleCenter;
                text.text = str[i];
                go[i].transform.SetParent(transform, false);
            }
            buttons[0].onClick.AddListener(() =>
            {
                Application.Quit();
            });
            buttons[1].onClick.AddListener(() => {
                GlobelDataManager.instance.SaveData();
            });
            buttons[2].onClick.AddListener(() => {
                GlobelDataManager.instance.LoadData();
            });
            buttons[3].onClick.AddListener(() => {
                SceneManager.LoadScene("Home");
            });
        }));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
