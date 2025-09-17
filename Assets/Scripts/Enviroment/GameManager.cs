using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int coins = 0;
    public int HP = 10;
    public int currentWave = 0;
    public int enemiesAlive = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UIManager.instance.UpdateCoins(coins);
        UIManager.instance.UpdateLives(HP);
        UIManager.instance.UpdateWave(currentWave);
        UIManager.instance.UpdateEnemies(enemiesAlive);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UIManager.instance.UpdateCoins(coins);
    }

    public void LoseLife(int amount)
    {
        HP -= amount;
        UIManager.instance.UpdateLives(HP);

        if (HP <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

    public void SetWave(int wave)
    {
        currentWave = wave;
        UIManager.instance.UpdateWave(currentWave);
    }

    public void SetEnemiesAlive(int amount)
    {
        enemiesAlive = amount;
        UIManager.instance.UpdateEnemies(enemiesAlive);
    }
}
