using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public Transform[] enemyPrefabs;
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
            UIManager.instance.UpdateWave(waveIndex);

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
        int rand = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyGO = Instantiate(enemyPrefabs[rand], spawnPoint.position, spawnPoint.rotation);

        
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.maxHealth += (waveIndex - 1) * hpIncreasePerWave;
            enemy.TakeDamage(0); 
        }
    }
}