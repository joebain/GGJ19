using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameEntry : MonoBehaviour
{

    public InputField input;

    // Start is called before the first frame update
    void Start()
    {
        input.onEndEdit.AddListener(ListenToSubmit);
    }


    public void ListenToSubmit(string playerName)
    {
        Debug.Log("TEST: " + playerName);
    }

    public void save()
    {
        Debug.Log("TEST: " + input.text);
        Game.Instance.player.Name = input.text;
    }


}
