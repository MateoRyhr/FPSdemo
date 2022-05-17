using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Map/MapData")]
public class MapData : ScriptableObject
{
    public string mapName;
    public Vector3 blueTeamRespawnPoint;
    public Vector3 redTeamRespawnPoint;
}
