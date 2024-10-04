using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum CommonAudios
{
    Select,
    Hit,
    Faint,
    None // Add None to your enum
}

[System.Serializable]
public class AudioData
{
    public CommonAudios commonAudios;
    public AudioClip clip;

    public AudioData(CommonAudios commonAudios, AudioClip clip)
    {
        this.commonAudios = commonAudios;
        this.clip = clip;
    }
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource soundEffects;

    [SerializeField] List<AudioData> commonAudios = new List<AudioData>();

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public AudioClip GetAudioClip(CommonAudios audioId) // Change parameter name to audioId
    {
        foreach (var audioData in commonAudios)
        {
            if (audioData.commonAudios == audioId)
            {
                return audioData.clip;
            }
        }
        return null;
    }

    public void PlaySoundEffect(CommonAudios audioId) // Change parameter name to audioId
    {
        if (audioId == CommonAudios.None) // Change comparison to CommonAudios.None
        {
            return;
        }
        else
        {
            soundEffects.PlayOneShot(GetAudioClip(audioId));
        }
    }

    public void PlayMusic(AudioClip clip, bool loop = true, bool fade = true)
    {
        if (clip == null)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayMusicAsync(clip, loop, fade));
        }
    }

    IEnumerator PlayMusicAsync(AudioClip clip, bool loop, bool fade)
    {
        if (fade == true)
        {
            yield return music.DOFade(0, 1f).WaitForCompletion();
        }

        music.clip = clip;
        music.loop = loop;
        music.Play();

        if (fade == true)
        {
            yield return music.DOFade(1, 1f).WaitForCompletion();
        }
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
        else
        {
            soundEffects.PlayOneShot(clip);
        }
    }
}
