using UnityEngine;

public class ExampleTask : Task
{
    [SerializeField]
    private bool testSucceeded;

    public override bool Execute()
    {
        return testSucceeded;
    }
}