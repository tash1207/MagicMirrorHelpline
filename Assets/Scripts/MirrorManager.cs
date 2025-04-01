using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    public static MirrorManager Instance { get; private set; }

    [SerializeField] GameObject[] mirrors;
    [SerializeField] AudioClip firstMirrorRingingSound;

    bool hasEnabledFirstMirror = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < mirrors.Length; i++)
        {
            mirrors[i].GetComponent<Mirror>().OnSceneStarted += OnSceneStarted;
            mirrors[i].GetComponent<Mirror>().OnSceneFinished += OnSceneFinished;
        }
    }

    void OnSceneStarted(Mirror.MirrorScene scene)
    {
        if (scene == Mirror.MirrorScene.Cinderella)
        {
            EnableMirror3();
            EnableMirror5();
            Inventory.Instance.EnableInventory();
        }
        else if (scene == Mirror.MirrorScene.Jack)
        {
            EnableMirror4();
            EnableMirror6();
        }
    }

    void OnSceneFinished(Mirror.MirrorScene scene)
    {
        if (scene == Mirror.MirrorScene.Cinderella ||
            scene == Mirror.MirrorScene.LittleMermaid ||
            scene == Mirror.MirrorScene.RobinHood)
        {
            if (mirrors[0].GetComponent<Mirror>().finishedMirrorTask &&
                mirrors[2].GetComponent<Mirror>().finishedMirrorTask &&
                mirrors[4].GetComponent<Mirror>().finishedMirrorTask)
                {
                    EnableMirror2();
                    EnableMirror4();
                    EnableMirror6();
                }
        }
        else if (scene == Mirror.MirrorScene.Jack ||
            scene == Mirror.MirrorScene.RedRiding ||
            scene == Mirror.MirrorScene.SnowWhite)
        {
            // Check if we have all ingredients then enable Little Mermaid final scene
            if (Inventory.Instance.HasAllIngredientsExceptLast())
            {
                EnableMirror3();
            }
        }
    }

    public bool HasEnabledFirstMirror()
    {
        return hasEnabledFirstMirror;
    }

    // Cinderella
    public void EnableMirror1()
    {
        SoundFXManager.Instance.PlaySoundFXClip(firstMirrorRingingSound, 1f);
        mirrors[0].GetComponent<Mirror>().StartRinging();
        hasEnabledFirstMirror = true;
    }

    // Jack
    public void EnableMirror2()
    {
        mirrors[1].GetComponent<Mirror>().StartRinging();
    }

    // Little Mermaid
    public void EnableMirror3()
    {
        mirrors[2].GetComponent<Mirror>().StartRinging();
    }

    // Red Riding Hood
    public void EnableMirror4()
    {
        mirrors[3].GetComponent<Mirror>().StartRinging();
    }

    // Robin Hood
    public void EnableMirror5()
    {
        mirrors[4].GetComponent<Mirror>().StartRinging();
    }

    // Snow White
    public void EnableMirror6()
    {
        mirrors[5].GetComponent<Mirror>().StartRinging();
    }
}
