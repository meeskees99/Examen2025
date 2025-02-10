using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    /*All materials are assigned by int values, the values are as follows:
     * 0 = Edge material (per object)
     * 1 = Lit material (per object)
     * 2 = Unlit material (per object)
     * 3 = Clay material (per object)
     * 4 = Dissolve shader (per object)
     The order in the editor in the MaterialHolder script need to be in this order always!*/
     
    [Header("Materials")]
    [SerializeField] private Material[] assignedMaterials;

    //readyToDeactivate is used to detect wether or not object is ready to be deactivated.
    [HideInInspector] public bool readyToDeactivate = false;
    [HideInInspector] public bool readyToActivate = false;
    [SerializeField] private CanvasManager canvasManager;

    // Update is used for checking if the dissolveshader is done with dissolving by asking and then setting a boolean (readyToDeactivate) to say if it can be deactivated by another script called canvasManager.
    void Update()
    {
        if(canvasManager.GameObjectToShow[canvasManager.currentModelNumber].GetComponent<DissolveShaderScript>().dissolveslider > 0.95f && canvasManager.GameObjectToShow[canvasManager.currentModelNumber].GetComponent<DissolveShaderScript>().done == true)
        {
            MaterialChange(1);
            readyToActivate = true;
        }
        else if(readyToDeactivate == true && canvasManager.GameObjectToShow[canvasManager.currentModelNumber].GetComponent<DissolveShaderScript>().done == false)
        {

            readyToDeactivate = false;
        }

        if(canvasManager.GameObjectToShow[canvasManager.currentModelNumber].GetComponent<DissolveShaderScript>().dissolveslider < -0.95f && canvasManager.GameObjectToShow[canvasManager.currentModelNumber].GetComponent<DissolveShaderScript>().done == true)
        {
            MaterialChange(1);
            readyToDeactivate = true;
        }
    }

    /*This function applies the materials assigned in inspector, this is done per UI button.
    The only material that uses 2 different materials at the same time is the edge view since it deforms the mesh in the 2nd material slot, and introduces backface culling to achieve its effect.
    This is why there is an if/else statement.*/
    public void MaterialChange(int materialNumber)
    {
        for(int i = 0; i < assignedMaterials.Length; i++)
        {
            assignedMaterials[i] = canvasManager.GameObjectToShow[canvasManager.currentModelNumber].GetComponent<MaterialHolder>().heldMaterials[i];
        }

        Material[] materials = new Material[canvasManager.GameObjectToShow[canvasManager.currentModelNumber].GetComponent<Renderer>().sharedMaterials.Length];
        if (materialNumber == 0f)
        {
            materials[0] = assignedMaterials[1];
            materials[1] = assignedMaterials[materialNumber];
        }
        else
        {
            materials[0] = assignedMaterials[materialNumber];
            materials[1] = assignedMaterials[materialNumber];
        }
        canvasManager.GameObjectToShow[canvasManager.currentModelNumber].GetComponent<MeshRenderer>().sharedMaterials = materials;
    }
}
