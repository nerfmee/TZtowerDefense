using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if(instance !=null)
        {
            Debug.LogError("More than one BuildManager is scene");
            return;
        }
        instance = this;
    }
 


    public GameObject StandardTurretPrefab;
    public GameObject FastTurretPrefab;

    private TurretBlueprint _turretToBuild;
    private Node _selectedNode;

   [SerializeField] private NodeUI _nodeUI;
    
    public bool CanBuild { get { return _turretToBuild != null;} }
    public bool HasMoney{ get { return PlayerStats.Money >= _turretToBuild.cost;} }

    public GameObject DeselectTurret()
    {
        return null;
    }


    public void SelectNode(Node node)
    {
        if (_selectedNode == node)
        {
            DeselectNode();
            return;
        }
        
        _selectedNode = node;
        _turretToBuild = null;
        
        _nodeUI.SetTarget(node);
    }

    private void DeselectNode()
    {
        _selectedNode = null;
        _nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        _turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return _turretToBuild;
    }
    
}
