using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameMode : MonoBehaviour
{
    public UnityEvent OnGameStart;
    public UnityEvent OnGameEnd;
    public UnityEvent OnRoundStart;
    public UnityEvent OnRoundEnd;
    public GameModeType gameModeType;
    public string GameModeName;
    public abstract void StartGame();
    public abstract void EndGame();
    public abstract void StartRound();
    public abstract void EndRound();
}

public enum GameModeType{
    TeamDeatmach
}
