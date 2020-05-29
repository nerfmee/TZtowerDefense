using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint fastTurret;
    
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret()
    {
        Debug.Log("Обычная турель выбрана");
        buildManager.SelectTurretToBuild(standardTurret);
    }
    public void SelectFustTurret()
    {
        Debug.Log("Быстрая турель выбрана");
        buildManager.SelectTurretToBuild(fastTurret);
    }



}
