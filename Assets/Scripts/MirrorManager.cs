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

    void Start()
    {
        // Iterate through mirrors and hook into their public Action<MirrorType> sceneStarted, sceneFinished
        mirrors[0].GetComponent<Mirror>().OnSceneStarted += OnSceneStarted;
    }

    void OnSceneStarted(Mirror.MirrorScene scene)
    {
        if (scene == Mirror.MirrorScene.Cinderella)
        {
            EnableMirror3();
            EnableMirror5();
        }
        else if (scene == Mirror.MirrorScene.Jack)
        {
            EnableMirror4();
            EnableMirror6();
        }
    }

    void OnSceneFinished(Mirror.MirrorScene scene)
    {
        if (scene == Mirror.MirrorScene.Jack ||
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

    // Cinderella
    public void EnableMirror1()
    {
        mirrors[0].GetComponent<Mirror>().StartRinging();
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
