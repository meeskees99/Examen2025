using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorToggleHelper : MonoBehaviour
{
    public Animator targetAnimator;
    public Toggle  AnimatorToggle;


    private void Update()
    {

        //verbind de value van de animator (Open/Close) en de toggle bool value.
       targetAnimator.SetBool("Open/Close", AnimatorToggle.isOn);
    }
}

