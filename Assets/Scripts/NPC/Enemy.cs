using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 2f;
    public int maxHealth = 100;
    private int currentHealth;
    public int coinReward = 1;

    [Header("Pathfinding")]
    private Transform targetWaypoint;
    private int waypointIndex = 0;

    [Header("Sprite Animation")]
    public Sprite[] runSprites;    
    public float fps = 10f;       
    private int frameIndex = 0;
    private float frameTimer = 0f;
    private SpriteRenderer spriteRenderer;

    
    public int[] flipWaypoints = { 2, 6, 14 };

    void Start()
    {
        currentHealth = maxHealth;

        
        targetWaypoint = Waypoints.points[0];

        
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (runSprites.Length > 0)
        {
            spriteRenderer.sprite = runSprites[0];
        }
    }

    void Update()
    {
        MoveAlongPath();
        AnimateRun();
    }

    
    void MoveAlongPath()
    {
        Vector3 dir = targetWaypoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            ReachGoal();
            return;
        }

        waypointIndex++;
        targetWaypoint = Waypoints.points[waypointIndex];

        
        if (System.Array.Exists(flipWaypoints, w => w == waypointIndex))
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1; 
            transform.localScale = scale;
        }
    }

    void ReachGoal()
    {
        GameManager.instance.LoseLife(1);
        Destroy(gameObject);
    }

    // --- Combat ---
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.instance.AddCoins(coinReward);
        Destroy(gameObject);
    }

    
    void AnimateRun()
    {
        if (runSprites.Length == 0) return;

        frameTimer += Time.deltaTime;
        if (frameTimer >= 1f / fps)
        {
            frameTimer = 0f;
            frameIndex = (frameIndex + 1) % runSprites.Length;
            spriteRenderer.sprite = runSprites[frameIndex];
        }
    }
}
