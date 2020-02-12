using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorArrowToggler : MonoBehaviour
{
    public string Button;

    void Update ()
    {
        if (Input.GetButtonDown(Button))
        {
            IndicatorArrow.ShowArrows = !IndicatorArrow.ShowArrows;
        } 
    }
}
