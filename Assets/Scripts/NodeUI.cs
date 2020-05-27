using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

   public GameObject ui;
   
   private Node target;
   public Button upgradeButton;
   
   public Text upgradeCost;
   public Text sellAmount;
   
   public void SetTarget(Node _target)
   {
      target = _target;

      transform.position = target.GetBuildPosition();

      if (!target.isUpgraded)
      {
         upgradeCost.text =  target.turretBlueprint.upgradeCost.ToString();
         upgradeButton.interactable = true;
      }

      sellAmount.text = target.turretBlueprint.GetSellAmount().ToString();

      ui.SetActive(true);
   }

   public void Hide()
   {
      ui.SetActive(false);
   }

   public void Upgrade()
   {
      if (target.UpgradeTurret())
      {
         upgradeCost.text = "DONE";
         upgradeButton.interactable = false;
      }
      BuildManager.instance.DeselectTurret();
   }

   public void Sell()
   {
      target.SellTurret();
      Hide();
      BuildManager.instance.DeselectTurret();
   }
   
}
