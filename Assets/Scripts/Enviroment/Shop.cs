using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Tower Prefabs")]
    public GameObject ZwaardPatron;
    public GameObject WarriorPatron;
    public GameObject BoogPatron;
    public GameObject WizardPatron;

    public static GameObject selectedTowerPrefab;

    [Header("Buttons")]
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    void Start()
    {
        button1.onClick.AddListener(() => SelectTower(ZwaardPatron));
        button2.onClick.AddListener(() => SelectTower(WarriorPatron));
        button3.onClick.AddListener(() => SelectTower(BoogPatron));
        button4.onClick.AddListener(() => SelectTower(WizardPatron));
    }

    void SelectTower(GameObject towerPrefab)
    {
        selectedTowerPrefab = towerPrefab;
        Debug.Log("Tower geselecteerd: " + towerPrefab.name);
    }
}
