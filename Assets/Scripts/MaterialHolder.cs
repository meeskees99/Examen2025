using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHolder : MonoBehaviour
{
    /*All materials are assigned by int values, the values are as follows:
     * 0 = edge material (per object)
     * 1 = lit material (per object)
     * 2 = unlit material (per object)
     * 3 = clay material (per object)
     * 4 = dissolve shader (per object)
     the order in the editor in the MaterialHolder script need to be in this order always!*/
    public Material[] heldMaterials;
}
