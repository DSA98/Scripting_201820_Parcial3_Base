/// <summary>
/// Task that instructs ControlledAI to follow its designated 'target'
/// </summary>
public class FollowTarget : Task
{
    bool task = false;

    public override bool Execute()
    {
        FollowToTag();
        //return base.Execute();
        return task;
    }

    public void FollowToTag()
    {
        ControlledAI.Agent.SetDestination(gameObject.GetComponent<GetNearestTarget>().GetActor().transform.position);
        task = true;
    }
}