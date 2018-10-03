using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController : ActorController
{
    [SerializeField]
    private float moveRadius = 50F;

    [SerializeField]
    private Root btRootNode;

    public void MoveAI()
    {
        MoveActor();
    }

    public void ExecuteRoot()
    {
        btRootNode.Execute();
    }

    protected override void Start()
    {
        base.Start();

        if (btRootNode != null)
        {
            btRootNode.SetControlledAI(this);
        }

        //AIMoveTest.Instance.onAIMoveIssued += ExecuteRoot;
        StartCoroutine(Move());
    }

    protected override void OnDestroy()
    {
        //AIMoveTest.Instance.onAIMoveIssued -= ExecuteRoot;
        base.OnDestroy();
    }

    protected override Vector3 GetTargetLocation()
    {
        Vector3 result = transform.position;

        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, moveRadius, 1))
        {
            result = hit.position;
        }

        return result;
    }

    //private void Update()
    //{
    //    btRootNode.Execute();
    //}

    private IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            ExecuteRoot();
        }
    }
}