using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PieceOfDestructible : DamageTaker
{
    [SerializeField] private LayerMask pieceAttachedLayer;
    [SerializeField] private int pieceBrokenLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private PhysicMaterial pieceSlideMaterial;
    [SerializeField] private PhysicMaterial brokenPieceMaterial;
    [SerializeField] GameObject DamageEffect;
    public UnityEvent DamageEvent;

    private int maxPiecesToBeChecked = 64;  //More high lower performance, but more pieces can be stay in the air
    private Rigidbody _rb;
    private Collider _collider;
    private Mesh _mesh;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _mesh  = GetComponent<MeshFilter>().mesh;
    }

    private void OnCollisionExit(Collision collision) {
        if(collision.gameObject.layer == pieceBrokenLayer){
            if(!IsAttached(new List<Collider>(),out List<Collider> piecesChecked)){
                for(int i = 0; i < piecesChecked.Count; i++){
                    piecesChecked[i].GetComponent<PieceOfDestructible>().Break();
                    piecesChecked[i].GetComponent<ForceReceiver>().ReceiveForce(
                        collision.rigidbody.velocity * (1f/(i*1.5f+1f)), collision.rigidbody.centerOfMass
                    );
                }
            }
        }
    }

    public override void TakeDamage(float damage, Vector3 contactPoint, GameObject entityDamageDealer)
    {
        Break();
        PlayDamageEffect(contactPoint);
        DamageEvent?.Invoke();
    }

    void Break(){
        _rb.isKinematic = false;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _rb.useGravity = true;
        gameObject.layer = pieceBrokenLayer;
        _collider.material = pieceSlideMaterial;
        this.Invoke(() => _collider.material = brokenPieceMaterial,0.1f);
        this.Invoke(() => {
            Destroy(gameObject);
        },5f);
    }

    void PlayDamageEffect(Vector3 position){
        GameObject damageEffectInstance = Instantiate(DamageEffect,position,Quaternion.Euler(0,0,0));
        damageEffectInstance.GetComponent<ParticleSystem>().Play();
        this.Invoke(() => Destroy(damageEffectInstance),damageEffectInstance.GetComponent<ParticleSystem>().main.duration);
    }

    bool IsAttached(List<Collider> piecesChecked,out List<Collider> piecesCheckedOut){
        //Pieces checked out returns all pieces checked except itself
        piecesCheckedOut = piecesChecked;
        if(piecesChecked.Count >= maxPiecesToBeChecked) return true;
        piecesChecked.Add(GetComponent<Collider>());
        float checkPieceRadio = Vector3.Distance(_collider.bounds.center,_collider.bounds.min) * 0.1f;
        List<Collider> piecesAttached = new List<Collider>();
        //Overlapsphere for each face of the mesh, to check pieces attached.
        for(int i = 0; i < _mesh.triangles.Length; i+=3){    //for each face
            Vector3 faceCentroidLocal = (_mesh.vertices[_mesh.triangles[i]] +
                _mesh.vertices[_mesh.triangles[i+1]] +
                _mesh.vertices[_mesh.triangles[i+2]]) / 3;
            if(Physics.OverlapSphere(transform.TransformPoint(faceCentroidLocal),checkPieceRadio,groundLayer).Length > 0)
                return true;
            List<Collider> piecesOverlapWithOnFaceSphere = Physics.OverlapSphere(
                transform.TransformPoint(faceCentroidLocal),
                checkPieceRadio,pieceAttachedLayer).ToList();
            foreach (Collider piece in piecesOverlapWithOnFaceSphere)
            {
                //There are multiple overlaps so it checks the repetitions
                if(!piecesAttached.Contains(piece))
                    piecesAttached.Add(piece);
            }
        }
        //Remove pieces that has been checked
        foreach (Collider piece in piecesChecked){
            piecesAttached.Remove(piece);
        }
        if(piecesAttached.Count > 0){
            for(int i = 0; i < piecesAttached.Count; i++){
                if(piecesAttached[i].GetComponent<PieceOfDestructible>().IsAttached(piecesChecked,out List<Collider> piecesOut)) return true;
            }
        }
        return false;
    }

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.green;
    //     float checkPieceRadio = Vector3.Distance(_collider.bounds.center,_collider.bounds.max) * 0.1f;
    //     //Overlapsphere for each face of the mesh, to check pieces attached.
    //     for(int i = 0; i < _mesh.triangles.Length; i+=3){    //for each face
    //         Vector3 faceCentroidLocal = (_mesh.vertices[_mesh.triangles[i]] +
    //             _mesh.vertices[_mesh.triangles[i+1]] +
    //             _mesh.vertices[_mesh.triangles[i+2]]) / 3;
    //         Gizmos.DrawSphere(
    //             transform.TransformPoint(faceCentroidLocal),
    //             checkPieceRadio);
    //     }
    // }
}
