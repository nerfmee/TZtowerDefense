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
 


    public GameObject standardTurretPrefab;
    public GameObject missileLauncherPrefab;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;
    
    public bool CanBuild { get { return turretToBuild != null;} }
    public bool HasMoney{ get { return PlayerStats.Money >= turretToBuild.cost;} }

    public GameObject DeselectTurret()
    {
        return null;
    }


    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        
        selectedNode = node;
        turretToBuild = null;
        
        nodeUI.SetTarget(node);
    }

    private void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
    
}
