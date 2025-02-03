using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static CursorManager instance;
    public static CursorManager Instance
    {
        get { 
            return instance; 
        }
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture,new Vector2(texture.width/2,texture.height/2),CursorMode.Auto);
    }
    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
