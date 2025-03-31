using UnityEngine;

public class Axe : Interactable
{
    [SerializeField] GameObject brokenCase;
    [SerializeField] AudioClip brokenGlassSound;

    void Awake()
    {
        objectName = "Axe";
        canPickUp = true;
    }

    override public void Interact()
    {
        base.Interact();
        string thought = "Does hunger count as enough of an emergency?";
        if (Inventory.Instance.inventoryEnabled)
        {
            string[] thoughts = new string[] {
                thought,
                "I certainly think so."
            };
            Think(thoughts);
        }
        else
        {
            Think(thought);
        }
    }

    override public bool PickUp()
    {
        if (base.PickUp())
        {
            SoundFXManager.Instance.PlaySoundFXClip(brokenGlassSound, 1f);
            brokenCase.SetActive(true);
            Inventory.Instance.AddObject(Inventory.InventoryObject.Axe, this);
            return true;
        }
        return false;
    }
}
