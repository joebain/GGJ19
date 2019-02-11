using System.Collections;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public string EventName;

    public bool OneShot = false;

    public static bool Triggered = false;

    public float Delay = 0;

    void Start()
    {
        if (!OneShot || !Triggered)
        {
            StartCoroutine(Play());
        }
    }

    private IEnumerator Play()
    {
        yield return new WaitForSeconds(Delay);
        DoPlay();
    }

    private void DoPlay()
    {
        Debug.Log("do play " + EventName);
        AkSoundEngine.PostEvent(EventName, gameObject);
        Triggered = true;
    }
}
