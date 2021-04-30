using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI level;

    public void SetLevelBar(int level)
    {
        this.level.text = "Level " + level.ToString();
    }
    public void SetExperience(float experience)
    {
        slider.value = experience;
    }

    public void SetNextLevelExperience(float value)
    {
        slider.maxValue = value;
    }
}
