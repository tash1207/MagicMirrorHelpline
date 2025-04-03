using UnityEngine;

public class RubberDuck : Interactable
{
    void Awake()
    {
        objectName = "Rubber duck";
        canPickUp = true;
    }

    override public void Interact()
    {
        base.Interact();
        string[] thoughts = new string[] {
            "It's my desk.",
            "And my lucky rubber ducky!"
        };
        Think(thoughts);
    }

    override public bool PickUp()
    {
        if (base.PickUp())
        {
            Inventory.Instance.AddObject(Inventory.InventoryObject.RubberDuck, this);
            return true;
        }
        return false;
    }
}
