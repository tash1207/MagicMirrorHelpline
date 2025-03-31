using UnityEngine;

public class Slippers : Interactable
{
    void Awake()
    {
        objectName = "Enchanted slippers";
        canPickUp = true;
    }

    override public void Interact()
    {
        base.Interact();
        string[] thoughts = new string[] {
            "Why Samantha keeps enchanted glass slippers on her desk, I have no idea.",
            "I could always try them on, they're supposed to fit anyone..."
        };
        Think(thoughts);
    }

    override public bool PickUp()
    {
        if (base.PickUp())
        {
            Inventory.Instance.AddObject(Inventory.InventoryObject.Slippers, this);
            return true;
        }
        return false;
    }
}
