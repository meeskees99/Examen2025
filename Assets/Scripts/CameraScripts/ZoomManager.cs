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
    [SerializeField]
    private float _minDistanceFromPrefab;
    [SerializeField]
    private float _maxDistanceFromPrefab;

    [Header("Objects")]

    public RotationManager RotationManager;
    private Camera _cameraToZoom; // deze camera wordt ge-assigned door RotationManager, daarom "hide" ik het
    private Transform _transformToLookAt; 

    void Start()
    {
        // haal waardes uit RotationManager en zet ze goed in ZoomManager
        _cameraToZoom = RotationManager.CameraToRotate;
        _transformToLookAt = RotationManager.TransformToLookAt;
    }

    void Update()
    {
        // bereken snelheid scrollen en beweeg camera gebaseerd op de uitkomende waarde
        _currentZoomSpeed = Input.mouseScrollDelta.y * _zoomSpeed;

        // scrollen werkt alleen binnen limitatie
        if(_distanceFromPrefab > _minDistanceFromPrefab && _distanceFromPrefab < _maxDistanceFromPrefab)
        {
            // beweeg camera
            _cameraToZoom.transform.Translate(Vector3.forward * _currentZoomSpeed * Time.deltaTime, Space.Self);
        }

        // bereken distance 
        _distanceFromPrefab = Vector3.Distance(_cameraToZoom.transform.position, _transformToLookAt.position);

        // zet distanceFromPrefab terug naar waardes binnen de limitatie
        _distanceFromPrefab = Mathf.Clamp(_distanceFromPrefab, _minDistanceFromPrefab, _maxDistanceFromPrefab);

        // pas distance in rotation manager aan
        RotationManager._currentDistanceFromTarget = _distanceFromPrefab;

    }
}
