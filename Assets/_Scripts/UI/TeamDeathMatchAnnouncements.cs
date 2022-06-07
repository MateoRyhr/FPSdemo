using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamDeathMatchAnnouncements : MonoBehaviour
{
    [SerializeField] GameObject _gameModeAnnouncement;
    [SerializeField] GameObject _blueTeamWin;
    [SerializeField] GameObject _redTeamWin;
    [SerializeField] GameObject _gameEndBlueTeamWin;
    [SerializeField] GameObject _gameEndRedTeamWin;

    #region GameModeAnnouncement

    public void EnableGameModeAnnouncement(float delay){
        this.Invoke(() => _gameModeAnnouncement.SetActive(true),delay);
    }

    public void DisableGameModeAnnouncement(float delay){
        this.Invoke(() => _gameModeAnnouncement.SetActive(false),delay);
    }

    #endregion

    public void EnableBlueTeamWin(float delay){
        this.Invoke(() => _blueTeamWin.SetActive(true),delay);
    }

    public void DisableBlueTeamWin(float delay){
        this.Invoke(() => _blueTeamWin.SetActive(false),delay);
    }

    public void EnableRedTeamWin(float delay){
        this.Invoke(() => _redTeamWin.SetActive(true),delay);
    }

    public void DisableRedTeamWin(float delay){
        this.Invoke(() => _redTeamWin.SetActive(false),delay);
    }

    public void EnableGameEndBlueTeamWin(float delay){
        this.Invoke(() => _gameEndBlueTeamWin.SetActive(true),delay);
    }

    public void DisableGameEndBlueTeamWin(float delay){
        this.Invoke(() => _gameEndBlueTeamWin.SetActive(false),delay);
    }

    public void EnableGameEndRedTeamWin(float delay){
        this.Invoke(() => _gameEndRedTeamWin.SetActive(true),delay);
    }

    public void DisableGameEndRedTeamWin(float delay){
        this.Invoke(() => _gameEndRedTeamWin.SetActive(false),delay);
    }
}
