using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    public static MirrorManager Instance { get; private set; }

    [SerializeField] GameObject[] mirrors;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void EnableMirror1()
    {
        mirrors[0].GetComponent<Mirror>().StartRinging();
    }

    public void DisableMirror1()
    {
        mirrors[0].GetComponent<Mirror>().SetToDefault();
    }
}
