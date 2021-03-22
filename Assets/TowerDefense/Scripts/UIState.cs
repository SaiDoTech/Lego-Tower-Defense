using UnityEngine;
using UnityEngine.UI;

public class UIState : MonoBehaviour
{
    public GameState gameState;

    public Button GenerateNewMapBt;
    public Button StartGameBt;
    public Button FinishGameBt;

    private void Start()
    {
        FinishGameBt.interactable = false;
    }

    public void OnGenerateNewMap()
    {
        gameState.CreateNewMap();
    }

    public void OnStartGame()
    {
        if (!gameState.IsGameStarted)
        {
            GenerateNewMapBt.interactable = false;
            FinishGameBt.interactable = true;
        }

        gameState.StartGame();
    }
}
