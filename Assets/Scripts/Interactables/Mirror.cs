using UnityEngine;

public class Mirror : Interactable
{
    override public void Interact()
    {
        base.Interact();
        Debug.Log("Mirror mirror...");
    }

    override public void PickUp()
    {
        base.PickUp();
    }
}
