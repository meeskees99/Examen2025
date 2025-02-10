using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public GameObject[] GameObjectToShow;
    public Canvas AssetList;
    public Canvas ModelView;
    public Material ModelListSkybox;
    public Material ModelViewSkybox;
    public int currentModelNumber;


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
        currentModelNumber = objectNumber;
        if (AssetList != null) AssetList.gameObject.SetActive(false);
        if (ModelView != null) ModelView.gameObject.SetActive(true);
        GameObjectToShow[objectNumber].SetActive(true);
        RenderSettings.skybox = ModelViewSkybox;
         
    }
        public void Back()
    {
        if (AssetList != null) AssetList.gameObject.SetActive(true);
        if (ModelView != null) ModelView.gameObject.SetActive(false);
        GameObjectToShow[currentModelNumber].SetActive(false);
        RenderSettings.skybox = ModelListSkybox;

    }
}
