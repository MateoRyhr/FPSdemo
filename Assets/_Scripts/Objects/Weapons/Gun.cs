using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : RangeWeapon
{
    [Header("Weapon stats")]
    public GunData _gunData;
    [SerializeField] GameObject _bullet;
    [SerializeField] SightController sightData;
    [SerializeField] FloatVariable sightRadius;
    [SerializeField] FloatVariable sightMinimumRadius;
    [SerializeField] FloatVariable sightMaximumRadius;
    [HideInInspector] public int CurrentAmmo;

    private float _timeAfterShootToStartRevoery = 0.25f;
    private float _recoilRecoveryTime = 0.25f;

    [HideInInspector] public bool _reloading;

    float timeSinceLastShoot;

    private void Start() {
        _reloading = false;
        CurrentAmmo = _gunData.magSize;
    }

    private void Update() {
        timeSinceLastShoot += Time.deltaTime;
    }

    public override void Shoot()
    {
        if(CurrentAmmo > 0){
            if(CanShoot()){
                CancelInvoke();
                StopCoroutine("RecoilRecovery");
                ShootABullet();
                OnShoot?.Invoke();
                this.Invoke(
                    () => StartCoroutine("RecoilRecovery",sightRadius.Value-sightMinimumRadius.Value),
                    _timeAfterShootToStartRevoery);
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
                CurrentAmmo = _gunData.magSize;
            },_gunData.reloadTime);
        }
    }

    private bool CanShoot() => !_reloading && timeSinceLastShoot > 1f / (_gunData.fireRate / 60f);
    private bool CanReload() => CurrentAmmo < _gunData.magSize && !_reloading;

    void ShootABullet(){
        Vector3 bulletDirection = GetBulletDirection();

        if(sightRadius.Value < sightMaximumRadius.Value)
            if(sightRadius.Value + _gunData.recoilForce > sightMaximumRadius.Value)
                sightRadius.Value = sightMaximumRadius.Value;
            else sightRadius.Value += _gunData.recoilForce;

        Vector3 aimDir = (bulletDirection - ProjectileRespawnPosition.position).normalized;
        GameObject bullet = Instantiate(_bullet,ProjectileRespawnPosition.position,Quaternion.LookRotation(aimDir,Vector3.up));
        bullet.GetComponent<DamageDealer>().DamageAmount = WeaponData.damage;
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * _gunData.velocity;
        // bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * _gunData.velocity,ForceMode.Impulse);
        CurrentAmmo--;
        timeSinceLastShoot = 0f;
    }

    IEnumerator RecoilRecovery(float radiusToReduce){
        sightRadius.Value -= (radiusToReduce / (_recoilRecoveryTime / Time.deltaTime));
        if(sightRadius.Value < sightMinimumRadius.Value) sightRadius.Value = sightMinimumRadius.Value;
        yield return new WaitForSeconds(Time.deltaTime);
        if(sightRadius.Value > sightMinimumRadius.Value) StartCoroutine("RecoilRecovery",radiusToReduce);
    }

    Vector3 GetBulletDirection(){
        if(sightRadius.Value > sightMinimumRadius.Value){
            Vector2 randomPoint = Random.insideUnitCircle * sightRadius.Value;

            Vector2 pointWithRecoil = new Vector2(
                Screen.width / 2f + randomPoint.x,
                Screen.height / 2f + randomPoint.y
            );
            Ray ray = Camera.main.ScreenPointToRay(pointWithRecoil);
            Vector3 rayDirection = Vector3.zero;
            if(Physics.Raycast(ray, out RaycastHit raycastHit,2000f))
                rayDirection = raycastHit.point;
            RaycastHit[] hits;
            hits = Physics.RaycastAll(
                Camera.main.transform.position,
                rayDirection - Camera.main.transform.position,
                2000f
            );            
            foreach (RaycastHit hit in hits)
            {
                if(hit.transform.root != Camera.main.transform.root){
                    return hit.point;                    
                }
            }
            return sightData.SightPoint;
        }else{
            return sightData.SightPoint;
        }
    }
}
