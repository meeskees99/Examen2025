using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCamera : MonoBehaviour
{
    public RotationManager RotationManager;

    [HideInInspector]
    public bool ResetViewerCamera;
    void Update()
    {
        // reset camera door input of door CanvasManager
        if(Input.GetMouseButtonDown(2) || ResetViewerCamera)
        {
            // reset camera position/rotation
            RotationManager.CameraToRotate.transform.position = new Vector3(0, 0, -5);
            RotationManager.CameraToRotate.transform.rotation = new Quaternion();

            RotationManager.TransformToLookAt.transform.position = new Vector3(0, 0, 0);
            RotationManager.TransformToLookAt.transform.rotation = new Quaternion();


            // reset values
            RotationManager.ResetValues();
            RotationManager.CurrentCameraRotation = new Quaternion();
            ResetViewerCamera = false;
        }
    }
}
