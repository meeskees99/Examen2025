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
        if(on == true && dissolveslider > 0.95f || on == false && dissolveslider < -0.95f)     //
        {
            done = true;
        }
        
    }
    public void DissolveOn()
    {
        dissolveslider = -1f;
        on = true;
    }
    public void DissolveOff()
    {
        dissolveslider = 1f;
        on = false;
    }
}
