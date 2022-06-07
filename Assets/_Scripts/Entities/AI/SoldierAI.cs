using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAI : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] GameObject _currentWeapon;
    [SerializeField] Camera _camera;
    [SerializeField] FieldOfView fieldOfView;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] int playerLayer;
    [SerializeField] FloatVariable _timeToStartAttack;
    [SerializeField] BoolVariable _arePlayersFreezed;

    PlayerGameData _playerData;

    //Patrolling
    Vector3 _walkPoint;
    bool _walkPointSet = false;
    [SerializeField] FloatVariable walkPointRange;

    //Attacking
    // private Transform _enemyToAttack;
    private Vector3 _enemyDirection;
    // private bool enemyIsInSight;

    void Start()
    {
        _playerData = GetComponent<PlayerGameData>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {   
        if(!_arePlayersFreezed.Value){
            if(!fieldOfView.CanSeeEnemy) Patrolling();
            if(fieldOfView.CanSeeEnemy){
                _enemyDirection = (fieldOfView.EnemySeeing.transform.position - transform.position).normalized;
                AttackEnemy();
            }
        }else{
            // Debug.Log("Bots freezed");
            agent.SetDestination(transform.position);
        }
        BodyRotateWithCamera();
        Debug.DrawRay(_walkPoint + Vector3.down * 0.25f,Vector3.up,Color.red,
            GetComponent<CapsuleCollider>().height+0.25f
        );
    }

    private void Patrolling()
    {
        // Debug.Log("Patrolling");
        if(!_walkPointSet) SearchWalkPoint();

        if(_walkPointSet){
            // agent.Move(_walkPoint);
            agent.SetDestination(_walkPoint);
        }

        float distanceToWalkPoint = Vector3.Distance(transform.position,_walkPoint);

        // Debug.Log("Distance to walk point " + distanceToWalkPoint);

        //Walkpoint reached
        if(distanceToWalkPoint < 2f)
            _walkPointSet = false;
    }

    private void SearchWalkPoint(){
        // Debug.Log(("Search walk point"));
        float randomZ = Random.Range(-walkPointRange.Value, walkPointRange.Value);
        float randomX = Random.Range(-walkPointRange.Value, walkPointRange.Value);
        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(WalkPointIsCorrect(_walkPoint)){
            _walkPointSet = true;
        }
    }

    private bool WalkPointIsCorrect(Vector3 walkPoint){
        bool PointIsOnGround = Physics.Raycast(walkPoint + new Vector3(0,0.6f,0),-transform.up,5f,groundLayer);
        bool thereIsAObject = Physics.Raycast(
            walkPoint + Vector3.down * 0.25f,
            transform.up,
            GetComponent<CapsuleCollider>().height+0.25f,
            groundLayer
        );
        return PointIsOnGround && !thereIsAObject;
    }

    private void AttackEnemy()
    {
        _walkPointSet = false;
        agent.SetDestination(transform.position);
        Gun gun = _currentWeapon.GetComponentInChildren<Gun>();
        if(IsEnemyInSight()){
            // Debug.Log("Disparando");
            this.Invoke(() => {
                if(gun.CurrentAmmo > 0) gun.Shoot();
                else gun.Reload();
            },_timeToStartAttack.Value);
        }else{
            // Debug.Log("Apuntando");
            _camera.transform.LookAt(
                fieldOfView.EnemySeeing.transform.position
                + Vector3.up * fieldOfView.EnemySeeing.GetComponent<CapsuleCollider>().height/1.5f
            );
        }
    }

    // bool IsEnemyInSight() => transform.InverseTransformDirection(transform.forward) == _enemyDirection;
    bool IsEnemyInSight(){
        Vector2 middleScreenPoint = new Vector2(Screen.width/2,Screen.height/2);
        Ray ray = _camera.ScreenPointToRay(middleScreenPoint);
        if(Physics.Raycast(ray, out RaycastHit raycastHit,Mathf.Infinity)){
            // Debug.Log("Ray hit");
            if(raycastHit.transform.GetComponentInParent<PlayerGameData>())
                if(raycastHit.transform.GetComponentInParent<PlayerGameData>().team != _playerData.team)
                    return true;
        }
        return false;
    }

    void BodyRotateWithCamera(){
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x,
            _camera.transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.x
        );
    }

    // private void OnDrawGizmos(){
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(_walkPoint+Vector3.up*0.6f,0.5f);
    // }
}
