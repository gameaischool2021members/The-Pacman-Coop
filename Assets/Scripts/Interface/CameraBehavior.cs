using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    public float scrollSpeed = 15.0f;
    //private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;
    private bool Fixed = false;
    // Use this for initialization
    void Start () {
        //ResetCamera = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Fixed)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (Camera.main.orthographicSize > 1)
                {
                    Camera.main.orthographicSize -= scrollSpeed;
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                Camera.main.orthographicSize += scrollSpeed;
            }
        }
    }

    void LateUpdate()
    {
        if (!Fixed)
        {
            if (Input.GetMouseButton(1))
            {
                Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
                if (Drag == false)
                {
                    Drag = true;
                    Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
            {
                Drag = false;
            }
            if (Drag == true)
            {
                Camera.main.transform.position = Origin - Diference;
            }
            /*
            //reset la camera avec un click gauche
            if (Input.GetMouseButton(1))
            {
                Camera.main.transform.position = ResetCamera;
            }
            */
        }
    }

    public void FixedCamera(bool isFixed)
    {
        Fixed = isFixed;
    }
}