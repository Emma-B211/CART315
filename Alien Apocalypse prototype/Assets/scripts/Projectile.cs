using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed = 12f;
    public int damage= 1;
    public float lifetime = 3f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start(){
        rb.gravityScale = 0f;
        rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other){
        EnemyAI enemy= other.GetComponentInParent<EnemyAI>();
        
        if (enemy != null){
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        if (!other.isTrigger){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
