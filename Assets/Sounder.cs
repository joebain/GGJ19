using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounder : MonoBehaviour
{
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
        AkSoundEngine.PostEvent("Play_Music_01", gameObject);
    }

    public void PlayMusic02()
    {
        AkSoundEngine.PostEvent("Play_Music_02", gameObject);
    }

    public void PauseMusic()
    {
        AkSoundEngine.PostEvent("Pause_Music", gameObject);
    }

    public void UnpauseMusic()
    {
        AkSoundEngine.PostEvent("Unpause_Music", gameObject);
    }
}
