using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SetCanvasScale : MonoBehaviour
{
    private CanvasScaler scaler;
    private PixelPerfectCamera cam;

    // Start is called before the first frame update
    void Awake()
    {
        scaler = GetComponent<CanvasScaler>();
        cam = FindObjectOfType<PixelPerfectCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        scaler.scaleFactor = cam.pixelRatio;
    }
}
