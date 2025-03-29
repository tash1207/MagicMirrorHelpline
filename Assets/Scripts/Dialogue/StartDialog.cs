using cherrydev;
using UnityEngine;

public class StartDialog : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph CinderellaDialogGraph;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogBehaviour.StartDialog(CinderellaDialogGraph);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
