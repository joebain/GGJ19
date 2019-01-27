using System;
using System.Collections;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public string EventName;
    public float Delay = 0;

    public static string MusicPlaying;
    private static bool debugAlreadyStarted;

    void Start()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        yield return new WaitForSeconds(Delay);
        DoPlay();
    }

    private void DoPlay()
    {
        if (debugAlreadyStarted) return;
        debugAlreadyStarted = true;
        if (MusicPlaying != EventName)
        {
            Debug.Log("do play " + EventName + ", music playing: " + MusicPlaying);
            AkSoundEngine.PostEvent(EventName, gameObject);
            MusicPlaying = EventName;
        }
        else
        {
            Debug.Log("avoiding playing " + EventName + ", music playing: " + MusicPlaying);
        }
    }
}
