using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideHUD()
    {
        Debug.Log("Hiding!");
        GetComponent<Canvas>().enabled = false;
    }

    public void DisplayHUD()
    {
        Debug.Log("Showing!");
        GetComponent<Canvas>().enabled = true;
    }
}
