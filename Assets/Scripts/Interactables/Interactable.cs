using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected string objectName = "object";
    protected bool canPickUp = false;

    protected bool hasInteracted = false;

    public virtual void Interact()
    {
        hasInteracted = true;
    }

    public virtual void PickUp()
    {
        if (!canPickUp)
        {
            Debug.Log("I can't pick that up!");
        }
        else if (!hasInteracted)
        {
            Debug.Log("I haven't even seen what it is yet!");
        }
        else
        {
            Debug.Log("Picked up " + objectName);
            Destroy(gameObject);
        }
    }
}
