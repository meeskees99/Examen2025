using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomManager : MonoBehaviour
{
    // -------- VARIABLES --------

    [Header("Values")]

    [SerializeField]
    private float _zoomSpeed; // gevoeligheid/sensitiviteit van zoomen

    private float _currentZoomSpeed; // huidige zoom snelheid
    private float _distanceFromPrefab; // huidige afstand van prefab

    // limieten
    private float _minimumDistanceFromPrefab;
    private float _maximumDistanceFromPrefab;

    [Header("Objects")]

    [HideInInspector]
    public Camera CameraToRotate; // deze camera wordt ge-assigned door RotationManager, daarom "hide" ik het

    void Start()
    {
        
    }

    void Update()
    {
        _currentZoomSpeed = Input.mouseScrollDelta.y * _zoomSpeed;

        CameraToRotate.transform.forward += new Vector3(_currentZoomSpeed, 0, 0);
    }
}
