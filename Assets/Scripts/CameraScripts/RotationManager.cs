using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;

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

    // afstand camera van prefab (WORDT BESTUURD DOOR ZOOM MANAGER)
    [HideInInspector]
    public float CurrentDistanceFromTarget;

    // hier sla ik de nieuwe rotatie waarde op
    [HideInInspector]
    public Quaternion CurrentCameraRotation;

    [SerializeField]
    private float _maxDistanceFromTarget;

    // nieuwe input system
    [SerializeField]
    private PlayerInput playerInput;
    private InputAction rotateAction;
    private InputAction tiltAction;


    [Header("Objects")]

    public Camera CameraToRotate;
    public Transform TransformToLookAt; // position waar de camera altijd naar kijkt (kan niet de camera zelf zijn door tilten)
    public GameObject Prefab;

    private void Awake()
    {
        // Assign actions from input asset
        rotateAction = playerInput.actions.FindActionMap("MouseControls").FindAction("Rotation");
        tiltAction = playerInput.actions.FindActionMap("MouseControls").FindAction("Tilting");
    }

    void OnEnable()
    {
        rotateAction.Enable();
        rotateAction.started += OnRotationStart;  
        rotateAction.canceled += OnRotationEnd;  

        tiltAction.Enable();
        tiltAction.started += OnTiltingStart;  
        tiltAction.canceled += OnTiltingEnd; 
    }

    void OnDisable()
    {
        rotateAction.started -= OnRotationStart;
        rotateAction.canceled -= OnRotationEnd;
        rotateAction.Disable();

        tiltAction.started -= OnTiltingStart;
        tiltAction.canceled -= OnTiltingEnd;
        tiltAction.Disable();
    }

    // wanneer user wil roteren
    private void OnRotationStart(InputAction.CallbackContext context)
    {
        // verander state
        if (_rotationState == RotationState.NotActive)
        {
            _rotationState = RotationState.Rotating;
            Debug.Log("Rotation Started");
        }
    }

    // wanneer user stopt met roteren
    private void OnRotationEnd(InputAction.CallbackContext context)
    {
        // verander state
        if (_rotationState == RotationState.Rotating)
        {
            _rotationState = RotationState.NotActive;
            Debug.Log("Rotation Ended");
        }
    }

    // wanneer user wil tilten
    private void OnTiltingStart(InputAction.CallbackContext context)
    {
        // Change state to tilting
        _rotationState = RotationState.Tilting;
        Debug.Log("Tilting Started");
    }

    // wanneer user stopt met tilten
    private void OnTiltingEnd(InputAction.CallbackContext context)
    {
        // Change state to not active
        if (_rotationState == RotationState.Tilting)
        {
            _rotationState = RotationState.NotActive;
            Debug.Log("Tilting Ended");
        }
    }


    // ik zet hier de begin waardes goed
    void Start()
    {

        _yRotation = CameraToRotate.transform.eulerAngles.y;
        _xRotation = CameraToRotate.transform.eulerAngles.x;

        CurrentCameraRotation = CameraToRotate.transform.rotation;
    }


    void Update()
    {
        switch(_rotationState)
        {
            case RotationState.NotActive:

                print("not active");
                // kijk of user wil rotaten (als de linker muis knop is ingedrukt en er een positie is om naar te kijken)
                if (rotateAction.triggered && TransformToLookAt != null)
                    _rotationState = RotationState.Rotating;

                // kijk of de user wil tilten (als de rechter muis knop is ingedrukt)
                else if (tiltAction.triggered)
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
                CurrentCameraRotation = Quaternion.Slerp(CurrentCameraRotation, targetRot, Time.deltaTime * _slerpSpeed);

                // zet afstand van object in Vector3 formaat
                Vector3 direction = new Vector3(0, 0, -CurrentDistanceFromTarget);


                RotateCamera(direction); // roteer camera

                break;


            case RotationState.Tilting:

                print("tilting");
                CalculateMouseSpeed(-_tiltSpeed); // calculeer muis snelheid

                // de nieuwe positie offset gebaseerd op muis input
                Vector3 newPosition = new Vector3(_horizontalMouseSpeed, _verticalMouseSpeed, 0);

                // bereken de nieuwe LOCALE positie offset gebaseerd op de huidige rotatie van de camera
                Vector3 newLocalPosition = CameraToRotate.transform.rotation * newPosition;

                TiltCamera(newLocalPosition);

                break;
        }
        
    }

    // functie om de camera te roteren
    void RotateCamera(Vector3 direction)
    {
        // bereken nieuwe rotatie
        Vector3 potentialNewRotation = TransformToLookAt.position + CurrentCameraRotation * direction; // bereken rotatie

        // kijk wat de nieuwe afstand zou worden
        float newDistance = Vector3.Distance(Prefab.transform.position, potentialNewRotation);

        // kijk of camera mag bewegen
        if (newDistance <= _maxDistanceFromTarget)
        {
            CameraToRotate.transform.position = TransformToLookAt.position + CurrentCameraRotation * direction; // bereken rotatie
            CameraToRotate.transform.LookAt(TransformToLookAt.position); // camera kijkt nogsteeds naar midden punt
        }
        
    }

    // functie om de camera te tilten
    void TiltCamera(Vector3 newLocalPosition)
    {
        // bereken nieuwe positie
        Vector3 potentialNewPosition = CameraToRotate.transform.position + newLocalPosition;

        // kijk wat de nieuwe afstand zou worden
        float newDistance = Vector3.Distance(Prefab.transform.position, potentialNewPosition);

        // kijk of camera mag bewegen
        if (newDistance <= _maxDistanceFromTarget)
        {
            CameraToRotate.transform.position = potentialNewPosition;
            TransformToLookAt.transform.position += newLocalPosition;
        }

    }

    // calculeer hier de snelheid van de muis van de user
    void CalculateMouseSpeed(float moveSpeed)
    {
        // bereken muis snelheden
        _horizontalMouseSpeed = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
        _verticalMouseSpeed = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;
    }

    // reset values from ResetCamera script
    public void ResetValues()
    {
        _xRotation = 0;
        _yRotation = 0;
    }
}
