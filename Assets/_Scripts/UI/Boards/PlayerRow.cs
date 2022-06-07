using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerRow : MonoBehaviour
{
    public TextMeshProUGUI playerPosition;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerKills;
    public TextMeshProUGUI playerDeaths;
    public TextMeshProUGUI playerAssists;

    public void SetData(PlayerGameData data){
        playerPosition.text = (data.boardPosition + 1).ToString();
        playerName.text = data.playerName;
        playerKills.text = data.kills.ToString();
        playerDeaths.text = data.deaths.ToString();
        playerAssists.text = data.assits.ToString();
    }
}
