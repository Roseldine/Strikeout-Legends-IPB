
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource _uiSource;
    [SerializeField] AudioSource _gameplaySource;
    [SerializeField] AudioSource _musicSource;

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = GetComponent<AudioManager>();
    }

    public void PlayUISound(AudioClip clip) => _uiSource.PlayOneShot(clip);
    public void PlayAttackSound(AudioClip clip) => _gameplaySource.PlayOneShot(clip);
    public void PlayMusic(AudioClip clip)
    {
        _musicSource.Stop();
        _musicSource.clip = clip;
        _musicSource.Play();
    }
}
