using UnityEngine;

[CreateAssetMenu(fileName = "NewTowerBlueprint", menuName = "TowerDefense/TowerBlueprint")]
public class TowerBlueprint : ScriptableObject
{
    public GameObject prefab;     
    public int cost = 10;

    
    public GameObject upgradedPrefab;
    public int upgradeCost;
}
