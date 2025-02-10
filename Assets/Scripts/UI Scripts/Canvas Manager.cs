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

    [SerializeField] private MaterialManager materialManager;


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
        RenderSettings.skybox = ModelViewSkybox;
        GameObjectToShow[objectNumber].SetActive(true);
        
        if (materialManager.readyToActivate == true)
        {
            GameObjectToShow[currentModelNumber].GetComponent<DissolveShaderScript>().on = true;
            GameObjectToShow[currentModelNumber].GetComponent<DissolveShaderScript>().done = false;
        }
        //else
        //{

        //}
         
    }
        public void Back()
    {
        if (materialManager.readyToDeactivate == true)
        {
            GameObjectToShow[currentModelNumber].SetActive(false);
            if (AssetList != null) AssetList.gameObject.SetActive(true);
            if (ModelView != null) ModelView.gameObject.SetActive(false);
            RenderSettings.skybox = ModelListSkybox;
        }
        else
        {
            GameObjectToShow[currentModelNumber].GetComponent<DissolveShaderScript>().on = false;
            GameObjectToShow[currentModelNumber].GetComponent<DissolveShaderScript>().done = false;
        }

    }
}
