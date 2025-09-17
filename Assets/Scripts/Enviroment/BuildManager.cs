using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public TowerBlueprint selectedTower;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        selectedTower = tower;
    }

    public void BuildTower(TowerSpot spot)
    {
        if (selectedTower == null) return;

        
        GameObject towerGO = Instantiate(selectedTower.prefab, spot.transform.position, Quaternion.identity);
        GameManager.instance.AddCoins(-selectedTower.cost);

        
        spot.SetOccupied(true);
    }
}
