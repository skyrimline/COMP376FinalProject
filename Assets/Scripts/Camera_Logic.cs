using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class Camera_Logic : MonoBehaviour
{
    //private CinemachineVirtualCamera myCam;
    private Camera myCam;

    private float cameraZoom;
    private float zoomStep = 20;
    private float zoomSpeed = 3;

    private Vector3 cameraPos;
    private float moveSpeed = 6f;
    private float moveAmount = 20f;
    private float edgeSize = 30;
    private bool isMouseDown = false;
    private Vector3 lastMousePosition = Vector3.zero;

    [SerializeField] Transform topLeftConfine;
    [SerializeField] Transform bottomRightConfine;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private float offset = 0;



    // Start is called before the first frame update
    void Start()
    {
        //myCam = GetComponent<CinemachineVirtualCamera>();
        //cameraZoom = myCam.orthographicSize;
        myCam = GetComponent<Camera>();
        cameraZoom = myCam.orthographicSize;
        cameraPos = myCam.transform.position;

        minX = topLeftConfine.position.x;
        maxX = bottomRightConfine.position.x;
        minY = bottomRightConfine.position.y;
        maxY = topLeftConfine.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        HandleZoom();
        HandleMove();
        CheckConfine();
        resetPosition();
    }

    private void HandleZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cameraZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomStep;
            // also update the cameraPos a little step
            //if (Input.GetAxis("Mouse ScrollWheel") > 0) 
            //{
            //    cameraPos = myCam.ScreenToWorldPoint(Input.mousePosition);
            //}
            //cameraPos += (myCam.ScreenToWorldPoint(Input.mousePosition) - cameraPos).normalized * 5;

        }

        float cameraZoomDifference = cameraZoom - myCam.orthographicSize;

        myCam.orthographicSize += cameraZoomDifference * zoomSpeed * Time.deltaTime;
        if (cameraZoomDifference > 0)
        {
            if (myCam.orthographicSize > cameraZoom)
            {
                myCam.orthographicSize = cameraZoom;
            }
        }
        else
        {
            if (myCam.orthographicSize < cameraZoom)
            {
                myCam.orthographicSize = cameraZoom;
            }
        }

        // within range
        if (myCam.orthographicSize > 30)
        {
            myCam.orthographicSize = 30;
            cameraZoom = 30;
        }
        if (myCam.orthographicSize < 6)
        {
            myCam.orthographicSize = 6;
            cameraZoom = 6;
        }

    }

    private void HandleMove()
    {
        // poll for input 
        // right edge
        //if (Input.mousePosition.x > Screen.width - edgeSize || Input.GetKey(KeyCode.D))
        //{
        //    cameraPos.x += moveAmount * Time.deltaTime;
        //}
        //// left edge
        //if (Input.mousePosition.x < edgeSize || Input.GetKey(KeyCode.A))
        //{
        //    cameraPos.x -= moveAmount * Time.deltaTime;
        //}
        //// up edge
        //if (Input.mousePosition.y > Screen.height - edgeSize || Input.GetKey(KeyCode.W))
        //{
        //    cameraPos.y += moveAmount * Time.deltaTime;
        //}
        //// down edge
        //if (Input.mousePosition.y < edgeSize || Input.GetKey(KeyCode.S))
        //{
        //    cameraPos.y -= moveAmount * Time.deltaTime;
        //}

        if (Input.GetKey(KeyCode.D))
        {
            cameraPos.x += moveAmount * Time.deltaTime;
        }
        // left edge
        if (Input.GetKey(KeyCode.A))
        {
            cameraPos.x -= moveAmount * Time.deltaTime;
        }
        // up edge
        if (Input.GetKey(KeyCode.W))
        {
            cameraPos.y += moveAmount * Time.deltaTime;
        }
        // down edge
        if (Input.GetKey(KeyCode.S))
        {
            cameraPos.y -= moveAmount * Time.deltaTime;
        }


        // use middle button to move cam
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            isMouseDown = true;
        }

        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
        {
            isMouseDown = false;
            lastMousePosition = Vector3.zero;
        }
        if (isMouseDown)
        {
            if (lastMousePosition != Vector3.zero)
            {
                Vector3 offset = Input.mousePosition - lastMousePosition;
                cameraPos = cameraPos - offset * 0.05f;
            }
            lastMousePosition = Input.mousePosition;
        }



        Vector3 cameraMoveDir = (cameraPos - transform.position).normalized;
        float distance = Vector3.Distance(cameraPos, transform.position);

        if (distance > 0)
        {
            Vector3 newCamPos = transform.position + cameraMoveDir * distance * moveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCamPos, cameraPos);

            if (distanceAfterMoving > distance)
            {
                // overshot
                newCamPos = cameraPos;
            }

            transform.position = newCamPos;
        }

    }


    private void CheckConfine()
    {
        float camLeft = transform.position.x - myCam.orthographicSize * myCam.aspect;
        float camRight = transform.position.x + myCam.orthographicSize * myCam.aspect;
        float camTop = transform.position.y + myCam.orthographicSize;
        float camBottom = transform.position.y - myCam.orthographicSize;

        if(camLeft < minX)
        {
            transform.position = new Vector3(minX + myCam.orthographicSize * myCam.aspect + offset, transform.position.y, transform.position.z);
            cameraPos.x = transform.position.x;
        }

        if(camRight > maxX)
        {
            transform.position = new Vector3(maxX - myCam.orthographicSize * myCam.aspect - offset, transform.position.y, transform.position.z);
            cameraPos.x = transform.position.x;
        }

        if (camTop > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY - myCam.orthographicSize - offset, transform.position.z);
            cameraPos.y = transform.position.y;
        }

        if (camBottom < minY)
        {
            transform.position = new Vector3(transform.position.x, minY + myCam.orthographicSize + offset, transform.position.z);
            cameraPos.y = transform.position.y;
        }
    }

    private void resetPosition()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cameraPos = new Vector3(3.1f, -5.9f, -10f);
            cameraZoom = 30.0f;
        }
    }
}
