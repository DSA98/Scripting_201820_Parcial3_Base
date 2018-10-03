using UnityEngine;
/// <summary>
/// Selector that succeeds if 'tagged' player is within a 'acceptableDistance' radius.
/// </summary>
public class IsTaggedActorNear : Selector
{
    [SerializeField]
    private float acceptableDistance;
    //private bool pathSucceded = false;

    protected override bool CheckCondition()
    {
        bool pathSucceded = false;
        gameObject.GetComponent<GetNearestTarget>().victim = null;
        Collider[] hits=Physics.OverlapSphere(transform.position, acceptableDistance);

        foreach(Collider hit in hits)
        {
            if(hit.gameObject.GetComponent<ActorController>() != null)
            {
                if (hit.gameObject.GetComponent<ActorController>().IsTagged)
                {
                    pathSucceded = true;
                }
            }
        }

        //return base.CheckCondition();
        return pathSucceded;
    }
}