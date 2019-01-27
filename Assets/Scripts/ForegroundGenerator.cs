using UnityEngine;

public class ForegroundGenerator : MonoBehaviour
{
    public Sprite[] Sprites;

    public float SpacingMin = 0.2f, SpacingMax = 2f;
    public float ScaleMin = 1f, ScaleMax = 2f;
    public float Distance = 10f;

    [InspectorButton("DoGenerate")]
    public bool _doGenerate;

    void Start()
    {
        DoGenerate();
    }

    public void DoGenerate() {

        for (int t = transform.childCount-1; t >= 0; t--)
        {
            DestroyImmediate(transform.GetChild(t).gameObject);
        }

        float distance = 0;
        int i = 1;
        while (distance < Distance)
        {
            distance += Random.Range(SpacingMin, SpacingMax);
            GameObject sprite = new GameObject("foreground_sprite_" + i);
            SpriteRenderer renderer = sprite.AddComponent<SpriteRenderer>();
            renderer.sortingOrder = 2;
            renderer.sprite = Sprites[Random.Range(0, Sprites.Length - 1)];
            float flip = 1;
            if (Random.value > 0.5f)
            {
                flip = -1;
            }
            float scale = Random.Range(ScaleMin, ScaleMax);
            sprite.transform.localScale = new Vector3(scale * flip, scale);
            sprite.transform.parent = transform;
            sprite.transform.localPosition = new Vector2(distance, renderer.sprite.bounds.extents.y*scale);

            i++;
        }
    }
}
