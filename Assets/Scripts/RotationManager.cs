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
        Rotating,
        Tilting
    }

    // -------- VARIABLES --------

    [Header("Values")]

    // hier hou ik bij in welke modus de code zit
    private RotationState _rotationState;


    [SerializeField]
    private float _rotationSpeed; // gevoeligheid/sensitiviteit van rotatie

    [SerializeField]
    private float _slerpSpeed; // snelheid lerp rotatie

    [SerializeField]
    private float _tiltSpeed; // gevoeligheid/sensitiviteit van tilten

    // waardes direction van bewegende muis 
    private float _horizontalMouseSpeed;
    private float _verticalMouseSpeed;

    // hier tel ik waardes van direction muis op zodat het een rotatie kan worden
    private float _xRotation;
    private float _yRotation;

    // afstand camera van prefab (MOET NOG AANGEZETEN WORDEN VOOR ZOOMING)
    private float _currentDistanceFromTarget = 5;

    // hier sla ik de nieuwe rotatie waarde op
    private Quaternion _currentCameraRotation;

    [Header("Objects")]

    public ZoomManager ZoomManager; // pas rotatie aan gebaseerd op afstand product in zoom manager
    public Camera CameraToRotate;
    public Transform TransformToLookAt; // position waar de camera altijd naar kijkt (kan niet de camera zelf zijn door tilten)
    public GameObject Prefab;


    // ik zet hier de begin waardes goed
    void Start()
    {

        _yRotation = CameraToRotate.transform.eulerAngles.y;
        _xRotation = CameraToRotate.transform.eulerAngles.x;

        _currentCameraRotation = CameraToRotate.transform.rotation;

        ZoomManager.CameraToRotate = CameraToRotate;
    }


    void Update()
    {
        switch(_rotationState)
        {
            case RotationState.NotActive:

                print("not active");
                // kijk of user wil rotaten (als de linker muis knop is ingedrukt en er een positie is om naar te kijken)
                if (Input.GetMouseButtonDown(0) && TransformToLookAt != null)
                    _rotationState = RotationState.Rotating;

                // kijk of de user wil tilten (als de rechter muis knop is ingedrukt)
                else if (Input.GetMouseButtonDown(1))
                    _rotationState = RotationState.Tilting;

                break;


            case RotationState.Rotating:

                print("rotating");
                CalculateMouseSpeed(_rotationSpeed); // calculeer muis snelheid

                // sla nieuwe rotatie waardes op gebaseerd op muis bewegingen
                _xRotation += _horizontalMouseSpeed;
                _yRotation -= _verticalMouseSpeed;

                // limiteer y rotatie om clipping te voorkomen
                _yRotation = Mathf.Clamp(_yRotation, -89, 89); 

                // bereken hier de rotatie waar de user heen wilt met muis
                Quaternion targetRot = Quaternion.Euler(_yRotation, _xRotation, 0);

                // de formule voor het berekenen van een smooth lerp naar nieuwe rotatie
                _currentCameraRotation = Quaternion.Slerp(_currentCameraRotation, targetRot, Time.deltaTime * _slerpSpeed);

                // zet afstand van object in Vector3 formaat
                Vector3 direction = new Vector3(0, 0, -_currentDistanceFromTarget);


                RotateCamera(direction); // roteer camera



                // als button wordt losgelaten, ga naar niet active
                if (Input.GetMouseButtonUp(0))
                    _rotationState = RotationState.NotActive;

                break;


            case RotationState.Tilting:

                print("tilting");
                CalculateMouseSpeed(-_tiltSpeed); // calculeer muis snelheid

                // de nieuwe positie offset gebaseerd op muis input
                Vector3 newPosition = new Vector3(_horizontalMouseSpeed, _verticalMouseSpeed, 0);

                // bereken de nieuwe LOCALE positie offset gebaseerd op de huidige rotatie van de camera
                Vector3 newLocalPosition = CameraToRotate.transform.rotation * newPosition;

                TiltCamera(newLocalPosition);

                // als button wordt losgelaten, ga naar niet active
                if (Input.GetMouseButtonUp(1))
                    _rotationState = RotationState.NotActive;

                break;
        }
        
    }

    // functie om de camera te roteren
    void RotateCamera(Vector3 direction)
    {
        CameraToRotate.transform.position = TransformToLookAt.position + _currentCameraRotation * direction; // bereken rotatie
        CameraToRotate.transform.LookAt(TransformToLookAt.position); // camera kijkt nogsteeds naar midden punt
    }

    // functie om de camera te tilten
    void TiltCamera(Vector3 newLocalPosition)
    {
        // voeg muis input toe aan huidige positie
        CameraToRotate.transform.position += newLocalPosition;

        // beweeg transform mee waar camera naar kijkt zodat het draaipunt meebeweegt
        TransformToLookAt.transform.position += newLocalPosition;
    }

    // calculeer hier de snelheid van de muis van de user
    void CalculateMouseSpeed(float moveSpeed)
    {
        // bereken muis snelheden
        _horizontalMouseSpeed = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
        _verticalMouseSpeed = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;
    }
}
