using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider Slider;

    public void SetMaxProgress(int maxValue)
    {
        Slider.maxValue = maxValue;
    }

    public void SetCurrentProgress(int value)
    {
        Slider.value = value;
    }
}
