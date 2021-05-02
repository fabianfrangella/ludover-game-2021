using System;
using UnityEngine;

public class PlayerExperienceManager : MonoBehaviour
{
    public int maxLevel;

    /**
     * <summary>
     * Public event to handle actions on level up
     * </summary>
     */
    public event Action OnLevelUp;
    public ExperienceBar experienceBar;

    public int level;

    private float currentExperience;
    private float nextLevelExperience;

    void Start()
    {
        level = 1;
        currentExperience = 0;
        nextLevelExperience = 100;
        experienceBar.SetLevelBar(level);
        experienceBar.SetExperience(currentExperience);
        experienceBar.SetNextLevelExperience(nextLevelExperience);
    }
    public int GetLevel()
    {
        return level;
    }

    public void GainExperience(float experience)
    {
        currentExperience += experience;
        experienceBar.SetExperience(currentExperience);
        if (currentExperience >= nextLevelExperience)
        {
            LevelUp();
            SetNextLevelExperience();
        }
    }
    private void LevelUp()
    {
        OnLevelUp();
        if (level + 1 >= maxLevel)
        {
            level = maxLevel;
            experienceBar.SetLevelBar(level);
            return;
        }
        level+= 1;
        experienceBar.SetLevelBar(level);
    }

    private void SetNextLevelExperience()
    {
        var exp = nextLevelExperience * (2 + (level / 2));
        nextLevelExperience = exp;
        experienceBar.SetNextLevelExperience(exp);
    }
}
