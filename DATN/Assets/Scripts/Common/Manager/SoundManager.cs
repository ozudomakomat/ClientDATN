using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance {
        get {
            if (instance == null) {
                instance = new SoundManager();
            }
            return instance;
        }
    }
    public AudioSource music;
    public AudioSource sfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void Init() {
        music.mute = SaveData.Instance.IsActiveMusic();
        sfx.mute = SaveData.Instance.IsActiveSfx();
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.AddListener(EventName.SFX_CHANGE, OnChange);
        EventDispatcher.Instance.AddListener(EventName.SOUND_CHANGE, OnChange);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventName.SFX_CHANGE, OnChange);
        EventDispatcher.Instance.RemoveListener(EventName.SOUND_CHANGE, OnChange);
    }

    private void OnChange(EventName eventType, object data)
    {
        if (eventType == EventName.SFX_CHANGE)
        {
            sfx.mute = !SaveData.Instance.IsActiveSfx();
            SaveData.Instance.SaveActiveSfx(sfx.mute);
        }
        else if (eventType == EventName.SOUND_CHANGE)
        {
            music.mute = !SaveData.Instance.IsActiveMusic();
            SaveData.Instance.SaveActiveMusic(music.mute);
        }
    }

    public void SetMusicVolume(float volume)
    {
        music.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        sfx.volume = volume;
    }

    public void PlaySFXClip(AudioClip audio, float vol=1f)
    {
        sfx.PlayOneShot(audio, vol);
    }

    public void PlayMusicClip(AudioClip audio, float vol=1f)
    {
        music.clip = audio;
        music.Play();
    }

}
