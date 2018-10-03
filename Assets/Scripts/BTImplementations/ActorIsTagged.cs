/// <summary>
/// Selector that succeeeds if ControlledAI is marked as 'tagged'
/// </summary>
public class ActorIsTagged : Selector
{
    //private bool taskSucceded = false;

    protected override bool CheckCondition()
    {
        bool taskSucceded = false;
        if (ControlledAI.IsTagged)
        {
            taskSucceded = true;
        }
        return taskSucceded;
    }
}