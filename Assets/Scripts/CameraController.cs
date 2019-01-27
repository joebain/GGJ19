using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform subject;
    public float xOffset;
    

    void LateUpdate()
    {
        transform.position = new Vector3(subject.position.x + xOffset, transform.position.y, transform.position.z);
    }
}
