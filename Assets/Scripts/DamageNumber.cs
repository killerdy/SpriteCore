using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime = 1f;
    public float scale=0.008f;
    public float scaleCir = 0.1f;
    public float timer;
    public float speed = 3f;
    public Rigidbody2D rd;
    public Text text;
    private Color color=Color.white;
    void Start()
    {
        //color = Color.white;
        rd = GetComponent<Rigidbody2D>();
        //text = GetComponent<Text>(); 
        StartCoroutine(Tool.instance.DelayedAction(() =>
        {

            StartCoroutine(LerpAction());
        }, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
       
      
    }
    public void SetParams(Vector2 direction,int num,bool isCir,Transform defender)
    {
        //Debug.Log(text);
        text.text=num.ToString();
        //Debug.Log(isCir);
        if (isCir)
        {
            transform.localScale = new Vector3(scaleCir, scaleCir, scaleCir);
            text.fontStyle = FontStyle.Bold;
            color = Color.red;
        }
        direction = direction.normalized;
        direction.y += Random.Range(0,0.2f);
        direction.x += Random.Range(0, 0.1f);
        rd.velocity = direction * speed;
        transform.SetParent(GameObject.Find("CanvasWorld").transform);
        transform.position = defender.position;

    }

    IEnumerator LerpAction()
    {
        timer = 0f;
        rd.velocity = Vector2.zero;
        rd.gravityScale = 0;
        while (timer < lifeTime)
        {
            timer += Time.deltaTime;
            float fadeAmount=Mathf.Lerp(1,0,timer/lifeTime);
            text.color = new Color(color.r, color.b, color.g,fadeAmount);
            yield return null;

        }
        Destroy(gameObject);
    }
}
