using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 6f;
    public float jumpForce = 12f;

    [Header("Chão")]
    public Transform groundCheck;          // objeto filho nos pés do jogador
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;          // layer "Ground" (o Tilemap)

    [Header("Dano")]
    public float invulnerableTime = 1.5f;
    public Vector2 knockback = new Vector2(5f, 6f);

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    Vector3 spawnPoint;
    float move;
    bool grounded;
    float invulnerableUntil;
    float knockbackUntil;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        spawnPoint = transform.position;
    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && grounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);   

        if (move != 0) sr.flipX = move < 0;

        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(move));
            anim.SetBool("Grounded", grounded);
            anim.SetFloat("VelY", rb.linearVelocity.y);
        }
    }

    void FixedUpdate()
    {
        if (Time.time < knockbackUntil) return;   // deixa o knockback agir
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);
    }

    public void TakeDamage(Vector2 sourcePosition)
    {
        if (Time.time < invulnerableUntil) return;
        invulnerableUntil = Time.time + invulnerableTime;

        GameManager.Instance.LoseLife();

        float dir = transform.position.x >= sourcePosition.x ? 1f : -1f;
        knockbackUntil = Time.time + 0.2f;
        rb.linearVelocity = new Vector2(dir * knockback.x, knockback.y);
        StartCoroutine(Blink());
    }

    public void Bounce() => rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 0.7f);

    public void Fall()   // caiu no abismo
    {
        GameManager.Instance.LoseLife();
        transform.position = spawnPoint;
        rb.linearVelocity = Vector2.zero;
        invulnerableUntil = Time.time + 1f;
    }

    IEnumerator Blink()
    {
        while (Time.time < invulnerableUntil)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        sr.enabled = true;
    }
}
