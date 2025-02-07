using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCamera : MonoBehaviour
{
    public RotationManager RotationManager;

    void Update()
    {
        // reset camera
        if(Input.GetMouseButtonDown(2))
        {
            // reset camera position/rotation
            RotationManager.CameraToRotate.transform.position = new Vector3(0, 0, -5);
            RotationManager.CameraToRotate.transform.rotation = new Quaternion();

            RotationManager.TransformToLookAt.transform.position = new Vector3(0, 0, 0);
            RotationManager.TransformToLookAt.transform.rotation = new Quaternion();


            // reset values
            RotationManager.ResetValues();
            RotationManager.CurrentCameraRotation = new Quaternion();
        }
    }
}
