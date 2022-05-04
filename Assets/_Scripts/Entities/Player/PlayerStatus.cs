using UnityEngine;

public class PlayerStatus : UnitStatus
{
    CapsuleCollider capsuleCollider;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public override bool GetIsOnGround(){
        // return Physics.CheckSphere(transform.position + Vector3.up * 0.1f,capsuleCollider.radius,groundLayer);
        //Center ray
        if(Physics.Raycast(new Ray(transform.position + Vector3.up*0.25f,Vector3.down),out RaycastHit hitCenter, 0.3f))
            return true;
        int numRays = 16;
        float degreesBetweenRays = 360f / numRays;
        Ray ray;
        for(int i=0;i<numRays;i++){
            Vector3 startPoint =
                this.transform.position
                + Vector3.up * 0.25f   //small amount to up to insure the ray hit the ground.
                + Vector3.forward * Mathf.Sin(degreesBetweenRays * i) * capsuleCollider.radius
                + Vector3.right * Mathf.Cos(degreesBetweenRays * i) * capsuleCollider.radius;
            ray = new Ray(startPoint, Vector3.down);
            Debug.DrawRay(startPoint,Vector3.down,Color.red,0.3f);
            if(Physics.Raycast(ray,out RaycastHit hit, 0.3f))
                return true;
        }
        return false;
    }

    public override bool GetCanMove()
    {
        return GetIsOnGround();
    }

    public override bool GetCanTurn()
    {
        return CurrentAttack != 0;
    }
}

