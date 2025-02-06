using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveShaderScript : MonoBehaviour
{

    public Material dissolveMaterial;
    public float dissolveslider;
    public float speed;
    public bool on;

    // Start is called before the first frame update
    void Start()
    {
        dissolveMaterial.SetFloat("_Time_Control", dissolveslider);

    }

    // Update is called once per frame
    void Update()
    {
        dissolveslider = Mathf.Clamp(dissolveslider, -1f, 1f);

        if (on == true && dissolveMaterial.GetFloat("_Time_Control") < 1f)
        {
            dissolveslider += speed * Time.fixedDeltaTime;
        }
        else if(dissolveMaterial.GetFloat("_Time_Control") > -1f)
        {
            dissolveslider -= speed * Time.fixedDeltaTime;
        }

        dissolveMaterial.SetFloat("_Time_Control", dissolveslider);
    }
}
