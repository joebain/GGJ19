using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class PipeSection : MonoBehaviour
{
    public enum JointType
    {
        Small,
        Medium,
        Big
    }

    public JointType StartJoint, EndJoint;
    private Tilemap tilemap;

    public BoundsInt SectionSize { get { return tilemap.cellBounds; } }
    public int VerticalOffset = 0;

    public Tilemap Tilemap { get { if (tilemap == null) { tilemap = GetComponent<Tilemap>(); } return tilemap; } }
}
