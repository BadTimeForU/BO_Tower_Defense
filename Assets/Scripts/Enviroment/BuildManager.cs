using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Meer dan 1 BuildManager in de scene!");
            return;
        }
        instance = this;
    }

    private GameObject towerToBuild;

    public void SetTowerToBuild(GameObject tower)
    {
        towerToBuild = tower;
    }

    public GameObject GetTowerToBuild()
    {
        return towerToBuild;
    }

    public void ClearTowerToBuild()
    {
        towerToBuild = null;
    }
}
