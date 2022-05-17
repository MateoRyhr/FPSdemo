using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public string weaponName;
    
    [Header("Shooting")]
    public float maxDistance;
    public float velocity;
    public float recoilForce;
    public float fireRate;                      //In Rounds per minute
    [Header("Reloading")] 
    // public int currentAmmo;
    public int magSize;
    public float recoilRecoveryTime;
    public float reloadTime;
    // public bool reloading;
}
