using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoSingleton<CameraController> {

    public GameObject player;       
    
    protected virtual void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 pos = player.transform.position;
            pos.z = transform.position.z;
            transform.position = pos;
        }      
    }

    public bool CheckInCamera(Vector3 point)
    {
        Vector3 newPoint = GetComponent<Camera>().WorldToViewportPoint(point);
        if (newPoint.x < 0 || newPoint.x > 1 || newPoint.y < 0 || newPoint.y > 1)
        {
            return false;
        }
        else
            return true;
    }
}
