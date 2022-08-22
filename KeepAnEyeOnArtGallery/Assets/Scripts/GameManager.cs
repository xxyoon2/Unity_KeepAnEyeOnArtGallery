using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public bool IsGameOver { get; private set; }

    public void EndGame()
    {
        IsGameOver = true;
    }
}
