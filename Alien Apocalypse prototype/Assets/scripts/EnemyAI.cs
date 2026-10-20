using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType{
        Infected,
        Runner
    }

    [Header("Enemy Type")]
    public EnemyType enemyType = EnemyType.Infected;

    [Header("Stats")]
    public int maxHealth = 3;
    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 3f;
    public float detectionRange = 6f;
    public float attackRange = 1f;
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;

    [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;

    private int currentHealth;
    private Transform player;
    private Transform targetPoint;
    private float nextAttackTime;
    private bool isDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        player = playerObject.transform;

        if (pointA != null && pointB != null)
        targetPoint = pointB;

        if (enemyType == EnemyType.Runner){
            chaseSpeed += 1.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead || player == null)
        return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance <= attackRange){
            AttackPlayer();
        } else if (distance <= detectionRange){
            ChasePlayer();
        } else {
            Patrol();
        }
    }

    void Patrol (){
        if (pointA == null ||pointB == null)
        return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint.position,
            patrolSpeed * Time.deltaTime
        );

        if (Vector2.Distance(
            transform.position,
            targetPoint.position
        ) <0.1f)
        {
            targetPoint = targetPoint == pointA 
            ? pointB
            : pointA;
        }
    }

    void ChasePlayer(){
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            chaseSpeed * Time.deltaTime
        );
        FaceTarget(player.position);
    }

    void AttackPlayer(){
        FaceTarget(player.position);

        if (Time.time < nextAttackTime)
        return;

        PlayerHealth health = 
        player.GetComponent < PlayerHealth> ();

        if (health != null){
            health.TakeDamage(attackDamage);
        }

        nextAttackTime = Time.time + attackCooldown;
    }

    void FaceTarget (Vector3 target){
        
        if (target.x > transform.position.x){
            transform.localScale = new Vector3(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    public void TakeDamage(int damage){
        if(isDead)
            return;

        currentHealth -= damage;

        Debug.Log (gameObject.name + "HP:" + currentHealth);

        if (currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        isDead = true;
        Destroy(gameObject);
    }
}
