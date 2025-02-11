using JetBrains.Annotations;
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

        //Activates the dissolve material through MaterialManager and toggles dissolve in a direction of "loading in";
        materialManager.ActivateDissolveMaterial();
        GameObjectToShow[currentModelNumber].GetComponent<DissolveShaderScript>().DissolveOn();
    }
    //Activates the ActivateDissolveMaterial function in the MaterialManager, and communicates with the DissolveShaderScript and toggles the dissolve in a direction of "loading out";
    public void Back()
    {
        materialManager.ActivateDissolveMaterial();
        GameObjectToShow[currentModelNumber].GetComponent<DissolveShaderScript>().DissolveOff();
    }
    //Deactivates the object after this function is called by MaterialManager and set the material to standard Lit material again.
    public void DeActivateObject()
    {
        GameObjectToShow[currentModelNumber].SetActive(false);
        if (AssetList != null) AssetList.gameObject.SetActive(true);
        if (ModelView != null) ModelView.gameObject.SetActive(false);
        RenderSettings.skybox = ModelListSkybox;
    }
}