using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform subject;
    private float offset;

    void Awake()
    {
        offset = transform.position.x - subject.position.x;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(subject.position.x + offset, transform.position.y, transform.position.z);
    }
}
