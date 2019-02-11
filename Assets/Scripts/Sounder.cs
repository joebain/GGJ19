using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounder : MonoBehaviour
{
    [InspectorButton("PlayMusic01")]
    public bool _playMusic01;
    [InspectorButton("PlayMusic02")]
    public bool _playMusic02;

    static Sounder instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX01()
    {
        AkSoundEngine.PostEvent("Play_SFX_01", gameObject);
    }

    public void PlaySFX02()
    {
        AkSoundEngine.PostEvent("Play_SFX_02", gameObject);
    }

    public void PlaySFX03()
    {
        AkSoundEngine.PostEvent("Play_SFX_03", gameObject);
    }

    public void PlaySFX04()
    {
        AkSoundEngine.PostEvent("Play_SFX_04", gameObject);
    }

    public void PlaySFX05()
    {
        AkSoundEngine.PostEvent("Play_SFX_05", gameObject);
    }

    public void PlayMusic01()
    {
        AkSoundEngine.PostEvent("Start_Music", gameObject);
    }

    public void PlayMusic02()
    {
        AkSoundEngine.PostEvent("StartGame", gameObject);
    }

    public void PauseMusic()
    {
        AkSoundEngine.PostEvent("Pause_Music", gameObject);
    }

    public void UnpauseMusic()
    {
        AkSoundEngine.PostEvent("Resume_Music", gameObject);
    }
}
