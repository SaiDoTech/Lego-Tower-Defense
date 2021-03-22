using UnityEngine;
using UnityEngine.UI;

public class UIState : MonoBehaviour
{
    public GameState gameState;

    public Button GenerateNewMapBt;
    public Button StartGameBt;
    public Button FinishGameBt;
    public Button PauseBt;

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

    public void OnFinishGame()
    {
        if (gameState.IsGameStarted)
        {
            FinishGameBt.interactable = false;
            StartGameBt.interactable = true;
            GenerateNewMapBt.interactable = true;
        }

        gameState.FinishGame();
    }

    public void OnPause()
    {
        if ((gameState.IsGameStarted) && (!gameState.IsGameOnPause))
        {


            gameState.SetGamePause();
        }
        else if ((gameState.IsGameStarted) && (gameState.IsGameOnPause))
        {


            gameState.EndGamePause();
        }
    }
}
