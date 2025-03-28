using UnityEngine;

public class Paper : Interactable
{
    void Awake()
    {
        objectName = "Tom's soup recipe";
        canPickUp = true;
    }

    override public void Interact()
    {
        base.Interact();
        Debug.Log("Tom's famous french onion soup recipe.");
    }

    override public void PickUp()
    {
        base.PickUp();
    }
}
