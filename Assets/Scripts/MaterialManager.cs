using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public Material originalMaterial;
    public Material edgeViewMaterial;
    public Material clayViewMaterial;
    public bool edgeViewButtonTemporary;
    public bool clayViewButtonTemporary;
    public GameObject currentObject;
    public Material[] materialsArray;
    public MeshRenderer currentMesh;

    public void Start()
    {
        edgeViewButtonTemporary = false;
        clayViewButtonTemporary = false;
    }
    public void Update()
    {
        //This part controls wether or not the edge view shader is active or not.
        if (edgeViewButtonTemporary)
        {
            currentObject = GameObject.FindGameObjectWithTag("Model");
            currentMesh = currentObject.GetComponent<MeshRenderer>();
            originalMaterial = currentMesh.material;
            Material[] materials = new Material[currentMesh.sharedMaterials.Length];
            materials[0] = originalMaterial;
            materials[1] = edgeViewMaterial;
            materialsArray = materials;
            currentObject.GetComponent<MeshRenderer>().sharedMaterials = materials;
        }
        else if (!edgeViewButtonTemporary && materialsArray[1] == edgeViewMaterial)
        {
            currentObject = GameObject.FindGameObjectWithTag("Model");
            currentMesh = currentObject.GetComponent<MeshRenderer>();
            originalMaterial = currentMesh.material;
            Material[] materials = new Material[currentMesh.sharedMaterials.Length];
            materials[0] = originalMaterial;
            materials[1] = originalMaterial;
            materialsArray = materials;
            currentObject.GetComponent<MeshRenderer>().sharedMaterials = materials;
        }

        //This part controls wether or not the clay view shader is active or not.
    }
}
