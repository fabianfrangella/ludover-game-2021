using UnityEngine;
public abstract class Item
{
    public ItemEnum name;
    public abstract void Use(Transform transform);

    public ItemEnum GetName()
    {
        return name;
    }
}

public enum ItemEnum
{
    HealthPotion,
    ManaPotion,
    StaminaPotion
}