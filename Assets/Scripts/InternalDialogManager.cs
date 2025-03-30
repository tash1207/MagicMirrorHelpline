using TMPro;
using UnityEngine;

public class InternalDialogManager : MonoBehaviour
{
    public static InternalDialogManager Instance { get; private set; }

    [SerializeField] GameObject dialogCanvas;
    [SerializeField] TMP_Text dialogText;

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
        dialogText.text = text;
        dialogCanvas.SetActive(true);
    }

    public void HideDialog()
    {
        dialogCanvas.SetActive(false);
    }
}
