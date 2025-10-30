using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 2f;
    public int baseHealth = 100;          // basis health
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

    private Transform endWaypoint;

    [Header("Death Effect (8-bit particles)")]
    public GameObject deathEffectPrefab;

    [Header("Death Sound")]
    public AudioClip deathSound;
    private AudioSource audioSource;

    private int waveBonus = 0; // extra HP per wave

    void Start()
    {
       
        if (GameManager.instance != null)
        {
            waveBonus = (GameManager.instance.currentWave - 1) * 5;
        }

        
        currentHealth = baseHealth + waveBonus;

        
        targetWaypoint = Waypoints.points[0];

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

        // Audio setup
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
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
        if (endWaypoint != null && Vector3.Distance(transform.position, endWaypoint.position) <= 0.5f)
        {
            ReachGoal();
            return;
        }

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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        GameManager.instance.AddCoins(coinReward);

        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        Destroy(gameObject, 0.3f);
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
