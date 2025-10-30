using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    public int hpIncreasePerWave = 5;

    private int waveIndex = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            waveIndex++;

            // Update wave in GameManager en UI
            GameManager.instance.SetWave(waveIndex);
            UIManager.instance.UpdateWave(waveIndex);

            // Spawn enemies
            for (int i = 0; i < waveIndex + 2; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("Geen enemies ingesteld in WaveSpawner!");
            return;
        }

        int rand = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyGO = Instantiate(enemyPrefabs[rand], spawnPoint.position, spawnPoint.rotation);

        Enemy enemy = enemyGO.GetComponent<Enemy>();
        if (enemy != null)
        {
            
            int extraHP = (waveIndex - 1) * hpIncreasePerWave;
            //enemy.SetWaveBonusHP(extraHP);
        }
    }
}