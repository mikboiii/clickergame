using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector3 Origin;
    Vector3 Difference;
    Vector3 ResetCamera;

    bool drag = false;

    private void Start()
    {
        ResetCamera = Camera.main.transform.position;
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if(drag == false)
            {
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }

        if (drag)
        {
            Camera.main.transform.position = Origin - Difference;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Camera.main.transform.position = ResetCamera;
        }
    }
}
