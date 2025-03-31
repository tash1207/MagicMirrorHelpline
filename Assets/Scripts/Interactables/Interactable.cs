using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] string defaultThought;

    protected string objectName = "object";
    protected bool canPickUp = false;

    protected bool hasInteracted = false;

    public virtual void Interact()
    {
        if (defaultThought != null && defaultThought != "")
        {
            Think(defaultThought);
        }
        hasInteracted = true;
    }

    public virtual bool PickUp()
    {
        if (!canPickUp)
        {
            Think("Why would I want to pick that up?");
            return false;
        }
        else if (!hasInteracted)
        {
            Think("I haven't even seen what it is yet!");
            return false;
        }
        else
        {
            Think("Picked up " + objectName);
            Destroy(gameObject);
            return true;
        }
    }

    protected void Think(string text)
    {
        InternalDialogManager.Instance.ShowDialog(text);
    }

    protected void Think(string[] thoughts)
    {
        InternalDialogManager.Instance.ShowDialog(thoughts);
    }

    public string GetName()
    {
        return objectName;
    }
}
