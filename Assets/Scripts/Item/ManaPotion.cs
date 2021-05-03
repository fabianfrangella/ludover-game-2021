using UnityEngine;

public class ManaPotion : Item
{
    public ManaPotion()
    {
        name = ItemEnum.ManaPotion;
    }

    override public void Use(Transform transform)
    {
        var manager = transform.GetComponent<PlayerManaManager>();
        manager.OnManaReceived(50);
    }
}