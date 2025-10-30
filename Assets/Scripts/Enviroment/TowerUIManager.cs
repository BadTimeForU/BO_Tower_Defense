using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
    public GameObject uiPanel;      
    public Button upgradeButton;    
    public Button closeButton;      

    private Tower selectedTower;

    void Start()
    {
        uiPanel.SetActive(false);

        upgradeButton.onClick.AddListener(UpgradeTower);
        closeButton.onClick.AddListener(CloseUI);
    }

    public void ShowUI(Tower tower)
    {
        selectedTower = tower;
        uiPanel.SetActive(true);
    }

    void UpgradeTower()
    {
        if (selectedTower != null)
        {
            selectedTower.UpgradeTower();
            CloseUI();
        }
    }

    void CloseUI()
    {
        uiPanel.SetActive(false);
        selectedTower = null;
    }
}