using System;
using System.Collections;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public string EventName;
    public float Delay = 0;

    public static string MusicPlaying;

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
        Debug.Log("do play " + EventName + ", music playing: " + MusicPlaying);
        if (MusicPlaying != EventName)
        {
            AkSoundEngine.PostEvent(EventName, gameObject);
            MusicPlaying = EventName;
        }
    }
}
