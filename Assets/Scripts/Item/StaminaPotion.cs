using UnityEngine;

public class StaminaPotion : Item
{
    public StaminaPotion()
    {
        name = ItemEnum.StaminaPotion;
    }

    public override void Use(Transform transform)
    {
        var manager = transform.GetComponent<PlayerStaminaManager>();
        manager.OnStaminaReceived(50);
    }
}