using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "Player/PlayerGameData")]
public class PlayerGameData : MonoBehaviour
{
    public string playerName;
    public Team team;
    public bool isABot;
    // public int soldierN
    public int boardPosition { get; set; }
    public int kills { get; set; }
    public int deaths { get; set; }
    public int assits { get; set; }
    public int money { get; set; }
}

public enum Team{
    blue,
    red
}
