using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LiveValue/GameState")]
public class GameStateVariable : ScriptableObject
{
    public GameState state;

    private void OnEnable()
    {
        state = GameState.PreGame;
    }

    public void ChangeStateTo(GameState stateVal)
    {
        state = stateVal;
    }
}

public enum GameState
{
    PreGame,
    InGame,
    GameSuccess,
    GameLose
}
