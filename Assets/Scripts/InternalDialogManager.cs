using TMPro;
using UnityEngine;

public class InternalDialogManager : MonoBehaviour
{
    public static InternalDialogManager Instance { get; private set; }

    [SerializeField] GameObject dialogCanvas;
    [SerializeField] TMP_Text dialogText;

    int currentIndex = 0;
    string[] dialogTexts = new string[0];
    Interactable currentInteractable;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ShowDialog(string text)
    {
        FindAnyObjectByType<Player>().PausePlayerMovement();
        dialogText.text = text;
        dialogCanvas.SetActive(true);
    }

    public void ShowDialog(string[] texts)
    {
        dialogTexts = texts;
        ShowDialog(texts[0]);
    }

    public void ShowDialog(string text, Interactable interactable)
    {
        FindAnyObjectByType<Player>().PausePlayerMovement();
        dialogText.text = text;
        dialogCanvas.SetActive(true);
        currentInteractable = interactable;
    }

    public void ShowDialog(string[] texts, Interactable interactable)
    {
        dialogTexts = texts;
        ShowDialog(texts[0], interactable);
    }

    public void NextDialog()
    {
        if (dialogTexts.Length == 0 || currentIndex == dialogTexts.Length - 1)
        {
            HideDialog();
        }
        else
        {
            currentIndex++;
            ShowDialog(dialogTexts[currentIndex]);
        }
    }

    public void HideDialog()
    {
        currentIndex = 0;
        dialogTexts = new string[0];
        dialogCanvas.SetActive(false);
        FindAnyObjectByType<Player>().pausePlayerMovement = false;

        if (currentInteractable != null)
        {
            currentInteractable.FollowUpAction();
            if (Inventory.Instance.inventoryEnabled)
            {
                currentInteractable.PickUp();
            }
            currentInteractable = null;
        }
    }
}
