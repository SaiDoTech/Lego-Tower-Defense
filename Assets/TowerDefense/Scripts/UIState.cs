using UnityEngine;
using UnityEngine.UI;

public class UIState : MonoBehaviour
{
    public GameState gameState;
    public Button GenerateNewMapBt;
    public Button StartGameBt;
    public Button FinishGameBt;
    public Button PauseBt;

    public Button Tool1;
    public Button Tool2;
    public Button Tool3;

    public Sprite PauseSpr;
    public Sprite PlaySpr;
    private GameObject pausePanel;

    private void Start()
    {
        FinishGameBt.interactable = false;

        pausePanel = PauseBt.transform.GetChild(0).gameObject;
        pausePanel.GetComponent<Image>().sprite = PauseSpr;
    }

    private void Update()
    {
        if (GameState.IsBuildModActive)
        {
            SetTolBtInteractable(false);
        }
        else
        {
            SetTolBtInteractable(true);
        }
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

    public void SetToolBt()
    {
        GameState.IsBuildModActive = true;
    }

    private void SetTolBtInteractable(bool value)
    {
        Tool1.interactable = value;
        Tool2.interactable = value;
        Tool3.interactable = value;
    }
}
