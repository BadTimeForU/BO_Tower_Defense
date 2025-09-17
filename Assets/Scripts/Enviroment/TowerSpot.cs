using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    private bool isOccupied = false;

    void OnMouseDown()
    {
        if (!isOccupied && GameManager.instance.coins >= BuildManager.instance.selectedTower.cost)
        {
            BuildManager.instance.BuildTower(this);
        }
    }

    public void SetOccupied(bool value)
    {
        isOccupied = value;
    }
}
