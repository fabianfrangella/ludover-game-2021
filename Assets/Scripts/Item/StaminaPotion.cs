using UnityEngine;

public class StaminaPotion : Item
{
    public StaminaPotion()
    {
        name = ItemEnum.StaminaPotion;
    }

    override public void Use(Transform transform)
    {
        var manager = transform.GetComponent<PlayerStaminaManager>();
        manager.OnStaminaReceived(50);
    }
}