using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float patrolDistance = 3f;
    public int pointsOnStomp = 20;
    public bool canBeStomped = true;

    Rigidbody2D rb;
    SpriteRenderer sr;
    float startX;
    int dir = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        startX = transform.position.x;
    }

    void FixedUpdate()
    {
        float x = transform.position.x;
        if (x > startX + patrolDistance) dir = -1;
        else if (x < startX - patrolDistance) dir = 1;

        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        sr.flipX = dir > 0;   
    }

    void OnCollisionEnter2D(Collision2D c) => HandleHit(c);
    void OnCollisionStay2D(Collision2D c) => HandleHit(c);

    void HandleHit(Collision2D c)
    {
        var player = c.collider.GetComponent<PlayerController>();
        if (player == null) return;

        bool stomp = canBeStomped &&
                     c.collider.bounds.min.y > GetComponent<Collider2D>().bounds.center.y;

        if (stomp)
        {
            GameManager.Instance.AddScore(pointsOnStomp);
            player.Bounce();
            Destroy(gameObject);
        }
        else
        {
            player.TakeDamage(transform.position);
        }
    }
}
