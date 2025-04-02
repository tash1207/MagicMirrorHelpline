using UnityEngine;

public class Axe : Interactable
{
    [SerializeField] GameObject brokenCase;
    [SerializeField] Sprite axeInBrokenCase;
    [SerializeField] AudioClip brokenGlassSound;

    bool hasBrokenGlass = false;

    void Awake()
    {
        objectName = "Axe";
        canPickUp = true;
        dontDestroyOnPickup = true;
    }

    override public void Interact()
    {
        base.Interact();
        string thought = "Does hunger count as enough of an emergency?";
        if (Inventory.Instance.inventoryEnabled)
        {
            if (hasBrokenGlass)
            {
                Think("On second thought, maybe I will need this.");
            }
            else
            {
                string[] thoughts = new string[] {
                    thought,
                    "I certainly think so."
                };
                Think(thoughts);
            }
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
            if (!hasBrokenGlass)
            {
                SoundFXManager.Instance.PlaySoundFXClip(brokenGlassSound, 1f);
                brokenCase.SetActive(true);
                hasBrokenGlass = true;
                GetComponent<SpriteRenderer>().sprite = axeInBrokenCase;
            }
            Inventory.Instance.AddObject(Inventory.InventoryObject.Axe, this);
            return true;
        }
        return false;
    }
}
