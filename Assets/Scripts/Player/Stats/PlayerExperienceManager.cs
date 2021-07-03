using System;
using UnityEngine;

public class PlayerExperienceManager : MonoBehaviour
{
    /**
     * <summary>
     * Public event to handle actions on level up
     * </summary>
     */
    public event Action OnLevelUp;
    public ExperienceBar experienceBar;

    private void Start()
    {
        if (experienceBar != null)
        {
            experienceBar.SetLevelBar(PlayerStats.instance.level);
            experienceBar.SetExperience(PlayerStats.instance.currentExperience);
            experienceBar.SetNextLevelExperience(PlayerStats.instance.nextLevelExperience);
        }

        PlayerStats.instance.SetExperienceManager(this);
    }

    public void GainExperience(float experience)
    {
        PlayerStats.instance.currentExperience += experience;
        experienceBar.SetExperience(PlayerStats.instance.currentExperience);
        if (PlayerStats.instance.currentExperience >= PlayerStats.instance.nextLevelExperience)
        {
            LevelUp();
            SetNextLevelExperience();
        }
    }
    private void LevelUp()
    {
        OnLevelUp();
        if (PlayerStats.instance.level + 1 >= PlayerStats.instance.maxLevel)
        {
            PlayerStats.instance.level = PlayerStats.instance.maxLevel;
            experienceBar.SetLevelBar(PlayerStats.instance.level);
            return;
        }
        PlayerStats.instance.level+= 1;
        experienceBar.SetLevelBar(PlayerStats.instance.level);
    }

    private void SetNextLevelExperience()
    {
        var exp = PlayerStats.instance.nextLevelExperience * (2 + (PlayerStats.instance.level / 2));
        PlayerStats.instance.nextLevelExperience = exp;
        experienceBar.SetNextLevelExperience(exp);
    }
}
