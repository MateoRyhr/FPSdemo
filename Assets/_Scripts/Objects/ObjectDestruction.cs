using System.Collections.Generic;
using UnityEngine;

public class ObjectDestruction : DamageTaker
{
    public List<GameObject> Pieces = new List<GameObject>();
    // public GameObject[] Pieces;
    Component[] ComponentsToDesactive;

    private void Awake(){
        ComponentsToDesactive = GetComponents<Component>();
    }

    public void DestroyObject(){
        foreach(Component component in ComponentsToDesactive){
            if(!component.Equals(transform)) Destroy(component);
        }
        foreach (GameObject piece in Pieces)
        {
            piece.SetActive(true);
        }
    }

    public override void TakeDamage(float damage, Vector3 contactPoint, GameObject entityDamageDealer)
    {
        GameObject pieceHitted = Pieces[0];
        float distance = Mathf.Infinity;
        for (int i = 0; i < Pieces.Count; i++){
            float distanceToCurrentPiece = Vector3.Distance(contactPoint,Pieces[i].transform.position);
            if(distanceToCurrentPiece < distance){
                distance = distanceToCurrentPiece;
                pieceHitted = Pieces[i];
            }
        }
        if(pieceHitted.GetComponent<ObjectDestruction>()){
            pieceHitted.GetComponent<ObjectDestruction>().TakeDamage(damage,contactPoint,entityDamageDealer);
        }else{
            if(pieceHitted.GetComponent<DamageOnlyEffect>())
                pieceHitted.GetComponent<DamageOnlyEffect>().TakeDamage(damage,contactPoint,entityDamageDealer);
            pieceHitted.GetComponent<Rigidbody>().isKinematic = false;
            pieceHitted.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            pieceHitted.GetComponent<Rigidbody>().useGravity = true;
            pieceHitted.GetComponent<ForceReceiver>().enabled = true;
            // pieceHitted.GetComponent<Collider>().enabled = true;
            this.Invoke(() => {
                Pieces.Remove(pieceHitted);
                Destroy(pieceHitted);
            },10f);
        }
    }
}
