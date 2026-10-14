using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth = 5f;
    public Vector3 offset = new Vector3(0, 1, -10);
    public float minX = -1000f, maxX = 1000f;   // limites do mapa
    public float minY = -1000f, maxY = 1000f;

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desired = target.position + offset;
        desired.x = Mathf.Clamp(desired.x, minX, maxX);
        desired.y = Mathf.Clamp(desired.y, minY, maxY);
        transform.position = Vector3.Lerp(transform.position, desired, smooth * Time.deltaTime);
    }
}
