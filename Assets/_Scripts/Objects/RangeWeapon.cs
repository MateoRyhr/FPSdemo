using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class RangeWeapon : Weapon
{
    public Transform ProjectileRespawnPosition;
    public UnityEvent OnShoot;
    public UnityEvent OnReload;
    public abstract void Shoot();
    public abstract void Reload();
}
