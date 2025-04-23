using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip onPlay;
    [SerializeField] private AudioClip onButtonClick;
    [SerializeField] private AudioClip windAmbience;
    [SerializeField] private AudioSource AmbienceSource;
    [SerializeField] private AudioSource oneShotSource;
    [SerializeField] private AudioSource OstSource;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }


    public void PlayWindAmbience()
    {
        AmbienceSource.loop = true;
        AmbienceSource.clip = windAmbience;
        AmbienceSource.Play();
    }

    public void PlayButtonClick()
    {
        oneShotSource.PlayOneShot(onButtonClick);
    }
}
