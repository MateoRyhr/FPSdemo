using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData")]
[Serializable]
public class CreateGameData : ScriptableObject
{
    public int redTeamBotsQuantity;
    public int blueTeamBotsQuantity;
    public int totalRounds;
    public int map;
    public float buyTime;
    public bool inGame = false;
    // public GameMode gameMode;
}

// public enum GameMode
// {
//     TeamDeatmach
// }