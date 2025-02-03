using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlinePlayerController : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    private event Action move;
    public bool isMove;
    private float elapsedTime;
    private float moveDuration;
    public Vector2 velocityDirection;
    public float flip = 1f;
    void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position;
    }
    void Update()
    {
        
        move?.Invoke();
        Flip();
    }
    public void AddMove(Action action=null)
    {

        elapsedTime = 0f;
        isMove = true;
        moveDuration = 0.5f;
        move =(() =>
        {
            if (elapsedTime >= moveDuration)
            {

                transform.position = endPosition;
                isMove=false;
                move = null;
            }
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);

            velocityDirection = (Vector2)endPosition - (Vector2)startPosition;
            transform.position=Vector3.Lerp(startPosition, endPosition, t); 
        })+action;
    }
    public void Flip()
    {
        if(velocityDirection.x<0)
        transform.localScale=new Vector3(-flip,flip,flip);
        else transform.localScale=new Vector3(flip,flip,flip);
    }
}
