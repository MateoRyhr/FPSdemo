using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsConfig : MonoBehaviour
{

    //Layer

    //Player = 6
    //MovementCollider = 8
    //Bullet = 9
    //Weapon = 10

    private void Awake() {
        Physics.IgnoreLayerCollision(6,6,true);
        Physics.IgnoreLayerCollision(6,8,true);
        Physics.IgnoreLayerCollision(6,10,true);
        Physics.IgnoreLayerCollision(8,10,true);
        Physics.IgnoreLayerCollision(9,8,true);
        Physics.IgnoreLayerCollision(9,9,true);
        Physics.IgnoreLayerCollision(9,10,true);
    }
}
