using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public FloatVariable Radius;
    [Range(0,360)]
    public float Angle;
    // public GameObject PlayerRef;
    public LayerMask TargetMask;
    public LayerMask obstructionMask;
    [HideInInspector] public Vector3 LastEnemyPosition;
    PlayerGameData _playerData;

    [HideInInspector] public bool CanSeeEnemy;
    [HideInInspector] public GameObject EnemySeeing;

    [HideInInspector] public float DistanceToTarget;

    private void Start()
    {
        // PlayerRef = GameObject.FindGameObjectWithTag("Player");
        _playerData = GetComponent<PlayerGameData>();
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, Radius.Value, TargetMask);
        CanSeeEnemy = false;
        EnemySeeing = null;
        for(int i = 0; i < rangeChecks.Length; i++){
            Transform target = rangeChecks[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < Angle / 2)
            {
                DistanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, DistanceToTarget, obstructionMask)){
                    if(
                        target.GetComponent<PlayerGameData>().team != _playerData.team &&
                        target.GetComponent<UnitHealth>().Health > 0){
                        // Debug.Log("Seeing enemy of team: " + target.GetComponent<PlayerGameData>().team);
                        CanSeeEnemy = true;
                        EnemySeeing = target.gameObject;
                        return;
                        // LastEnemyPosition = PlayerRef.transform.position;
                    }
                }
            }
        }
    }
}
