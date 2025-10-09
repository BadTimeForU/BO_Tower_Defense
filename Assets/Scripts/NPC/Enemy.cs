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

    [Header("Flip bij bepaalde Waypoints")]
    public int[] flipWaypoints = { 2, 6, 14 };

    private Transform endWaypoint; // Wordt automatisch gevonden met tag

    void Start()
    {
        currentHealth = maxHealth;

        // Vind alle waypoints
        targetWaypoint = Waypoints.points[0];

        // Zoek het einde via tag
        GameObject endObj = GameObject.FindGameObjectWithTag("EndWaypoint");
        if (endObj != null)
        {
            endWaypoint = endObj.transform;
        }
        else
        {
            Debug.LogWarning("Geen object met tag 'EndWaypoint' gevonden!");
        }

        // Sprite setup
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (runSprites.Length > 0)
            spriteRenderer.sprite = runSprites[0];
    }

    void Update()
    {
        MoveAlongPath();
        AnimateRun();
    }

    void MoveAlongPath()
    {
        if (targetWaypoint == null) return;

        Vector3 dir = targetWaypoint.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        // Controleer of we bij het EndWaypoint zijn
        if (endWaypoint != null && Vector3.Distance(transform.position, endWaypoint.position) <= 0.5f)
        {
            ReachGoal();
            return;
        }

        // Normaal verdergaan
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            ReachGoal();
            return;
        }

        waypointIndex++;
        targetWaypoint = Waypoints.points[waypointIndex];

        // Flip sprite bij bepaalde waypoints
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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
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
