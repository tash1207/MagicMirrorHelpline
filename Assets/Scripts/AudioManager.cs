using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip officeAudio;
    [SerializeField] AudioClip magicAudio;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlayOfficeAudio()
    {
        PlayAudio(officeAudio);
    }

    public void PlayMagicAudio()
    {
        PlayAudio(magicAudio);
    }

    void PlayAudio(AudioClip audioClip)
    {
        audioSource.Pause();
        float timestamp = audioSource.time;
        audioSource.clip = audioClip;
        audioSource.time = timestamp;
        audioSource.Play();
    }
}
