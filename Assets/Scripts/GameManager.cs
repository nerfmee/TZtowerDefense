using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool IsGameOver;

    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private Text _killStatText;

    private void Start()
    {
        IsGameOver = false;
    }
    private void Update()
    {
        if (IsGameOver)
            return;
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        IsGameOver = true;
        _gameOverUI.SetActive(true);
        _killStatText.text = ("You kill: " + PlayerStats.KillStat + " enemies");
        Debug.Log("Game Over!");
    }
}
