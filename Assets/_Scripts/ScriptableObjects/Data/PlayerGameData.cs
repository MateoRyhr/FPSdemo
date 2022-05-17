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
    public int kills;
    public int deaths;
    public int assits;
}

public enum Team{
    blue,
    red
}
