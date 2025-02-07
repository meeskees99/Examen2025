using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    //All materials are assigned for each object here. This can only be done in the editor.
    [Header("Materials")]
    [SerializeField] private Material dissolveMaterial;
    [SerializeField] private Material materialLit;
    [SerializeField] private Material materialUnlit;
    [SerializeField] private Material edgeViewMaterial;
    [SerializeField] private Material clayViewMaterial;

    //readyToDeactivate is used to detect wether or not object is ready to be deactivated.
    [HideInInspector] public bool readyToDeactivate = false;

    //These variables hold the information for the material desired to be changed to.
    private Material changerMaterialZero;
    private Material changerMaterialOne;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<DissolveShaderScript>().on == true && GetComponent<DissolveShaderScript>().dissolveslider < -0.95f)
        {
            LitViewActivate();
            readyToDeactivate = false;
        }else if (GetComponent<DissolveShaderScript>().dissolveslider > 0.95f)
        {
            LitViewActivate();
            readyToDeactivate = true;
        }
    }

    //This function applies the materials assigned in all seperate material functions.
    public void ApplyMaterials()
    {
        Material[] materials = new Material[GetComponent<Renderer>().sharedMaterials.Length];
        materials[0] = changerMaterialZero;
        materials[1] = changerMaterialOne;
        GetComponent<MeshRenderer>().sharedMaterials = materials;
    }

    //Activates the dissolve effect for fading an object in/out.
    public void DissolveActivate()
    {
        changerMaterialZero = dissolveMaterial;
        changerMaterialOne = dissolveMaterial;
        ApplyMaterials();
    }

    //Activates lit view.
    public void LitViewActivate()
    {
        changerMaterialZero = materialLit;
        changerMaterialOne = materialLit;
        ApplyMaterials();
    }

    //Activates unlit view.
    public void UnlitViewActivate()
    {
        changerMaterialZero = materialUnlit;
        changerMaterialOne = materialUnlit;
        ApplyMaterials();
    }

    //Activates edge view.
    public void EdgeViewActivate()
    {
        changerMaterialZero = materialLit;
        changerMaterialOne = edgeViewMaterial;
        ApplyMaterials();
    }

    //Activates clay view.
    public void ClayViewActivate()
    {
        changerMaterialZero = clayViewMaterial;
        changerMaterialOne = clayViewMaterial;
        ApplyMaterials();
    }
}
