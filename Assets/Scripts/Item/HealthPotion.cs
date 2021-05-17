using UnityEngine;

public class HealthPotion : Item
{
    public HealthPotion()
    {
        name = ItemEnum.HealthPotion;
    }

    public override void Use(Transform transform)
    {
        var manager = transform.GetComponent<PlayerHealthManager>();
        manager.OnHealing(50);
    }
}
