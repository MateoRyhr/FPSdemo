using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public CreateGameData CreateGameData;
    // public IntVariable blueTeamMembers;
    // public IntVariable redTeamMembers;
    [SerializeField] GameObject _unitsContainer;
    [SerializeField] GameObject _redTeamPlayer;
    [SerializeField] GameObject _blueTeamPlayer;
    [SerializeField] GameObject _redTeamBot;
    [SerializeField] GameObject _blueTeamBot;

    public void RespawnUnits(){
        MapData mapData = GameManager.Instance.Maps[CreateGameData.map];
        //Players spawn in your own, if the match has been started, they wait for the end to respawn

        for(int i = 0; i < CreateGameData.blueTeamBotsQuantity; i++){
            GameObject bot = Instantiate(_blueTeamBot,mapData.blueTeamRespawnPoint,Quaternion.Euler(0,0,0));
            bot.transform.parent = _unitsContainer.transform;
            bot.GetComponent<PlayerGameData>().team = Team.blue;
            GameData.BlueTeamMembers++;
        }
        for(int i = 0; i < CreateGameData.redTeamBotsQuantity; i++){
            GameObject bot = Instantiate(_redTeamBot,mapData.redTeamRespawnPoint,Quaternion.Euler(0,0,0));
            bot.transform.parent = _unitsContainer.transform;
            bot.GetComponent<PlayerGameData>().team = Team.red;
            GameData.RedTeamMembers++;
        }
    }

    public void RespawnRedPlayer(){
        MapData mapData = GameManager.Instance.Maps[CreateGameData.map];        
        GameObject player = Instantiate(_redTeamPlayer,mapData.redTeamRespawnPoint,Quaternion.Euler(0,0,0));
        player.transform.SetParent(_unitsContainer.transform);
        player.GetComponent<PlayerGameData>().team = Team.red;
        GameData.RedTeamMembers++;
        HUD HUD = UIManager.Instance.HUD.GetComponent<HUD>();
        HUD.UnitHealth = player.GetComponent<UnitHealth>();
        HUD.CurrentWeapon = player.GetComponent<UnitMultipleWeapons>().CurrentWeaponContainer;
    }

    public void RespawnBluePlayer(){
        MapData mapData = GameManager.Instance.Maps[CreateGameData.map];
        GameObject player = Instantiate(_blueTeamPlayer,mapData.blueTeamRespawnPoint,Quaternion.Euler(0,0,0));
        player.transform.SetParent(_unitsContainer.transform);
        player.GetComponent<PlayerGameData>().team = Team.blue;
        GameData.BlueTeamMembers++;
        HUD HUD = UIManager.Instance.HUD.GetComponent<HUD>();
        HUD.UnitHealth = player.GetComponent<UnitHealth>();
        HUD.CurrentWeapon = player.GetComponent<UnitMultipleWeapons>().CurrentWeaponContainer;
    }

    public void ResetUnits(){
        MapData mapData = GameManager.Instance.Maps[CreateGameData.map];
        for(int i = 0; i < _unitsContainer.transform.childCount; i++){
            Transform unit = _unitsContainer.transform.GetChild(i);
            Team unitTeam = unit.GetComponent<PlayerGameData>().team;
            Vector3 unitPosition = unitTeam == Team.blue ? mapData.blueTeamRespawnPoint : mapData.redTeamRespawnPoint;
            if(unit.GetComponent<UnitHealth>().HasBeenDestructed){
                if(unit.GetComponent<PlayerGameData>().isABot){
                    if(unitTeam == Team.blue)   Instantiate(_blueTeamBot,unitPosition,Quaternion.Euler(0,0,0));
                    else Instantiate(_redTeamBot,unitPosition,Quaternion.Euler(0,0,0));
                }else{
                    if(unitTeam == Team.blue)   Instantiate(_blueTeamPlayer,unitPosition,Quaternion.Euler(0,0,0));
                    else Instantiate(_redTeamPlayer,unitPosition,Quaternion.Euler(0,0,0));
                }
                Destroy(unit.gameObject);
                if(unitTeam == Team.blue) GameData.BlueTeamMembers++;
                else GameData.RedTeamMembers++;
                // Instantiate(unit.gameObject,unitPosition,Quaternion.Euler(0,0,0));
            }else{
                unit.GetComponent<UnitHealth>().ResetHealth();
                unit.position = unitPosition;
            }
        }
    }

    public void RemoveUnits(){
        for(int i = 0; i < _unitsContainer.transform.childCount; i++){
            Destroy(_unitsContainer.transform.GetChild(i).gameObject);
        }
    }
}
