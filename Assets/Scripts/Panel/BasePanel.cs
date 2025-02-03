using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    // Start is called before the first frame update
    //public Image image;
    public RectTransform rect;
    public bool isVisible=false;
    public int height, width;

    public virtual void SetSize(int x,int y)
    {
        height = y; width = x;
        rect.sizeDelta = new Vector2(width,height);
        
    }

    public void SetActive()
    {
        
        isVisible = !isVisible;
        gameObject.SetActive(isVisible);
    }
    void Start()
    {
        //SetActive();
        //image.  sprite=Resources.Load<>
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
