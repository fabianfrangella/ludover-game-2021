using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;

    public void SetMana(float mana)
    {
        slider.value = mana;
    }

    public void SetMaxMana(float value)
    {
        slider.maxValue = value;
    }
}
