using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class RagdollDeath : MonoBehaviour
{
    [SerializeField] Collider _movementCollider;
    [SerializeField] Rigidbody _movementRigidbody;
    [SerializeField] GameObject _animatorObject;
    [SerializeField] GameObject inputs;
    // [SerializeField] GameObject _rigObject;
    [SerializeField] GameObject[] _ragdollParts;

    public void ActiveRagdoll(){
        _movementCollider.enabled = false;
        Destroy(_movementRigidbody);
        if(GetComponent<SoldierAI>()) Destroy(GetComponent<SoldierAI>());
        if(GetComponent<NavMeshAgent>()) Destroy(GetComponent<NavMeshAgent>());
        if(inputs) inputs.SetActive(false);
        // Destroy(_rigObject.gameObject);
        Component[] animationComponents = _animatorObject.GetComponents<Component>();
        for(int i=animationComponents.Length-1;i>=0;i--){
            if(!animationComponents[i].Equals(animationComponents[i].transform))
                Destroy(animationComponents[i]);
        }

        // foreach (Component component in animationComponents){
        //     if(!component.Equals(component.transform)) Destroy(component);
        // }

        foreach (GameObject ragdollPart in _ragdollParts){
            Rigidbody bodyPartRb = ragdollPart.GetComponent<Rigidbody>();
            bodyPartRb.isKinematic = false;
            bodyPartRb.collisionDetectionMode = CollisionDetectionMode.Continuous;   
            bodyPartRb.useGravity = true;
        }
        
        // GameObject ragdollInstance = Instantiate(Ragdoll,transform.position,transform.rotation);
        // ragdollInstance.transform.SetParent(transform.parent);
        // ragdollInstance.GetComponentInChildren<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
        // Destroy(gameObject);
    }
}
