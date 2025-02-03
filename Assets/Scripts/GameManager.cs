using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 mouseLeftPosition;
    public Vector2 mouseRightPosition;
    public bool isClick = false;
    public Button btn;
    public int tipListIndex;
 
    void Start()
    {
        btn=GameObject.Find("Canvas/GameOver").transform.GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        
        //StartCoroutine(DelayedAction(()=>{tip.gameObject.SetActive(false); },60f));
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{ 

        //    isClick = true;
        //    //Vector3 screenPosition=Input.mousePosition;
        //    mouseLeftPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    //RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        //    //if (hit.collider != null)
        //    //{
        //    //    mouseLeftPosition = hit.point;
        //    //}
            
        //}
        if (Input.GetMouseButtonDown(1))
        {
            mouseRightPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //GenSlime(mouseRightPosition);
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    mouseLeftPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //}

    }
    //void GenSlime(Vector2 position)
    //{
    //    //GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Slime"));
    //    GameObject go = ActItemPool.instance.GetPool("Slime");
    //    go.transform.position = position;
    //    Slime S = go.transform.GetComponent<Slime>();
    //    S.color = Random.ColorHSV();
    //    S.moveSpeed = Random.Range(0.01f, 2f);
    //    S.maxDistance = Random.Range(1f, 5f);
    //}
    public IEnumerator DelayedAction(Action action,float delay)
    {
        yield return new WaitForSeconds(delay); 

        action();
    }

}
