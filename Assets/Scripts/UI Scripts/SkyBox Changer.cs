using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkyBoxChanger : MonoBehaviour
{
public Material DayShader;
public Material NightShader;

public Toggle Day;

    public void Skybox()
    {
        if (Day.isOn) RenderSettings.skybox = DayShader;
        else RenderSettings.skybox = NightShader;
    }
}
