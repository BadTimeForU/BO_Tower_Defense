using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Button upgradeButton;
    private Tower targetTower;

    void Start()
    {
        gameObject.SetActive(false);
        upgradeButton.onClick.AddListener(OnUpgradeClick);
    }

    public void SetTargetTower(Tower tower)
    {
        targetTower = tower;
    }

    void OnUpgradeClick()
    {
        if (targetTower != null)
            targetTower.UpgradeTower();
    }
}