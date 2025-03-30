using UnityEngine;

public class Mirror : Interactable
{
    override public void Interact()
    {
        base.Interact();
        Think("I'm sure someone will call the Magic Mirror Helpline while I'm stuck here.");
    }
}
