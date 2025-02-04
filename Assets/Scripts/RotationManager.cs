using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float _tiltSpeed;

    private float _currentMouseSpeed;
    private Vector3 _currentMousePosition;

    [Header("Objects")]
    public Camera CameraToRotate;
    public Transform TransformToLookAt; // position waar de camera altijd naar kijkt
    public GameObject Prefab;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // functie om de camera te roteren
    void RotateCamera()
    {

    }

    // functie om de camera te tilten
    void TiltCamera()
    {

    }

    // calculeer hier de snelheid van de muis van de user
    void CalculateMouseSpeed(Vector3 mousePosition)
    {

    }
}
