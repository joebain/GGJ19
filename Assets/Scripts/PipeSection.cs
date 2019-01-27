using UnityEngine;
using UnityEngine.Tilemaps;

public class PipeSection : MonoBehaviour
{
    public enum JointType
    {
        Small,
        Medium,
        Big
    }

    public JointType StartJoint, EndJoint;
    public Tilemap[] tilemaps;

    public BoundsInt SectionSize { get { return tilemaps[0].cellBounds; } }
    public int VerticalOffset = 0;

    public Tilemap[] Tilemaps { get { return tilemaps; } }
}
