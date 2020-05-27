using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool IsGameOver;

    [SerializeField] private GameObject gameOverUI;

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
        gameOverUI.SetActive(true);
        Debug.Log("Game Over!");
    }
}
