using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sfx
{

}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    private AudioSource bgmAudioSorce;

    [SerializeField]
    private List<AudioClip> sfxClipList = new List<AudioClip>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        Audio audio = PoolManager.Instance.Pop("Audio") as Audio;
        AudioSource audioSource = audio.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip = sfxClipList[(int)sfx];
        audioSource.Play();
    }

    public void PlaySfxRandomness(Sfx sfx, float randomness)
    {
        Audio audio = PoolManager.Instance.Pop("Audio") as Audio;
        AudioSource audioSource = audio.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip = sfxClipList[(int)sfx];
        audioSource.Play();
    }
}
