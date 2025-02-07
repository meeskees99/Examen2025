using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomManager : MonoBehaviour
{
    // -------- VARIABLES --------

    [Header("Values")]

    [SerializeField]
    private float _zoomSpeed; // gevoeligheid/sensitiviteit van zoomen

    [SerializeField]
    private float _distanceSpeedChange; // veranderd speed gebaseerd op distance

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
        // bereken afstand na beweging
        float distanceFromPrefab = Vector3.Distance(_cameraToZoom.transform.position, _transformToLookAt.position);

        // pas distance aan in RotationManager
        RotationManager.CurrentDistanceFromTarget = distanceFromPrefab;


        // bereken distance speed offset
        float distanceSpeedModifier = distanceFromPrefab * _distanceSpeedChange;

        // bereken zoom speed
        float currentZoomSpeed = Input.mouseScrollDelta.y * _zoomSpeed * distanceSpeedModifier;

        // bereken de mogelijke nieuwe camera positie
        Vector3 nextPosition = _cameraToZoom.transform.position + _cameraToZoom.transform.forward * currentZoomSpeed * Time.deltaTime;

        // bereken afstand 
        float nextDistance = Vector3.Distance(nextPosition, _transformToLookAt.position);

        // kijk of het binnen limiet blijft
        if (nextDistance >= _minDistanceFromPrefab && nextDistance <= _maxDistanceFromPrefab)
        {
            // Beweeg de camera alleen als de nieuwe afstand binnen de grenzen ligt
            _cameraToZoom.transform.position = nextPosition;
        }

    }
}
