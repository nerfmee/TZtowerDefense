using UnityEngine;
using UnityEngine.UI;

public class GoldUi : MonoBehaviour
{

    public Text MoneyText;
    void Update()
    {
        MoneyText.text ="Money: " + PlayerStats.Money.ToString();
    }
}
