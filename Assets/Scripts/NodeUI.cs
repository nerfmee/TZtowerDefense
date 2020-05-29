using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
   public GameObject UI;
   public Button UpgradeButton;
   public Text UpgradeCost;
   public Text SellAmount;

   private Node _target;

 
   public void SetTarget(Node target)
   {
      _target = target;

      transform.position = _target.GetBuildPosition();

      if (!_target.isUpgraded)
      {
         UpgradeCost.text =  _target.turretBlueprint.upgradeCost.ToString();
         UpgradeButton.interactable = true;
      }

      SellAmount.text = _target.turretBlueprint.GetSellAmount().ToString();

      UI.SetActive(true);
   }

   public void Hide()
   {
      UI.SetActive(false);
   }

   public void Upgrade()
   {
      if (_target.UpgradeTurret())
      {
         UpgradeCost.text = "DONE";
         UpgradeButton.interactable = false;
      }
      BuildManager.instance.DeselectTurret();
   }

   public void Sell()
   {
      _target.SellTurret();
      Hide();
      BuildManager.instance.DeselectTurret();
   }
   
}
