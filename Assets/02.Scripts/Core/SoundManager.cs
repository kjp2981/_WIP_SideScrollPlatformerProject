using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum Sfx
{

}

public enum Bgm
{

}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [SerializeField]
    private AudioSource bgmAudioSorce;

    #region AudioClip List
    [SerializeField, Foldout("Clip List")]
    private List<AudioClip> sfxClipList = new List<AudioClip>();
    [SerializeField, Foldout("Clip List")]
    private List<AudioClip> bgmClipList = new List<AudioClip>();
    #endregion

    private float sfxVolume = 1f;
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
        audioSource.volume = sfxVolume;
        audioSource.Play();
    }

    public void PlayBgm(Bgm bgm)
    {
        if(bgmAudioSorce.loop == false)
            bgmAudioSorce.loop = true;

        bgmAudioSorce.Stop();
        bgmAudioSorce.clip = bgmClipList[(int)bgm];
        bgmAudioSorce.Play();
    }

    public void BgmVolume(float volume)
    {
        bgmAudioSorce.volume = volume;
    }

    public void SfxVolume(float volome)
    {
        sfxVolume = volome;
    }
}
