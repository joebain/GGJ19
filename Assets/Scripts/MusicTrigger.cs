using System;
using System.Collections;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public string EventName;
    public float Delay = 0;

    //[InspectorButton("DoPlay")]
    //public bool _doPlay;

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
        Debug.Log("play " + EventName);
        AkSoundEngine.PostEvent(EventName, gameObject);
    }
}
