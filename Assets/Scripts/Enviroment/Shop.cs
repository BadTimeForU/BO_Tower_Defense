using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueprint ZwaardPatron;
    public TowerBlueprint WarriorPatron;
    public TowerBlueprint BoogPatron;
    public TowerBlueprint wizardPatron;

    public void SelectGoonPatron()
    {
        BuildManager.instance.SelectTowerToBuild(ZwaardPatron);
    }

    public void SelectEdgePatron()
    {
        BuildManager.instance.SelectTowerToBuild(WarriorPatron);
    }

    public void SelectDihPatron()
    {
        BuildManager.instance.SelectTowerToBuild(BoogPatron);
    }

    public void SelectWizardPatron()
    {
        BuildManager.instance.SelectTowerToBuild(wizardPatron);
    }
}
