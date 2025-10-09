using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    [Header("Tower Settings")]
    public bool flipX = false;  

    private GameObject currentTower;

    private void OnMouseDown()
    {
        GameObject towerToBuild = Shop.selectedTowerPrefab;

        if (towerToBuild == null)
        {
            Debug.Log("Geen toren geselecteerd!");
            return;
        }

        if (currentTower != null)
        {
            Debug.Log("Hier staat al een toren!");
            return;
        }

        
        currentTower = Instantiate(towerToBuild, transform.position, transform.rotation);

        
        currentTower.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

       
        if (flipX)
        {
            Vector3 localScale = currentTower.transform.localScale;
            localScale.x *= -1;
            currentTower.transform.localScale = localScale;
        }

        Debug.Log($"Toren geplaatst: {currentTower.name} (FlipX: {flipX})");

        
        Shop.selectedTowerPrefab = null;
    }
}
