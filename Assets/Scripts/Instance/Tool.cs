using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 mouseLeftPosition;
    public Vector2 mouseRightPosition;
    public Vector2 mousePosition;
    public static Tool instance;
    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
          
           
            mouseLeftPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //PlayerManager.instance.targetPosion = mouseLeftPosition;
        }
        if(Input.GetMouseButtonDown(1))
        {
            mouseRightPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlayerManager.instance.targetPosion = mouseRightPosition;
        }
    }
    void Awake()
    {
        instance=this;
        DontDestroyOnLoad(gameObject);
    }
    public IEnumerator DelayedAction(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();

    }
    public IEnumerator DelayedAction(Action action)
    {
        yield return null;
        action();
    }

}
