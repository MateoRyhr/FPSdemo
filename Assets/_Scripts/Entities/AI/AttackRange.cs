using System;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] FloatVariable _attackRange;

    public bool IsTheEnemyInRange(float distanceToTarget) => distanceToTarget <= _attackRange.Value;
}