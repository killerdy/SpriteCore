using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyManager : MonoBehaviour
{
    [System.Serializable]
    public struct SkyData
    {
        public string name;
        public Sprite sprite;
        public float startTime;
        public float endTime;

    }
    public List<SkyData> skyList=new List<SkyData>();
    public float timeOfDay = 0;
    public float dayDuartion = 240f;
    public float transtionDuration = 2f;
    private float currentTranstionTime = 0f;
    public SpriteRenderer[] spriteRenderer;
    private int rd = 0;
    private Sprite currentSkySprite;
    private Sprite nextSkySprite;
    public static SkyManager instance;
   
    void Start()
    {
        //cam = GameObject.Find("Main Cameraf")
        instance = this;
        //gameObject.SetActive(true);
        Object[] resources;
        resources = Resources.LoadAll("Sky");
        string name = "";
        Sprite sprite;
        float startTime = 0f;
        float endTime = dayDuartion/ resources.Length*2;
        foreach(Object o in resources)
        {
            if (!(o is Sprite)) continue;
            name= o.name;
            sprite = o as Sprite;
            skyList.Add(new SkyData{ name=name,sprite=sprite,startTime=startTime,endTime = endTime });
            startTime = endTime;
            endTime += dayDuartion / resources.Length*2;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay += Time.deltaTime;
        timeOfDay %= dayDuartion;
        
        UpdateSky();
        UpdateTransition();
    }

    void UpdateSky()
    {
       
        SkyData currentSky=GetCurrentSky();
        if (currentSkySprite == null)
        {
            currentSkySprite = nextSkySprite = currentSky.sprite;
        }
        if(currentSky.sprite!=currentSkySprite)
        {
            nextSkySprite = currentSky.sprite;
        }
 
    }
    SkyData GetCurrentSky()
    {
        foreach (SkyData sky in skyList)
        {
            if (timeOfDay >= sky.startTime && timeOfDay <= sky.endTime)
                return sky;
        }
        return skyList[0];
    }
    void UpdateTransition()
    {
        
        if (currentSkySprite != nextSkySprite)
        {
            currentTranstionTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentTranstionTime / transtionDuration);
            Color color =Color.Lerp(new Color(1,1,1,0),new Color(1,1,1,1),progress);
            spriteRenderer[rd++].color = color;
            rd %= 2;
            spriteRenderer[rd].sprite = nextSkySprite;
            if (progress >= 1)
            {
                currentSkySprite = nextSkySprite;
                spriteRenderer[rd].color = new Color(1, 1, 1, 1);
                currentTranstionTime = 0f;
            }
           
        }
       
    }
}
