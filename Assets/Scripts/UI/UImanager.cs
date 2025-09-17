using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI TowerPowerCoinsText;
    public TextMeshProUGUI enemiesText;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void UpdateWave(int wave)
    {
        if (waveText != null)
            waveText.text = $"Wave: {wave}";
    }

    public void UpdateLives(int lives)
    {
        if (HPText != null)
            HPText.text = $"Lives: {lives}";
    }

    public void UpdateCoins(int coins)
    {
        if (TowerPowerCoinsText != null)
            TowerPowerCoinsText.text = $"Coins: {coins}";
    }

    public void UpdateEnemies(int enemiesAlive)
    {
        if (enemiesText != null)
            enemiesText.text = $"Enemies: {enemiesAlive}";
    }
}
