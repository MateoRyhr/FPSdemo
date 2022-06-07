using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Boards : MonoBehaviour
{
    [SerializeField] private RectTransform BlueTeamPlayers;
    [SerializeField] private RectTransform RedTeamPlayers;
    [SerializeField] private PlayerRow PlayerRow;

    private List<PlayerGameData> bluePlayers = new List<PlayerGameData>();
    private List<PlayerGameData> redPlayers = new List<PlayerGameData>();

    public void InsertNewPlayer(PlayerGameData data){
        GameObject playerRow;
        if(data.team == Team.blue){
            playerRow = Instantiate(PlayerRow.gameObject,BlueTeamPlayers);
            bluePlayers.Add(data);
            data.boardPosition = bluePlayers.Count;
        }
        else{
            playerRow = Instantiate(PlayerRow.gameObject,RedTeamPlayers);
            redPlayers.Add(data);
            data.boardPosition = bluePlayers.Count;
        }
        playerRow.GetComponent<PlayerRow>().SetData(data);
        OrderPlayers();
    }

    public void OrderPlayers(){
        redPlayers.Sort(OrderByDeaths);
        bluePlayers.Sort(OrderByDeaths);
        bluePlayers.Sort(OrderByAssists);
        redPlayers.Sort(OrderByAssists);
        redPlayers.Sort(OrderByKills);
        bluePlayers.Sort(OrderByKills);

        //Assigning board positions
        for(int i = 0; i < redPlayers.Count; i++){
            redPlayers[i].boardPosition = i;
        }
        for(int i = 0; i < bluePlayers.Count; i++){
            bluePlayers[i].boardPosition = i;
        }

        PlayerRow[] BlueTeamPlayersRows = BlueTeamPlayers.GetComponentsInChildren<PlayerRow>();
        PlayerRow[] RedTeamPlayersRows = RedTeamPlayers.GetComponentsInChildren<PlayerRow>();

        //Assigning board positions to SiblingIndex
        for(int i = 0; i < BlueTeamPlayersRows.Length; i++){
            for(int j = 0; j < bluePlayers.Count; j++){
                if(BlueTeamPlayersRows[i].playerName.text == bluePlayers[j].playerName){
                    BlueTeamPlayersRows[i].transform.SetSiblingIndex(bluePlayers[j].boardPosition);
                    BlueTeamPlayersRows[i].SetData(bluePlayers[j]);
                }
            }
        }

        for(int i = 0; i < RedTeamPlayersRows.Length; i++){
            for(int j = 0; j < redPlayers.Count; j++){
                if(RedTeamPlayersRows[i].playerName.text == redPlayers[j].playerName){
                    RedTeamPlayersRows[i].transform.SetSiblingIndex(redPlayers[j].boardPosition);
                    RedTeamPlayersRows[i].SetData(redPlayers[j]);
                }
            }
        }
    }

    public void UpdatePlayer(PlayerGameData data){
        if(data.team == Team.blue){
            bluePlayers.Remove(bluePlayers[data.boardPosition]);
            bluePlayers.Insert(data.boardPosition,data);
        }else{
            redPlayers.Remove(redPlayers[data.boardPosition]);
            redPlayers.Insert(data.boardPosition,data);
        }
    }

    public void DeletePlayer(PlayerGameData data){
        if(data.team == Team.blue){
            Destroy(BlueTeamPlayers.GetChild(data.boardPosition).gameObject);
            bluePlayers.Remove(bluePlayers[data.boardPosition]);
        }
        else{
            Destroy(RedTeamPlayers.GetChild(data.boardPosition).gameObject);
            redPlayers.Remove(redPlayers[data.boardPosition]);
        }
    }

    public void CleanBoards(){
        for(int i = BlueTeamPlayers.childCount-1; i >= 0; i--){
            Destroy(BlueTeamPlayers.GetChild(i).gameObject);
            // bluePlayers.Remove(bluePlayers[i]);
        }
        for(int i = RedTeamPlayers.childCount-1; i >= 0; i--){
            Destroy(RedTeamPlayers.GetChild(i).gameObject);
            // redPlayers.Remove(bluePlayers[i]);
        }
        bluePlayers.Clear();
        redPlayers.Clear();
    }

    int OrderByDeaths(PlayerGameData x, PlayerGameData y){
        if(x.deaths == y.deaths) return 0;
        if(x.deaths > y.deaths) return 1;
        else return -1;
    }

    int OrderByAssists(PlayerGameData x, PlayerGameData y){
        if(x.assits == y.assits) return 0;
        if(x.assits > y.assits) return -1;
        else return 1;
    }

    int OrderByKills(PlayerGameData x, PlayerGameData y){
        if(x.kills == y.kills) return 0;
        if(x.kills > y.kills) return -1;
        else return 1;
    }
}
