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
    public bool isDeveloperMode;

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {


            mouseLeftPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //PlayerManager.instance.targetPosion = mouseLeftPosition;
        }
        if (Input.GetMouseButtonDown(1))
        {
            mouseRightPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlayerManager.instance.targetPosion = mouseRightPosition;
        }
        if (isDeveloperMode) DeveloperMode();
    }
    void Awake()
    {
        instance = this;
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
    void DeveloperMode()
    {
        Debug.Log("¿ª·¢Õß");
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                int id = EquipManager.instance.GenEquip(0);
                EquipManager.instance.EquipToBag(id);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                int id = EquipManager.instance.GenEquip(1);
                EquipManager.instance.EquipToBag(id);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                int id = EquipManager.instance.GenEquip(2);
                EquipManager.instance.EquipToBag(id);
            }

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ActItemManager.instance.GenHumanObject(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ActItemManager.instance.GenHumanObject(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ActItemManager.instance.GenHumanObject(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ActItemManager.instance.GenHumanObject(4);
            }
        }
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    ActItemManager.instance.GenHumanObject(2);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    ActItemManager.instance.GenHumanObject(3);
        //}

    }
}
