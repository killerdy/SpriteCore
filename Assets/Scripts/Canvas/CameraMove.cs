using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove instance;
    public Camera mainCamera;
    // Start is called before the first frame update
    public float borderSize = 0.1f;
    public float moveSpeed = 1f;
    public float smoothTime = 0.2f;
    private float zoomSpeed = 100f;
    public float zoomSmoothTime = 0.1f;
    public float minSize = 2f;
    public float maxSize = 10f;
    public Vector3 currentVelocity;
    private float currenSizeVelocity;
    void Start()
    {
        instance = this;
        mainCamera = GetComponent<Camera>();
    }
    void Update()
    {
        HandleSize();
        //if(!BagPanel.instance.isChick)
        if(BagPanel.instance == null||!BagPanel.instance.isVisible)
            HandlePan();
    }
    // Update is called once per frame
    void HandlePan()
    {
        Vector3 mouseScreenPosition=Input.mousePosition;
        Vector3 mouseViewerPosition=mainCamera.ScreenToViewportPoint(mouseScreenPosition);
        float offsetX = 0f;
        float offsetY = 0f;
        if (mouseViewerPosition.x < borderSize)
            offsetX = mouseViewerPosition.x - borderSize;
        else if (mouseViewerPosition.x > 1 - borderSize) 
            offsetX = mouseViewerPosition.x -(1 - borderSize);
        if (mouseViewerPosition.y < borderSize)
            offsetY = mouseViewerPosition.y - borderSize;
        else if (mouseViewerPosition.y > 1 - borderSize)
            offsetY = mouseViewerPosition.y - (1 - borderSize);
        if (offsetX != 0 || offsetY != 0)
        {
            Vector3 moveDirection = new Vector3(offsetX,offsetY,0f);
            Vector3 targetPosition=transform.position+moveDirection*moveSpeed*mainCamera.orthographicSize;
            transform.position= Vector3.SmoothDamp(transform.position, targetPosition,ref currentVelocity, smoothTime);
        }
    }
    void HandleSize()
    {
        float scrollInpout = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInpout != 0f)
        {
            float targetSize = mainCamera.orthographicSize - scrollInpout * zoomSpeed;
            targetSize = Mathf.Clamp(targetSize, minSize, maxSize);
            mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, targetSize, ref currenSizeVelocity, 0.01f);

        }
        
    }
}
