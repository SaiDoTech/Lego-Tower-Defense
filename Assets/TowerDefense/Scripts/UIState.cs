using UnityEngine;
using UnityEngine.UI;

public class UIState : MonoBehaviour
{
    public GameState gameState;
    public Button GenerateNewMapBt;
    public Button StartGameBt;
    public Button FinishGameBt;
    public Button PauseBt;

    public Sprite PauseSpr;
    public Sprite PlaySpr;
    private GameObject pausePanel;

    private void Start()
    {
        FinishGameBt.interactable = false;

        pausePanel = PauseBt.transform.GetChild(0).gameObject;
        pausePanel.GetComponent<Image>().sprite = PauseSpr;
    }

    public void OnGenerateNewMap()
    {
        gameState.CreateNewMap();
    }

    public void OnStartGame()
    {
        if (!GameState.IsGameStarted)
        {
            GenerateNewMapBt.interactable = false;
            StartGameBt.interactable = false;
            FinishGameBt.interactable = true;
        }

        gameState.StartGame();
    }

    public void OnFinishGame()
    {
        if (GameState.IsGameStarted)
        {
            FinishGameBt.interactable = false;
            StartGameBt.interactable = true;
            GenerateNewMapBt.interactable = true;
        }

        gameState.FinishGame();
    }

    public void OnPause()
    {
        if ((GameState.IsGameStarted) && (!GameState.IsGameOnPause))
        {
            pausePanel.GetComponent<Image>().sprite = PlaySpr;

            gameState.SetGamePause();
        }
        else if ((GameState.IsGameStarted) && (GameState.IsGameOnPause))
        {
            pausePanel.GetComponent<Image>().sprite = PauseSpr;

            gameState.EndGamePause();
        }
    }

    public void OnToolClick()
    {

    }
}
