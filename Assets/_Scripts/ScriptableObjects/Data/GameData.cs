using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static int BlueTeamMembers = 0;
    public static int RedTeamMembers = 0;
    public static bool InGame = false;
    
    public static void AddBlueMember() => BlueTeamMembers++;
    public static void RemoveBlueMember() => BlueTeamMembers--;
    public static void AddRedMember() => RedTeamMembers++;
    public static void RemoveRedMember() => RedTeamMembers--;

    public static void ResetMembersCount(){
        BlueTeamMembers = 0;
        RedTeamMembers = 0;
    }
}
