using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamDeathmatch : GameMode
{
    // [SerializeField] FloatVariable buyTime;
    [SerializeField] UnitsManager _unitManager;
    [SerializeField] CreateGameData CreateGameData;
    private int blueTeamRoundsWon;
    private int redTeamRoundsWon;
    private int currentRound;

    public override void StartGame()
    {
        currentRound = 1;
        OnGameStart?.Invoke();
        _unitManager.RespawnUnits();
        StartRound();
        // GameData.inGame = true;
    }

    public override void EndGame()
    {
        Debug.Log("Game end");
        OnGameEnd?.Invoke();
        // GameData.inGame = false;
    }

    public override void StartRound()
    {
        Debug.Log("Start round " + currentRound);
        GameData.InGame = true;
        _unitManager.ResetUnits();
        GameManager.Instance.FreezeTeams();
        this.Invoke(() => GameManager.Instance.UnfreezeTeams(),CreateGameData.buyTime);
        OnRoundStart?.Invoke();
    }

    public override void EndRound()
    {
        Debug.Log("Round " + currentRound + " end");
        currentRound++;
        GameData.InGame = false;
        if(GameEnd()){
            EndGame();
        }else{
            this.Invoke(() => StartRound(),5f);
        }
        OnRoundEnd?.Invoke();
    }

    private void Update() {
        if(GameData.InGame){
            if(TeamEliminated()){
                EndRound();
            }
        }
    }

    bool TeamEliminated() => GameData.BlueTeamMembers == 0 || GameData.RedTeamMembers == 0;
    bool GameEnd() => currentRound >= CreateGameData.totalRounds;
}
