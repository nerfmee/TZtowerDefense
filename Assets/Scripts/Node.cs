using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color HoverColor;
    public Vector3 PositionOffset;

    [HideInInspector] public GameObject turret;
    [HideInInspector] public TurretBlueprint turretBlueprint;
    [HideInInspector] public bool isUpgraded = false;
    private BuildManager _buildManager;
    private Renderer _renderer;
    private Color _startColor;
    public Color NotEnoughMoneyColor;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _startColor = _renderer.material.color;

        _buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + PositionOffset;
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
            _buildManager.SelectNode(this);
            return;
        }

        if (!_buildManager.CanBuild)
            return;

        BuildTurret(_buildManager.GetTurretToBuild());
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!_buildManager.CanBuild)
            return;
        if (_buildManager.HasMoney)
            _renderer.material.color = HoverColor;
        else
            _renderer.material.color = NotEnoughMoneyColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _startColor;
    }
}
