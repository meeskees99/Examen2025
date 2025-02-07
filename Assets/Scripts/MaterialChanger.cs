using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [Header("Activators")]
    public bool dissolveActivate;
    public bool unlitViewActivate;
    public bool litViewActivate;
    public bool edgeViewActivate;
    public bool clayViewActivate;

    [Header("Materials")]
    [SerializeField] private Material dissolveMaterial;
    [SerializeField] private Material materialLit;
    [SerializeField] private Material materialUnlit;
    [SerializeField] private Material edgeViewMaterial;
    [SerializeField] private Material clayViewMaterial;

    private Material changerMaterialZero;
    private Material changerMaterialOne;

    // Update is called once per frame
    void Update()
    {
        // first 5 if statements are to be deleted when replaced with buttons!

        if (dissolveActivate)
        {
            DissolveActivate();
            ButtonClick();
            dissolveActivate = false;
        }

        if (litViewActivate)
        {
            LitViewActivate();
            ButtonClick();
            litViewActivate = false;
        }

        if (unlitViewActivate)
        {
            UnlitViewActivate();
            ButtonClick();
            unlitViewActivate = false;
        }

        if (edgeViewActivate)
        {
            EdgeViewActivate();
            ButtonClick();
            edgeViewActivate = false;
        }

        if (clayViewActivate)
        {
            ClayViewActivate();
            ButtonClick();
            clayViewActivate = false;
        }
    }

    //This function applies the materials assigned in all seperate material functions.
    public void ButtonClick()
    {
        Material[] materials = new Material[GetComponent<Renderer>().sharedMaterials.Length];
        materials[0] = changerMaterialZero;
        materials[1] = changerMaterialOne;
        GetComponent<MeshRenderer>().sharedMaterials = materials;
    }
    //Following functions activate view modes by setting the "materials to change" before setting buttonclick to true. 
    //("Materials to change" refers to the materials called changerMaterialZero and changerMaterialOne)

    //Activates the dissolve effect.
    public void DissolveActivate()
    {
        changerMaterialZero = dissolveMaterial;
        changerMaterialOne = dissolveMaterial;
    }

    //Activates lit view.
    public void LitViewActivate()
    {
        changerMaterialZero = materialLit;
        changerMaterialOne = materialLit;
    }

    //Activates unlit view.
    public void UnlitViewActivate()
    {
        changerMaterialZero = materialUnlit;
        changerMaterialOne = materialUnlit;
    }

    //Activates edge view.
    public void EdgeViewActivate()
    {
        changerMaterialZero = materialLit;
        changerMaterialOne = edgeViewMaterial;
    }

    //Activates clay view.
    public void ClayViewActivate()
    {
            changerMaterialZero = clayViewMaterial;
            changerMaterialOne = clayViewMaterial;
    }
}
