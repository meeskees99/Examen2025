using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timeout : MonoBehaviour

{

    public float TimeoutTime = 10f;

    public Canvas ModelListCanvas;
    public Canvas ModelViewCanvas;

    public Material ModelListSkybox;

    public float lastMovementTime;
    private Vector3 lastMousePosition;

    void Start()
    {
        // checked of de model list canvas uit staat en de model view canvas aan staat
        if (ModelListCanvas != null)
        {
            ModelListCanvas.gameObject.SetActive(false);
        }
        if  (ModelViewCanvas != null)
        {
            ModelViewCanvas.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        // Kijkt of de muis heeft bewogen
        if (Input.mousePosition != lastMousePosition)
        {
            lastMovementTime = 0;
            
        }

        lastMovementTime += Time.deltaTime;
        lastMousePosition = Input.mousePosition;

        // Als de muis lang genoeg stil staat dan laad hij de ModelViewCanvas
        if (lastMovementTime >= TimeoutTime)
        {
            if (ModelListCanvas != null && !ModelListCanvas.gameObject.activeSelf)
            {
                ModelListCanvas.gameObject.SetActive(true);
                ModelViewCanvas.gameObject.SetActive(false);
                RenderSettings.skybox = ModelListSkybox;

            }
        }
    }
}
