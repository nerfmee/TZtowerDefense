using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    [HideInInspector] public GameObject turret;
    [HideInInspector] public TurretBlueprint turretBlueprint;
    [HideInInspector] public bool isUpgraded = false;
    BuildManager buildManager;
    private Renderer rend;
    private Color startColor;
    public Color notEnoughMoneyColor;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    
    private void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Не хватает денег чтобы построить!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = (GameObject) Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;
        
      
        Debug.Log("Турель построена! Денег осталось: " + PlayerStats.Money);
    }

    public bool UpgradeTurret()
    {
        bool isCanUpgraded = true;
        
        
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Не хватает денег чтобы улучшить!");
            return !isCanUpgraded;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;
        //Убираем старую турель
        Destroy(turret);
        //Строим новую 
        
        GameObject _turret = (GameObject) Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        isUpgraded = true;
        Debug.Log("Турель улучшена!" + PlayerStats.Money);
        return isCanUpgraded;
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        Destroy(turret);
        turretBlueprint = null;
    }
    
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;

        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
