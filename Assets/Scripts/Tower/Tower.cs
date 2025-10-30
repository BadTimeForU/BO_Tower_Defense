using UnityEngine;
using UnityEngine.UI;

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
    public AudioClip upgradeSound;
    private AudioSource audioSource;

    private float fireCountdown = 0f;
    private Transform target;

    [Header("UI")]
    public GameObject upgradeUIPrefab;
    private GameObject activeUI;

    void Start()
    {
        if (towerAnimator == null)
            towerAnimator = GetComponent<TowerAnimator>();

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

    void OnMouseDown()
    {
        if (activeUI != null)
        {
            Destroy(activeUI);
            activeUI = null;
            return;
        }

        if (upgradeUIPrefab == null)
        {
            Debug.LogWarning("Upgrade UI prefab is niet ingesteld!");
            return;
        }

        Vector3 uiPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
        activeUI = Instantiate(upgradeUIPrefab, uiPos, Quaternion.identity, GameObject.Find("Canvas").transform);

        Button upgradeBtn = activeUI.transform.Find("UpgradeButton").GetComponent<Button>();
        Button closeBtn = activeUI.transform.Find("CloseButton").GetComponent<Button>();

        upgradeBtn.onClick.AddListener(() =>
        {
            UpgradeTower();
        });

        closeBtn.onClick.AddListener(() =>
        {
            Destroy(activeUI);
            activeUI = null;
        });
    }

    public void UpgradeTower()
    {
        if (upgradedPrefab == null)
        {
            Debug.Log("Geen upgrade prefab ingesteld!");
            return;
        }

        if (GameManager.instance.coins < upgradeCost)
        {
            Debug.Log("Niet genoeg coins om te upgraden!");
            return;
        }

        GameManager.instance.SpendCoins(upgradeCost);

        if (upgradeSound != null)
            audioSource.PlayOneShot(upgradeSound);

        GameObject upgradedTower = Instantiate(upgradedPrefab, transform.position, transform.rotation);
        Destroy(gameObject);

        if (activeUI != null)
            Destroy(activeUI);
    }
}
