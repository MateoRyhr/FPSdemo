using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public CreateGameData CreateGameData;
    [SerializeField] ListOfNames blueTeamNames;
    [SerializeField] ListOfNames redTeamNames;
    [SerializeField] GameObject _unitsContainer;

    [SerializeField] GameObject _redTeamPlayer;
    [SerializeField] GameObject _blueTeamPlayer;
    [SerializeField] GameObject _redTeamBot;
    [SerializeField] GameObject _blueTeamBot;

    private void Start() {
        blueTeamNames.ResetValues();
        redTeamNames.ResetValues();
    }

    public void RespawnUnits(){
        MapData mapData = GameManager.Instance.Maps[CreateGameData.map];
        //Players spawn in your own, if the match has been started, they wait for the end to respawn

        for(int i = 0; i < CreateGameData.blueTeamBotsQuantity; i++){
            GameObject bot = Instantiate(_blueTeamBot,mapData.blueTeamRespawnPoint,Quaternion.Euler(0,0,0));
            bot.transform.parent = _unitsContainer.transform;
            bot.GetComponent<PlayerGameData>().team = Team.blue;
            bot.GetComponent<PlayerGameData>().playerName = blueTeamNames.GetName();
            UIManager.Instance.GetComponent<Boards>().InsertNewPlayer(bot.GetComponent<PlayerGameData>());
            GameData.BlueTeamMembers++;
        }
        for(int i = 0; i < CreateGameData.redTeamBotsQuantity; i++){
            GameObject bot = Instantiate(_redTeamBot,mapData.redTeamRespawnPoint,Quaternion.Euler(0,0,0));
            bot.transform.parent = _unitsContainer.transform;
            bot.GetComponent<PlayerGameData>().team = Team.red;
            bot.GetComponent<PlayerGameData>().playerName = redTeamNames.GetName();
            UIManager.Instance.GetComponent<Boards>().InsertNewPlayer(bot.GetComponent<PlayerGameData>());
            GameData.RedTeamMembers++;
        }
        PositionPlayers();
    }

    public void RespawnRedPlayer(){
        MapData mapData = GameManager.Instance.Maps[CreateGameData.map];        
        GameObject player = Instantiate(_redTeamPlayer,mapData.redTeamRespawnPoint,Quaternion.Euler(0,0,0));
        player.transform.SetParent(_unitsContainer.transform);
        player.GetComponent<PlayerGameData>().team = Team.red;
        player.GetComponent<PlayerGameData>().playerName = GameManager.Instance.playerNickname;
        UIManager.Instance.GetComponent<Boards>().InsertNewPlayer(player.GetComponent<PlayerGameData>());
        GameData.RedTeamMembers++;
        // HUD HUD = UIManager.Instance.HUD.GetComponent<HUD>();
        // HUD.UnitHealth = player.GetComponent<UnitHealth>();
        // HUD.CurrentWeapon = player.GetComponent<UnitMultipleWeapons>().CurrentWeaponContainer;
    }

    public void RespawnBluePlayer(){
        MapData mapData = GameManager.Instance.Maps[CreateGameData.map];
        GameObject player = Instantiate(_blueTeamPlayer,mapData.blueTeamRespawnPoint,Quaternion.Euler(0,0,0));
        player.transform.SetParent(_unitsContainer.transform);
        player.GetComponent<PlayerGameData>().team = Team.blue;
        player.GetComponent<PlayerGameData>().playerName = GameManager.Instance.playerNickname;
        UIManager.Instance.GetComponent<Boards>().InsertNewPlayer(player.GetComponent<PlayerGameData>());
        GameData.BlueTeamMembers++;
        // HUD HUD = UIManager.Instance.HUD.GetComponent<HUD>();
        // HUD.UnitHealth = player.GetComponent<UnitHealth>();
        // HUD.CurrentWeapon = player.GetComponent<UnitMultipleWeapons>().CurrentWeaponContainer;
    }

    public void ResetUnits(){
        MapData mapData = GameManager.Instance.Maps[CreateGameData.map];
        for(int i = 0; i < _unitsContainer.transform.childCount; i++){
            Transform unit = _unitsContainer.transform.GetChild(i);
            Team unitTeam = unit.GetComponent<PlayerGameData>().team;
            Vector3 unitPosition = unitTeam == Team.blue ? mapData.blueTeamRespawnPoint : mapData.redTeamRespawnPoint;
            if(unit.GetComponent<UnitHealth>().HasBeenDestructed){
                GameObject newUnit;
                if(unit.GetComponent<PlayerGameData>().isABot){
                    if(unitTeam == Team.blue) newUnit = Instantiate(_blueTeamBot,unitPosition,Quaternion.Euler(0,0,0));
                    else newUnit = Instantiate(_redTeamBot,unitPosition,Quaternion.Euler(0,0,0));
                }else{
                    if(unitTeam == Team.blue) newUnit = Instantiate(_blueTeamPlayer,unitPosition,Quaternion.Euler(0,0,0));
                    else newUnit = Instantiate(_redTeamPlayer,unitPosition,Quaternion.Euler(0,0,0));
                }
                
                PlayerGameData oldData = unit.GetComponent<PlayerGameData>();
                PlayerGameData newUnitData = newUnit.GetComponent<PlayerGameData>();

                TransferPlayerData(newUnitData,oldData);
                UIManager.Instance.GetComponent<Boards>().UpdatePlayer(newUnitData);

                // UIManager.Instance.GetComponent<Boards>().DeletePlayer(oldData);
                // UIManager.Instance.GetComponent<Boards>().InsertNewPlayer(newUnitData);

                newUnit.transform.SetParent(_unitsContainer.transform);
                Destroy(unit.gameObject);
                if(unitTeam == Team.blue) GameData.BlueTeamMembers++;
                else GameData.RedTeamMembers++;
                // Instantiate(unit.gameObject,unitPosition,Quaternion.Euler(0,0,0));
            }else{
                unit.GetComponent<UnitHealth>().ResetHealth();
                unit.GetComponentInChildren<HUD>().UpdateHealth();
                unit.GetComponentInChildren<HUD>().UpdateAmmo();
                unit.position = unitPosition;
            }
        }
        PositionPlayers();
        UIManager.Instance.GetComponent<Boards>().OrderPlayers();
    }

    private void TransferPlayerData(PlayerGameData newData, PlayerGameData oldData){
        newData.playerName = oldData.playerName;
        newData.kills = oldData.kills;
        newData.assits = oldData.assits;
        newData.deaths = oldData.deaths;
        newData.boardPosition = oldData.boardPosition;
        newData.money = oldData.money;
    }

    public void RemoveUnits(){
        for(int i = 0; i < _unitsContainer.transform.childCount; i++){
            Destroy(_unitsContainer.transform.GetChild(i).gameObject);
        }
    }

    public void PositionPlayers(){
        MapData mapData = GameManager.Instance.Maps[CreateGameData.map];

        List<Transform> bluePlayers = new List<Transform>();
        List<Transform> redPlayers = new List<Transform>();

        //Divide the players in two lists
        for(int i = 0; i < _unitsContainer.transform.childCount; i++){
            if(_unitsContainer.transform.GetChild(i).GetComponent<PlayerGameData>().team == Team.blue){
                bluePlayers.Add(_unitsContainer.transform.GetChild(i));
            }else{
                redPlayers.Add(_unitsContainer.transform.GetChild(i));
            }
        }

        for (int i = 0; i < bluePlayers.Count; i++){
            bluePlayers[i].position = new Vector3(
                mapData.blueTeamRespawnPoint.x + Mathf.Cos((360/4 * i) * Mathf.Deg2Rad) * 2,
                mapData.blueTeamRespawnPoint.y,
                mapData.blueTeamRespawnPoint.z + Mathf.Sin((360/4 * i) * Mathf.Deg2Rad) * 2
            );
        }
        for (int i = 0; i < redPlayers.Count; i++){
            redPlayers[i].position = new Vector3(
                mapData.redTeamRespawnPoint.x + Mathf.Cos((360/4 * i) * Mathf.Deg2Rad) * 2,
                mapData.redTeamRespawnPoint.y,
                mapData.redTeamRespawnPoint.z + Mathf.Sin((360/4 * i) * Mathf.Deg2Rad) * 2
            );
        }
    }
}
