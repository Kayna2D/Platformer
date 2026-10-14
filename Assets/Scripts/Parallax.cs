using UnityEngine;

// parallaxX: 0 = fixa na tela (céu) ... 1 = move junto com o mundo (primeiro plano).
public class Parallax : MonoBehaviour
{
    [Range(0f, 1f)] public float parallaxX = 0.5f;
    [Range(0f, 1f)] public float parallaxY = 0.9f;
    public bool repeat = true;

    Transform cam;
    float startX, startY, length;

    void Start()
    {
        cam = Camera.main.transform;
        startX = transform.position.x;
        startY = transform.position.y;
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null && sr.sprite != null)
            length = sr.sprite.bounds.size.x * transform.lossyScale.x;
    }

    void LateUpdate()
    {
        float distX = cam.position.x * parallaxX;
        float distY = cam.position.y * parallaxY;
        transform.position = new Vector3(startX + distX, startY + distY, transform.position.z);

        if (!repeat || length <= 0f) return;
        float rel = cam.position.x * (1f - parallaxX);
        if (rel > startX + length) startX += length;
        else if (rel < startX - length) startX -= length;
    }
}
