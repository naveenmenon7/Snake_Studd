using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _normalFood;
    [SerializeField] private AudioClip _bonusFood;
    [SerializeField] private AudioClip _snakeDeath;

    private void Start()
    {
        _musicSource.clip = _backgroundMusic;
        _musicSource.loop = true;
        _musicSource.volume = 0.2f;
        _musicSource.Play();
    }

    public void PlayButtonClick()
    {
        _sfxSource.PlayOneShot(_buttonClick, 0.65f);
    }

    public void PlayFoodCollect(bool isBonus)
    {
        AudioClip selectedClip = isBonus ? _bonusFood : _normalFood;

        _sfxSource.PlayOneShot(selectedClip, 0.8f);
    }

    public void PlaySnakeDeath()
    {
        _sfxSource.PlayOneShot(_snakeDeath, 0.8f);
    }
}