using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeamDeathmatch : GameMode
{
    // [SerializeField] FloatVariable buyTime;
    [SerializeField] UnitsManager _unitManager;
    [SerializeField] CreateGameData CreateGameData;

    public UnityEvent OnBlueTeamWinARound;
    public UnityEvent OnRedTeamWinARound;
    public UnityEvent OnBlueTeamWin;
    public UnityEvent OnRedTeamWin;

    private int blueTeamRoundsWon = 0;
    private int redTeamRoundsWon = 0;
    private int currentRound = 0;

    private void Update() {
        if(GameData.InGame){
            if(TeamEliminated()){
                EndRound();
            }
        }
    }

    public override void StartGame()
    {
        GameData.ResetMembersCount();
        GameManager.Instance.UpdateState(GameState.SelectTeamMenu);
        blueTeamRoundsWon = 0;
        redTeamRoundsWon = 0;
        currentRound = 0;
        OnGameStart?.Invoke();
        // UIManager.Instance.GetComponent<Boards>().CleanBoards();
        _unitManager.RespawnUnits();
        StartRound();
    }

    public override void StartRound()
    {
        currentRound++;
        GameData.InGame = true;
        _unitManager.ResetUnits();
        GameManager.Instance.FreezeTeams();
        this.Invoke(() => GameManager.Instance.UnfreezeTeams(),CreateGameData.buyTime);
        OnRoundStart?.Invoke();
    }

    public override void EndRound()
    {
        GameData.InGame = false;
        if(RedTeamLoose()){
            blueTeamRoundsWon++;
            if(!GameEnd()) OnBlueTeamWinARound?.Invoke();
        }else{
            redTeamRoundsWon++;
            if(!GameEnd()) OnRedTeamWinARound?.Invoke();
        }
        if(GameEnd()){
            EndGame();
        }else{
            this.Invoke(() => StartRound(),5f);
        }
        if(!GameEnd()) OnRoundEnd?.Invoke();
    }

    public override void EndGame()
    {
        if(RedTeamWinGame()){
            // Debug.Log("Gano el equipo rojo");
            OnRedTeamWin?.Invoke();
        }else{
            // Debug.Log("Gano el equipo azul");
            OnBlueTeamWin?.Invoke();
        }
        OnGameEnd?.Invoke();
        this.Invoke(() => _unitManager.RemoveUnits(),4.9f);
        this.Invoke(() => GameManager.Instance.NewGameRandomMap(),5f);
    }

    bool TeamEliminated() => GameData.BlueTeamMembers == 0 || GameData.RedTeamMembers == 0;
    bool RedTeamLoose() => GameData.RedTeamMembers == 0;
    bool RedTeamWinGame() => redTeamRoundsWon == CreateGameData.totalRounds;
    bool GameEnd() => currentRound == CreateGameData.totalRounds;
}
