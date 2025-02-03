using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Utils : MonoBehaviour
{
    
    public int targetHeight = 240; // 目标分辨率高度
    private Camera cam;
    private Canvas canvas;
    void Start()
    {
        cam = GetComponent<Camera>();
        canvas = GetComponent<Canvas>();
        UpdateCamera();
    }

    void Update()
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }
        UpdateCamera();
    }
    void UpdateCamera()
    {
        int screenHeight = Screen.height;
        targetHeight = Mathf.RoundToInt(Mathf.Min(canvas.GetComponent<RectTransform>().rect.height, canvas.GetComponent<RectTransform>().rect.width));
        int scalingFactor = 1;

        for (int i = 1; i <= 100; i++)
        {
            if (targetHeight * i <= screenHeight)
            {
                scalingFactor = i;
            }
            else
            {
                break;
            }
        }

        // 计算正交相机大小
        float orthographicSize = targetHeight / 2f / (float)scalingFactor;

        // 应用正交相机大小
        cam.orthographicSize = orthographicSize;
    }
}
