using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public string weaponName;
    
    [Header("Shooting")]
    public float damage;
    public float maxDistance;
    public float velocity;
    [Header("Reloading")] 
    // public int currentAmmo;
    public int magSize;
    public float fireRate;                      //In Rounds per minute
    public float reloadTime;
    // public bool reloading;
}
