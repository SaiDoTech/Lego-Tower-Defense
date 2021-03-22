using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public bool IsGameStarted { get; private set; }
    public bool IsGameOnPause { get; private set; }

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
