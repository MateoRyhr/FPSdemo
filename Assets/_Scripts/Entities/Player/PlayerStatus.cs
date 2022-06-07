using UnityEngine;

public class PlayerStatus : UnitStatus
{
    CapsuleCollider capsuleCollider;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public override bool GetIsOnGround(){
        Collider[] objectsInSphere = Physics.OverlapSphere(transform.position,capsuleCollider.radius*0.99f);
        foreach (Collider objectInSphere in objectsInSphere){
            if(objectInSphere.transform.root != transform.root)
                return true;
        }
        return false;
        //Center ray
        // if(Physics.Raycast(new Ray(transform.position + Vector3.up*0.25f,Vector3.down),out RaycastHit hitCenter, 0.3f))
        //     return true;
        // int numRays = 16;
        // float degreesBetweenRays = 360f / numRays;
        // Ray ray;
        // for(int i=0;i<numRays;i++){
        //     Vector3 startPoint =
        //         this.transform.position
        //         + Vector3.up * 0.25f   //small amount to up to insure the ray hit the ground.
        //         + Vector3.forward * Mathf.Sin(degreesBetweenRays * i) * capsuleCollider.radius
        //         + Vector3.right * Mathf.Cos(degreesBetweenRays * i) * capsuleCollider.radius;
        //     ray = new Ray(startPoint, Vector3.down);
        //     if(Physics.Raycast(ray,out RaycastHit hit, 0.3f, groundLayer)){
        //         Debug.DrawRay(startPoint,Vector3.down,Color.red,0.30f);
        //         return true;
        //     }else{
        //         Debug.DrawRay(startPoint,Vector3.down,Color.green,0.30f);
        //     }
        // }
        // return false;
    }

    public override bool GetCanMove()
    {
        return true;
    }
}

