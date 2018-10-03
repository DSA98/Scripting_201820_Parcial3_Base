using UnityEngine;

public class ExampleSelector : Selector
{
    [SerializeField]
    private bool testSucceeded;

    protected override bool CheckCondition()
    {
        return testSucceeded;
    }
}