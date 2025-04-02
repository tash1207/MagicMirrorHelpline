using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] string defaultThought;

    protected string objectName = "object";
    protected bool canPickUp = false;

    protected bool hasInteracted = false;
    protected bool performFollowUpAction = false;
    protected bool dontDestroyOnPickup = false;

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
        if (canPickUp)
        {
            Think("Picked up " + objectName);
            if (dontDestroyOnPickup)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
            return true;
        }
        return false;
    }

    public virtual void FollowUpAction() { }

    protected void Think(string text)
    {
        InternalDialogManager.Instance.ShowDialog(text, this);
    }

    protected void Think(string[] thoughts)
    {
        InternalDialogManager.Instance.ShowDialog(thoughts, this);
    }

    public string GetName()
    {
        return objectName;
    }
}
