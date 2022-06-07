using UnityEngine;
using UnityEngine.AI;

public class AnimationMovement : MonoBehaviour
{   
    Animator _animator;
    Rigidbody _rb;
    NavMeshAgent _navMeshAgent;
    public FloatVariable MaxSpeed;

    //Animator parameters names
    const string SPEED = "speed";

    void Awake()
    {
        if(GetComponentInParent<NavMeshAgent>()) _navMeshAgent = GetComponentInParent<NavMeshAgent>();
        _rb = GetComponentInParent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateMoveAnimation();
    }

    void UpdateMoveAnimation()
    {
        if(_navMeshAgent)   _animator.SetFloat(SPEED,_navMeshAgent.velocity.magnitude / (MaxSpeed.Value*1f));
        else    _animator.SetFloat(SPEED,_rb.velocity.magnitude / (MaxSpeed.Value*1f));
    }
}
