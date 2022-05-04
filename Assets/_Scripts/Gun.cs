using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : RangeWeapon
{
    [Header("Weapon stats")]
    [SerializeField] GunData _gunData;
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _barrelEnd;
    [SerializeField] SightController sightData;
    private int _currentAmmo;
    private bool _reloading;

    float timeSinceLastShoot;

    private void Start() {
        _reloading = false;
        _currentAmmo = _gunData.magSize;
    }

    private void Update() {
        timeSinceLastShoot += Time.deltaTime;
    }

    public override void Shoot()
    {
        if(_currentAmmo > 0){
            if(CanShoot()){
                ShootABullet();
                OnShoot?.Invoke();
            }
        }
    }

    public override void Reload()
    {
        if(CanReload()){
            _reloading = true;
            OnReload?.Invoke();
            this.Invoke(() => {
                _reloading = false;
                _currentAmmo = _gunData.magSize;
            },_gunData.reloadTime);
        }
    }

    private bool CanShoot() => !_reloading && timeSinceLastShoot > 1f / (_gunData.fireRate / 60f);
    private bool CanReload() => _currentAmmo < _gunData.magSize;

    void ShootABullet(){
        GameObject bullet = Instantiate(_bullet,_barrelEnd.position, Quaternion.LookRotation(sightData.SightPoint,Vector3.up));
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().AddForce(_bullet.transform.forward * _gunData.velocity,ForceMode.Impulse);
        _currentAmmo--;
        timeSinceLastShoot = 0f;
    }
}
