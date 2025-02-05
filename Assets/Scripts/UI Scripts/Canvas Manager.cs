using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public GameObject[] GameObjectToShow;
    public Canvas CanvasToHide;
    public Canvas CanvasToShow;
    public Material Skybox;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ButtonClick(int objectNumber)
    {
        if (CanvasToHide != null) CanvasToHide.gameObject.SetActive(false);
        if (CanvasToShow != null) CanvasToShow.gameObject.SetActive(true);
        GameObjectToShow[objectNumber].SetActive(true);
        RenderSettings.skybox = Skybox;

    }
}
