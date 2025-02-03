using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public Transform player;
    private Vector3 nameOffset=new Vector3(0,-0.55f,0);
    public  void SetPlayer(Transform transform)
    {
        player = transform;
        Debug.Log("∂‘œÛ"+player);
    }
    // Start is called before the first frame update
    void Awake()
    {
        transform.SetParent(GameObject.Find("CanvasWorld").transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position= player.position+nameOffset;
    }
}
