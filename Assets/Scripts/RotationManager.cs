using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.GraphicsBuffer;

public class RotationManager : MonoBehaviour
{
    /* Ik gebruik hier een enum om de huidige status bij te houden van de rotation manager,
    zodat ik duidelijk in stapjes het process kan coderen */
    public enum RotationState
    {
        NotActive,
        Rotating
    }

    // -------- VARIABLES --------

    [Header("Values")]

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _slerpSpeed;

    [SerializeField]
    private float _tiltSpeed;

    private float _horizontalMouseSpeed;
    private float _verticalMouseSpeed;

    private float _xRotation;
    private float _yRotation;

    private float _currentDistanceFromTarget = 5;

    private Quaternion _currentCameraRotation;

    [Header("Objects")]

    public Camera CameraToRotate;
    public Transform TransformToLookAt; // position waar de camera altijd naar kijkt
    public GameObject Prefab;


    void Start()
    {
        _yRotation = CameraToRotate.transform.eulerAngles.y;
        _xRotation = CameraToRotate.transform.eulerAngles.x;

        _currentCameraRotation = CameraToRotate.transform.rotation;
    }

    void Update()
    {
        // code is alleen actief als de linker muis knop is ingedrukt en er een positie is om naar te kijken
        if(Input.GetMouseButton(0) && TransformToLookAt != null)
        {
            print("working");
            CalculateMouseSpeed(); // calculeer muis snelheid

            _xRotation += _horizontalMouseSpeed;
            _yRotation -= _verticalMouseSpeed;

            // bereken hier de rotatie waar de user heen wil met muis
            Quaternion targetRot = Quaternion.Euler(_xRotation, _yRotation, 0);

            // de formule voor het berekenen van een smooth lerp naar nieuwe rotatie
            _currentCameraRotation = Quaternion.Slerp(_currentCameraRotation, targetRot, Time.deltaTime * _slerpSpeed);

            // zet afstand van object in Vector3 formaat
            Vector3 direction = new Vector3(0, 0, -_currentDistanceFromTarget);
            

            RotateCamera(direction); // roteer camera
        }
    }

    // functie om de camera te roteren
    void RotateCamera(Vector3 direction)
    {
        CameraToRotate.transform.position = TransformToLookAt.position + _currentCameraRotation * direction;
        CameraToRotate.transform.LookAt(TransformToLookAt.position); // camera kijkt nogsteeds naar rotatie punt
    }

    // functie om de camera te tilten
    void TiltCamera()
    {

    }

    // calculeer hier de snelheid van de muis van de user
    void CalculateMouseSpeed()
    {
        // bereken muis snelheid
        _horizontalMouseSpeed = Input.GetAxis("Mouse Y") * _rotationSpeed * Time.deltaTime;
        _verticalMouseSpeed = Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;
    }
}
