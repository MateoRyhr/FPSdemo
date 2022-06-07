using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : RangeWeapon
{
    [Header("Weapon stats")]
    [SerializeField] GameObject _bullet;
    [SerializeField] FloatVariable sightMinimumRadius;
    [SerializeField] FloatVariable sightMaximumRadius;

    public Camera Camera;
    Vector3 _bulletDirection;

    private PlayerGameData playerData;

    [HideInInspector] public bool _reloading;

    float timeSinceLastShoot;

    private void Awake() {
        CurrentAmmo = RangeWeaponData.magSize;
    }

    private void Start() {
        playerData = GetComponentInParent<PlayerGameData>();
        _reloading = false;
    }

    private void Update() {
        timeSinceLastShoot += Time.deltaTime;
    }

    public override void Shoot()
    {
        if(CurrentAmmo > 0){
            if(CanShoot()){
                ShootABullet();
                OnShoot?.Invoke();
                GetComponentInParent<CurrentWeapon>().OnShoot?.Invoke();
            }
        }
    }

    public override void Reload()
    {
        if(CanReload()){
            _reloading = true;
            OnReload?.Invoke();
            GetComponentInParent<CurrentWeapon>().OnReloadStart?.Invoke();
            this.Invoke(() => {
                _reloading = false;
                CurrentAmmo = RangeWeaponData.magSize;
                GetComponentInParent<CurrentWeapon>().OnReloadFinish?.Invoke();
            },RangeWeaponData.reloadTime);
        }
    }

    private bool CanShoot() => !_reloading && timeSinceLastShoot > 1f / (RangeWeaponData.fireRate / 60f);
    private bool CanReload() => CurrentAmmo < RangeWeaponData.magSize && !_reloading;

    void ShootABullet(){
        Vector3 bulletDirection = GetBulletDirection();
        Vector3 aimDir = (bulletDirection - 
            Camera.transform.position + Camera.transform.forward * 0.5f).normalized;
        GameObject bullet = Instantiate(_bullet,Camera.transform.position + Camera.transform.forward * 0.75f,Quaternion.LookRotation(aimDir,Vector3.up));
        bullet.GetComponent<Projectile>().playerGameData = playerData;
        bullet.GetComponent<DamageDealer>().DamageAmount = WeaponData.damage;
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * RangeWeaponData.velocity;
        CurrentAmmo--;
        timeSinceLastShoot = 0f;
    }

    Vector3 GetBulletDirection(){
        Vector2 randomPoint = Random.insideUnitCircle * GetComponentInParent<CurrentWeapon>().SightRadius.SightRadiusValue;
        Vector2 pointWithRecoil = new Vector2(
            Screen.width / 2f + randomPoint.x,
            Screen.height / 2f + randomPoint.y
        );
        Ray ray = Camera.ScreenPointToRay(pointWithRecoil);
        Vector3 rayDirection = Vector3.zero;
        if(Physics.Raycast(ray, out RaycastHit raycastHit,Mathf.Infinity)){
            if(raycastHit.transform.root != Camera.transform.root) return raycastHit.point;
            rayDirection = raycastHit.point;
        }
        RaycastHit[] hits;
        hits = Physics.RaycastAll(
            Camera.transform.position,
            rayDirection - Camera.transform.position,
            Mathf.Infinity
        );            
        foreach (RaycastHit hit in hits)
        {
            if(hit.transform.root != Camera.transform.root){
                return hit.point;
            }
        }
        return raycastHit.point;
    }
}
