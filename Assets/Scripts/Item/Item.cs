using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item<T> : MonoBehaviour, ItemInterface
{
    public ItemType itemType;
    public T owner;
    public abstract void Use();

    public void SetOwner(T newOwner)
    {
        owner = newOwner;
    }

    new public ItemType GetType()
    {
        return itemType;
    }
}

public enum ItemType
{
    HEALTHPOTION,
    MANAPOTION,
    STAMINAPOTION
}

public interface ItemInterface
{ 
    void Use();
    ItemType GetType();
}