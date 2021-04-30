using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperienceManager : MonoBehaviour
{
    public int maxLevel;

    /**
     * <summary>
     * Public event to handle actions on level up
     * </summary>
     */
    public event System.Action OnLevelUp; 

    public int level;
    private float currentExperience;
    private float nextLevelExperience;

    void Start()
    {
        level = 1;
        currentExperience = 0;
        nextLevelExperience = 100;
    }

    public int GetLevel()
    {
        return level;
    }

    public void GainExperience(float experience)
    {
        currentExperience += experience;
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
            return;
        }

        level+= 1;
    }

    private void SetNextLevelExperience()
    {
        nextLevelExperience = nextLevelExperience * (2 + (level / 2));
    }
}
