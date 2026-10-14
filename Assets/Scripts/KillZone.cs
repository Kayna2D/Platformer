using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var p = other.GetComponent<PlayerController>();
        if (p != null) p.Fall();
    }
}
