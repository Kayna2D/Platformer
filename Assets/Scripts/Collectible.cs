using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    public int points = 10;
    public bool countsForGoal = true;   // conta para vencer a fase
    public bool givesLife = false;      // item de vida (desmarque countsForGoal)

    void Reset() => GetComponent<Collider2D>().isTrigger = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.Instance.AddScore(points);
        if (givesLife) GameManager.Instance.AddLife();
        if (countsForGoal) GameManager.Instance.ItemCollected();
        Destroy(gameObject);
    }
}
