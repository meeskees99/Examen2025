using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public Material originalMaterial, edgeViewMaterial;
    public bool edgeViewActivate;
    public GameObject[] currentObjects;
    public GameObject currentActiveObject;
    public Material[] materials;
    public MeshRenderer activeMesh;

    public void Start()
    {
        edgeViewActivate = false;
        currentObjects = GameObject.FindGameObjectsWithTag("Model");
    }
    public void Update()
    {
        if (edgeViewActivate)
        {
            EdgeViewActivator();
            edgeViewActivate = false;
        }
    }
    void EdgeViewActivator()
    {
        for (int i = 0; i < currentObjects.Length; i++)
        {
            if (currentObjects[i].activeInHierarchy)
            {
                currentActiveObject = currentObjects[i];
                activeMesh = currentActiveObject.GetComponent<MeshRenderer>();
                originalMaterial = activeMesh.material;
                Material[] materials = new Material[activeMesh.sharedMaterials.Length];
                materials[0] = originalMaterial;
                materials[1] = edgeViewMaterial;
                currentActiveObject.GetComponent<MeshRenderer>().sharedMaterials = materials;
            }
        }
    }
}
