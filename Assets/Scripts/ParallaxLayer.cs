using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float ParallaxFactor = 2f;
    
    void LateUpdate()
    {
        transform.position = new Vector3(-Camera.main.transform.position.x * ParallaxFactor, transform.position.y, transform.position.z);
    }

}
