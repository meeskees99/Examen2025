using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveShaderScript : MonoBehaviour
{

    public Material dissolveMaterial;
    public float dissolveslider;
    public float speed = 0.2f;
    public bool on;
    public bool done;
    public MaterialManager materialManager;

    // Start is called before the first frame update
    void Start()
    {
        dissolveMaterial.SetFloat("_Time_Control", dissolveslider);
        //dissolveslider = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (done == false)
        {

            //de animatie over time

            dissolveslider = Mathf.Clamp(dissolveslider, -1f, 1f);

            if (on == true && dissolveMaterial.GetFloat("_Time_Control") < 1f)
            {
                dissolveslider += speed * Time.deltaTime;
            }
            else if (dissolveMaterial.GetFloat("_Time_Control") > -1f)
            {
                dissolveslider -= speed * Time.deltaTime;
            }

            dissolveMaterial.SetFloat("_Time_Control", dissolveslider);
        }

        //checkt of de animatie in moet faden en of hij het eindpunt heeft bereikt en zet de dissolvematerial uit via de materialmanager..

        if (on == true && dissolveslider > 0.95f && done == false)
        {
            done = true;
            on = false;
        }

        //checkt of de animatie uit moet faden en of hij het eindpunt heeft bereikt en zet de dissolvematerial uit via de materialmanager.

        if (on == false && dissolveslider < -0.95f && done == false)
        {
            materialManager.DeactivateDissolveMaterial();
            done = true;
            on = true;
        }

    }

    //de functies die aangeroepen kunnen worden door andere UI buttons en die in Materialmanager de dissolvematerial aanzetten.
    public void DissolveOn()
    {
        dissolveslider = -1f;
        materialManager.ActivateDissolveMaterial();
        on = true;
        done = false;
    }

    public void DissolveOff()
    {
        dissolveslider = 1f;
        materialManager.ActivateDissolveMaterial();
        on = false;
        done = false;
    }
}