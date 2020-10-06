using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void SetHealth(float healthRatio)
    {
        slider.value = healthRatio;
    }
}
