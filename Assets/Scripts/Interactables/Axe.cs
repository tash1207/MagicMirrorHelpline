using UnityEngine;

public class Axe : Interactable
{
    [SerializeField] GameObject brokenCase;
    void Awake()
    {
        objectName = "Axe";
        canPickUp = true;
    }

    override public void Interact()
    {
        base.Interact();
        string[] thoughts = new string[] {
            "Does hunger count as enough of an emergency?",
        };
        Think(thoughts);
    }

    override public bool PickUp()
    {
        if (base.PickUp())
        {
            brokenCase.SetActive(true);
            Inventory.Instance.AddObject(Inventory.InventoryObject.Axe, this);
            return true;
        }
        return false;
    }
}
