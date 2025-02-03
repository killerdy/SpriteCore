using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadUpManager : MonoBehaviour
{
    public static HeadUpManager instance;
    public Transform hp, mp, exp;
    public Text textHp, textMp,textName,textLv;
    public Image imageHp, imageMp, imageExp;
    // Start is called before the first frame update
    public ActItem player;
    void Start()
    {
        instance = this;
        textHp=hp.GetComponentInChildren<Text>();
        textMp=mp.GetComponentInChildren<Text>();
        //StartCoroutine(Tool.instance.DelayedAction(() =>
        //{
        //    ChangePlayer();
        //},1f));
        
        //textExp=exp.GetComponentInChildren<Text>(); 
        //imageHp=hp.GetComponentInChildren<Image>();
        //imageMp=mp.GetComponentInChildren<Image>();
        //imageExp =exp.GetComponentInChildren<Image>();

    }
    public void ChangePlayer()
    {
        
        player=PlayerManager.instance.player;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        textName.text = player.Name;
        textHp.text = player.HP.ToString() + "/" + player.HPMax.ToString();
        textMp.text = player.MP.ToString() + "/" + player.MPMax.ToString();
        textLv.text="Rank:"+player.level.ToString();
        imageExp.fillAmount= (float)(player.exp * 1.0 / player.ExpMax);
        imageHp.fillAmount = (float)(player.HP * 1.0 / player.HPMax);
        imageMp.fillAmount = (float)(player.MP * 1.0 / player.MPMax);
    }
}
