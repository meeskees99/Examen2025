using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timeout : MonoBehaviour

{
    [Header("Idle Settings")]
    [Tooltip("Time in seconds with no mouse movement before the main menu is shown.")]
    public float idleTimeThreshold = 10f;

    [Header("Main Menu Canvas")]
    [Tooltip("The Main Menu canvas to activate when idle.")]
    public Canvas ModelListCanvas;
    public Canvas ModelViewCanvas;

    // Track the last time the mouse moved and its position.
    private float lastMovementTime;
    private Vector3 lastMousePosition;

    void Start()
    {
        // Record the initial mouse position and time.
        lastMovementTime = Time.time;
        lastMousePosition = Input.mousePosition;

        // Ensure the main menu canvas is hidden at the start.
        if (ModelListCanvas != null)
        {
            ModelListCanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Check if the mouse has moved.
        if (Input.mousePosition != lastMousePosition)
        {
            lastMovementTime = Time.time;
            lastMousePosition = Input.mousePosition;
        }

        // Als de muis lang genoeg stil staat dan laad hij de ModelViewCanvas
        if (Time.time - lastMovementTime >= idleTimeThreshold)
        {
            if (ModelListCanvas != null && !ModelListCanvas.gameObject.activeSelf)
            {
                ModelListCanvas.gameObject.SetActive(true);
                ModelViewCanvas.gameObject.SetActive(false);

            }
        }
    }
}
