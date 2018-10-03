/// <summary>
/// Task that instructs ControlledAI to stop its current movement.
/// </summary>
public class StopMovement : Task
{
    public override bool Execute()
    {
        ControlledAI.Agent.SetDestination(transform.position);
        //return base.Execute();
        return true;
    }
}