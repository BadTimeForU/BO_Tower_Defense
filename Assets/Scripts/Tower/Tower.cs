using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Stats")]
    public int cost = 10;
    public float range = 5f;
    public float fireRate = 1f;
    public int damage = 20;

    [Header("Upgrade Stats")]
    public GameObject upgradedPrefab;
    public int upgradeCost = 20;

    [Header("Projectile")]
    public GameObject projectilePrefab;

    [Header("Animation")]
    public TowerAnimator towerAnimator;

    [Header("Audio")]
    public AudioClip shootSound;           
    private AudioSource audioSource;       

    private float fireCountdown = 0f;
    private Transform target;

    void Start()
    {
        if (towerAnimator == null)
            towerAnimator = GetComponent<TowerAnimator>();

        // Audio setup
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    void Update()
    {
        FindTarget();

        if (target == null)
        {
            if (towerAnimator != null)
                towerAnimator.PlayIdle();
            return;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    void Shoot()
    {
        if (towerAnimator != null)
            towerAnimator.PlayAttack();
        
        if (shootSound != null && audioSource != null)
            audioSource.PlayOneShot(shootSound);

        GameObject projectileGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile proj = projectileGO.GetComponent<Projectile>();
        if (proj != null)
            proj.Seek(target, damage);

        Invoke(nameof(BackToIdle), 0.3f);
    }

    void BackToIdle()
    {
        if (towerAnimator != null)
            towerAnimator.PlayIdle();
    }
}
