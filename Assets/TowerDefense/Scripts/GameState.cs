using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static bool IsGameStarted;
    public static bool IsGameOnPause;
    public static bool IsBuildModActive;

    private static MapCreator mapCreator;

    private void Start()
    {
        mapCreator = gameObject.GetComponent<MapCreator>();
    }

    public void CreateNewMap()
    {
        if (!IsGameStarted)
        {
            mapCreator.CreateNewMap();
        }
    }

    public void FinishGame()
    {
        if (IsGameStarted)
        {
            IsGameStarted = false;
            IsGameOnPause = false;

            SceneManager.LoadScene(0);
        }
    }

    public void StartGame()
    {
        if (!IsGameStarted)
        {
            // start game
            IsGameStarted = true;
        }
    }

    public void SetGamePause()
    {
        if ((!IsGameOnPause) && (IsGameStarted))
        {
            // pause game
            IsGameOnPause = true;
        }
    }

    public void EndGamePause()
    {
        if ((IsGameOnPause) && (IsGameStarted))
        {
            // end pause
            IsGameOnPause = false;
        }
    }
}
